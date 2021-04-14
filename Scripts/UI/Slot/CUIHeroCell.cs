using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;

public class CUIHeroCell : MonoBehaviour
{


    [SerializeField] private Image ins_ImgHeroProfile;
    [SerializeField] private Image ins_ImgGuardianstone;
    [SerializeField] private List<Image> ins_ImgIcons;


    [Space]
    [SerializeField] private Text ins_txtMyHero;
    [SerializeField] private Text ins_txtHeroName;
    [SerializeField] private Text ins_txtHeroContent;
    [SerializeField] private Text ins_txtReinforcement;
    [SerializeField] private Text ins_txtHeroLv;

    [SerializeField] private Text ins_txtAttack;
    [SerializeField] private Text ins_txtCritical;

    [SerializeField] private Slider ins_SliderAttack;
    [SerializeField] private Slider ins_SliderCritical;

    private EmHeroType _eHeroType;
    public EmHeroType m_eHeroType { get { return _eHeroType; } }

    private int _nId = 0;
    public int m_nId { get { return _nId; } }
    private int _nLv = 0;
    private int _nHp = 0;
    private int _nAttack = 0;
    private int _nCritical = 0;
    private string _strHeroName = string.Empty;
    private string _strHeroContent = string.Empty;
    private string _strPath = string.Empty;
    private string _strGuardianStonePath = string.Empty;
    private bool _bClickLock = false;


    public void SetData(int nId)
    {
        this._nId = nId;
        this._eHeroType = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_eHeroType;
        this._strHeroName = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_strName.ToString();
        this._strHeroContent = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_strContent.ToString();
        this._strPath = CDataManager.Inst.m_strProfilePath + CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_strProfile.ToString();
        this._strGuardianStonePath = CDataManager.Inst.m_strItemProfilePath + CDBManager.Inst.m_listItemInfo[_nId].m_strPath.ToString();

        for (int i = 0; i < ins_ImgIcons.Count; i++)
        {
            ins_ImgIcons[i].gameObject.SetActive(false);
        }

        ins_txtHeroLv.text = string.Format(" {0} {1} ", CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 32), CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nLv);

        ins_txtMyHero.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 12).ToString();
        ins_txtHeroName.text = _strHeroName.ToString();
        ins_txtHeroContent.text = _strHeroContent.ToString();

        ins_txtAttack.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 24);
        ins_txtCritical.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 25);


        ins_SliderAttack.maxValue = 18;
        ins_SliderCritical.maxValue = 7;

        ins_SliderAttack.value = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nAttack;
        ins_SliderCritical.value = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nCritical;


        ins_ImgHeroProfile.sprite = CResourceLoader.Load<Sprite>(_strPath);
        ins_ImgGuardianstone.sprite = CResourceLoader.Load<Sprite>(_strGuardianStonePath);

        ins_txtReinforcement.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 37);

        SetHeroIcon(this._eHeroType);
    }


    private void SetHeroIcon(EmHeroType eHeroType)
    {
        switch (eHeroType)
        {
            case EmHeroType.Striker:
                ins_ImgIcons[0].gameObject.SetActive(true);

                break;

            case EmHeroType.Penetration:
                ins_ImgIcons[1].gameObject.SetActive(true);

                break;

            case EmHeroType.Recovery:
                ins_ImgIcons[2].gameObject.SetActive(true);
                break;
        }

    }


    public void SetData(int nId, EmHeroType eHeroType, string strName, string strHeroContent, string strPath)
    {
        this._nId = nId;
        this._eHeroType = eHeroType;
        this._strHeroName = strName;
        this._strHeroContent = strHeroContent;
        this._strPath = strPath;
    }

    private int _nCnt = 0;

    public void OnClickReinforcement()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        // (조건) Lv 3 이하만.
        if (CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nLv <= 2)
        {
            for (int i = 0; i < CDBPlayerInfo.Inst.m_listInvenInfo.Count; i++)
            {
                if (CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nId == CDBPlayerInfo.Inst.m_listInvenInfo[i].m_nItemId)
                {
                    HeroUpgrade();

                    _nCnt = CDBPlayerInfo.Inst.m_listInvenInfo[i].m_nCnt - 1;
                    if (_nCnt > 0)
                    {
                        CDBPlayerInfo.Inst.SetSameItemPurchase(_nCnt, CDBPlayerInfo.Inst.m_listInvenInfo[i].m_nId);
                    }
                    else
                    {
                        //수량이 하나만 있다면 전부 삭제.
                        CDBPlayerInfo.Inst.SetItemUpdate(0, 16, 0, "0", CDBPlayerInfo.Inst.m_listInvenInfo[i].m_nId);
                    }

                    _bClickLock = false;
                    return;
                }
                else
                {
                    
                    StartCoroutine(CUIManager.Inst.CorAction(EmUIActionType.NoSeatsAvailable));
                }
            }
        }
        else
        {
            // 강화 불가.
            StartCoroutine(CUIManager.Inst.CorAction(EmUIActionType.Unreinforceable, _strHeroName));
        }
       

        _bClickLock = false;
    }


    private void HeroUpgrade()
    {
       _nLv = (CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nLv-1) + 1;

        _nHp = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nHp + CLevelManager.Inst.m_listLevel[_nLv].m_nHp;
        _nAttack = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nAttack + CLevelManager.Inst.m_listLevel[_nLv].m_nAttack;
        _nCritical = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_nCritical + CLevelManager.Inst.m_listLevel[_nLv ].m_nCritical;

        CDBPlayerInfo.Inst.SetHeroEnhance(CLevelManager.Inst.m_listLevel[_nLv].m_nLv, _nHp, _nAttack, _nCritical, _nId);
    }
}
