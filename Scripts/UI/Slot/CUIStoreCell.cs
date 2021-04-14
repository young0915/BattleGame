using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CUIStoreCell : MonoBehaviour
{

    [Header("Hero & Item")]
    [SerializeField] private Image ins_imgProfile;
    [SerializeField] private GameObject ins_objProfileItemNHero;
    [SerializeField] private GameObject ins_objProfileTower;
    [SerializeField] private GameObject ins_objGroupTitleItemHero;
    [SerializeField] private GameObject ins_objGroupTitleTower;
    [SerializeField] private GameObject ins_objGroupContent;
    [SerializeField] private Button ins_btnStroeCell;
    [SerializeField] private Button ins_btnPurchase;
    [SerializeField] private Text ins_txtTitle;
    [SerializeField] private Text ins_txtContent;
    [SerializeField] private Text ins_txtMoney;

    [Header("Tower")]
    [SerializeField] private Image ins_imgTowerProfile;
    [SerializeField] private Text ins_txtTowerTitle;

    private EmItem _eItem;
    private int _nId = 0;
    private int _nPlayerId = 0;
    private bool _bClickLock = false;
    private int _nMoney = 0;
    private string _strPath = string.Empty;
    private string _strTitle = string.Empty;
    private string _strContent = string.Empty;


    // 변경될 때 사용할 변수.
    private int _nTowerLv = 0;
    private int _nLv = 0;
    private int _nAttack = 0;
    private string _strName = string.Empty;
    private string _strProfile = string.Empty;


    public void SetData(CStoreModel Model)
    {
        this._nId = Model.m_nId;
        this._eItem = Model.m_eItem;
        ins_objProfileItemNHero.SetActive(false);
        ins_objProfileTower.SetActive(false);
        ins_objGroupTitleItemHero.SetActive(false);
        ins_objGroupTitleTower.SetActive(false);
        ins_objGroupContent.SetActive(false);

        // OSA 의 버그를 방지하기 위한 것.
        if (Model.m_eCellState == EmCellState.NotComplete)
        {
            ins_btnPurchase.interactable = true;
            ins_btnStroeCell.interactable = true;
        }
        else
        {
            ins_btnPurchase.interactable = false;
            ins_btnStroeCell.interactable = false;
        }


        switch (_eItem)
        {
            case EmItem.Hero:
                ins_objProfileItemNHero.SetActive(true);
                ins_objGroupTitleItemHero.SetActive(true);
                ins_objGroupContent.SetActive(true);


                _strPath = CDataManager.Inst.m_strProfilePath + CDBManager.Inst.m_listHeroInfo[_nId].m_strProfile.ToString();

                _strTitle = CDBManager.Inst.m_listHeroInfo[_nId].m_strName;
                _strContent = CDBManager.Inst.m_listHeroInfo[_nId].m_strContent;
                _nMoney = CDBManager.Inst.m_listHeroInfo[_nId].m_nMoney;

                break;

            case EmItem.Tower:
                ins_objProfileTower.SetActive(true);
                ins_objGroupTitleTower.SetActive(true);

                _nPlayerId = ((int)CDBManager.Inst.m_listTowerInfo[_nId].m_eTowerType) - 1;
                if (CDBPlayerInfo.Inst.m_listTowerInfo[_nPlayerId].m_eTowerType != EmTowerType.None)
                {
                    _nTowerLv = _nId + 1;
                }
                else
                {
                    _nTowerLv = _nId;
                }

                _strPath = CDataManager.Inst.m_strTowerProfilePath + CDBManager.Inst.m_listTowerInfo[_nTowerLv].m_strProfile;
                _strTitle = CDBManager.Inst.m_listTowerInfo[_nTowerLv].m_strName;
                _strContent = string.Empty;
                _nMoney = CDBManager.Inst.m_listTowerInfo[_nTowerLv].m_nMoney;

                break;

            case EmItem.Etc:
                ins_objProfileItemNHero.SetActive(true);
                ins_objGroupTitleItemHero.SetActive(true);
                ins_objGroupContent.SetActive(true);

                _strPath = CDataManager.Inst.m_strItemProfilePath + CDBManager.Inst.m_listItemInfo[_nId].m_strPath;
                _strTitle = CDBManager.Inst.m_listItemInfo[_nId].m_strName;
                _strContent = CDBManager.Inst.m_listItemInfo[_nId].m_strContent;
                _nMoney = CDBManager.Inst.m_listItemInfo[_nId].m_nMoney;


                break;

        }

        ins_imgProfile.sprite = CResourceLoader.Load<Sprite>(_strPath);
        ins_imgTowerProfile.sprite = CResourceLoader.Load<Sprite>(_strPath);
        ins_txtTitle.text = _strTitle.ToString();
        ins_txtTowerTitle.text = _strTitle.ToString();
        ins_txtContent.text = _strContent.ToString();
        ins_txtMoney.text = _nMoney.ToString();



    }

    public void OnClickPurchase()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;


        if (CDBPlayerInfo.Inst.m_nMoney >= _nMoney)
        {
            // 돈이 있을 경우.

 
            switch (_eItem)
            {
                case EmItem.Hero:
                    if (CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_eHeroType == 0)
                    {
                        CDBPlayerInfo.Inst.SetHeroPurchase((int)CDBManager.Inst.m_listHeroInfo[_nId].m_eHeroType, CDBManager.Inst.m_listHeroInfo[_nId].m_nId);

                    }
                    else
                    {
                        // 캐릭터를 소지하고 있다면.
                        StartCoroutine(CUIManager.Inst.CorAction(EmUIActionType.PossessionProduct));
                        _bClickLock = false;
                        return;
                    }

                    break;

                case EmItem.Tower:

                    TowerPurchase();

                    break;
                case EmItem.Etc:

                    ItemPurchase();

                    break;
            }

        }
        else
        {
            // 돈이 없을 경우.
            StartCoroutine(CUIManager.Inst.CorAction(EmUIActionType.lackMoney));
        }

        ins_btnPurchase.interactable = false;
        StartCoroutine(CorBtnCoolTime());

        CDBPlayerInfo.Inst.m_nMoney -= _nMoney;
        CDBPlayerInfo.Inst.OnPlayerInfoUpdate(CDBPlayerInfo.Inst.m_nMoney);

        _bClickLock = false;
    }

    private void TowerPurchase()
    {
        _nPlayerId = ((int)CDBManager.Inst.m_listTowerInfo[_nId].m_eTowerType) - 1;

        if (CDBPlayerInfo.Inst.m_listTowerInfo[_nPlayerId].m_eTowerType == EmTowerType.None)
        {

            _nLv = CDBManager.Inst.m_listTowerInfo[_nId].m_nLv;
            _nAttack = CDBManager.Inst.m_listTowerInfo[_nId].m_nAttack;
            _strName = CDBManager.Inst.m_listTowerInfo[_nId].m_strName;
            _strProfile = CDBManager.Inst.m_listTowerInfo[_nId].m_strProfile;

        }
        else
        {

            _nLv = CDBManager.Inst.m_listTowerInfo[_nTowerLv].m_nLv;
            _nAttack = CDBPlayerInfo.Inst.m_listTowerInfo[_nId].m_nAttack
                + CLevelManager.Inst.m_listLevel[_nLv - 1].m_nAttack;

            _strName = CDBManager.Inst.m_listTowerInfo[_nTowerLv].m_strName;

            _strProfile = CDBManager.Inst.m_listTowerInfo[_nTowerLv].m_strProfile;

        }

        CDBPlayerInfo.Inst.SetTowerPurchase(_nPlayerId,
                    (int)CDBManager.Inst.m_listTowerInfo[_nId].m_eTowerType,
                       _nLv,
                       _strName,
                       CDBManager.Inst.m_listTowerInfo[_nId].m_nMoney,
                       _nAttack,
                      _strProfile
                       );
    }

    private void ItemPurchase()
    {
        string strItem = CDBManager.Inst.m_listItemInfo[_nId].m_strPath;
        int nCnt = 0;


        for (int i = 0; i < CDBPlayerInfo.Inst.m_listInvenInfo.Count; i++)
        {
            if (CDBPlayerInfo.Inst.m_listInvenInfo[i].m_eInven == EmInven.None)
            {
                CDBPlayerInfo.Inst.SetItemUpdate(1, _nId, 1, strItem, i);
                return;
            }
            else
            {
                if (CDBPlayerInfo.Inst.m_listInvenInfo[i].m_strItemPath == CDBManager.Inst.m_listItemInfo[_nId].m_strPath && 
                    CDBPlayerInfo.Inst.m_listInvenInfo[i].m_nCnt <11)
                {
                    // 수량확인.
                    if(CDBPlayerInfo.Inst.m_listInvenInfo[i].m_nCnt < 11)
                    {
                        nCnt = CDBPlayerInfo.Inst.m_listInvenInfo[i].m_nCnt + 1;
                        CDBPlayerInfo.Inst.SetSameItemPurchase(nCnt, _nId);
                        return;
                    }
                    else
                    {
                        CDBPlayerInfo.Inst.SetItemUpdate(1, _nId, 1, strItem, (i+1));
                        return;
                    }
                }
            }
        }
    }

    private IEnumerator CorBtnCoolTime()
    {

        yield return new WaitForSeconds(0.5f);
        ins_btnPurchase.interactable = true;
    }

   
}
