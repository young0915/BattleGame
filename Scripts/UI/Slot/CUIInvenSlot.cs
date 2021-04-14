using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//, IPointerExitHandler
public class CUIInvenSlot : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Text ins_txtItmeCnt;
    [SerializeField] private Image ins_ImgItem;
    [SerializeField] private Toggle ins_tgDelete;                         // tg는 Toggle의 약자.

    private CInvenModel cInvenModel;

    private bool _bClickLock = false;
    private bool _bIsEnabled = false;
    private int _nId = 0;
    private string _strItemPath = string.Empty;

    private int _nItemCnt;

    public void SetInven(CInvenModel cModel)
    {
        this.cInvenModel = cModel;

        this._nId = cInvenModel.m_nId;
        this.ins_tgDelete.enabled = cInvenModel.m_bIsOn;
        this.ins_tgDelete.isOn = cInvenModel.m_bIsOn;
        this._bIsEnabled = cInvenModel.m_bIsOn;

        SetItem();
    }


    // 인베에 있는 아이템 셋팅.
    private void SetItem()
    {
        // 현재 가지고 있는 아이템 수량.
        _nItemCnt = CDBPlayerInfo.Inst.m_listInvenInfo[_nId].m_nCnt;
        ins_txtItmeCnt.text = _nItemCnt.ToString();



        // 아이템 이미지.
        _strItemPath = CDataManager.Inst.m_strItemProfilePath + CDBPlayerInfo.Inst.m_listInvenInfo[_nId].m_strItemPath;
        ins_ImgItem.sprite = CResourceLoader.Load<Sprite>(_strItemPath);

        SetItemState();
    }

    private void SetItemState()
    {
        if (CDBPlayerInfo.Inst.m_listInvenInfo[_nId].m_eInven == EmInven.None)
        {
            this.ins_ImgItem.enabled = false;
            this.ins_txtItmeCnt.enabled = false;

        }
        else
        {
            this.ins_ImgItem.enabled = true;

            if (_nItemCnt > 1)
            {
                this.ins_txtItmeCnt.enabled = true;
                this.ins_txtItmeCnt.text = _nItemCnt.ToString();
            }
        }
    }


    public void OnClickInven()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        if(_bIsEnabled)
        {
            _bIsEnabled = false;
        }
        else
        {
            _bIsEnabled = true;
        }


        ins_tgDelete.isOn = _bIsEnabled;
        cInvenModel.m_bIsOn = ins_tgDelete.isOn;

        _bClickLock = false;
    }

    #region [code] EventSystems


    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // 아이템 정보창 생성.
            StartCoroutine(CUIManager.Inst.CorItemInfo(transform, _nId));
        }
    }


    #endregion

}
