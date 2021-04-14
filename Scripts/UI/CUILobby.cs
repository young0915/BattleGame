using System;
using UnityEngine;
using UnityEngine.UI;

public class CUILobby : CBaseUI
{

    [SerializeField] private Text ins_txtNickName;
    public Text m_txtNickName { get { return ins_txtNickName; } }
    [SerializeField] private Text ins_txtMoney;                  // 현재 가지고 있는 돈.
    [SerializeField] private Text ins_txtShop;
    [SerializeField] private Text ins_txtBtnHero;
    [SerializeField] private Text ins_txtBtnInven;
    [SerializeField] private Text ins_txtBtnTower;
    [SerializeField] private Image ins_ImgProfile;

    [SerializeField] private CUIQuestScrollView ins_cQuestScroView;
    public CUIQuestScrollView m_cQuestScrollView { get { return ins_cQuestScroView; } }

    private int _nMoney;
    private bool _bClickLock = false;

    public void Initialization()
    {
        SetTextInfo();

    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();

        // 현재 가지고 있는  돈.

        _nMoney = CDBPlayerInfo.Inst.m_nMoney;
        ins_txtMoney.text = _nMoney.ToString();

        ins_txtNickName.text = CDBPlayerInfo.Inst.m_strName.ToString();
        ins_txtShop.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 11);
        ins_txtBtnHero.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 12);
        ins_txtBtnInven.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 13);
        ins_txtBtnTower.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 23);


    }

    // 프로필을 수정하려면.
    public void OnClickProfileModify()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        StartCoroutine(CUIManager.Inst.CorProfileWin());

        _bClickLock = false;
    }

    public void OnClickLobbyPreferences()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;


        StartCoroutine(CUIManager.Inst.CorLobbyPreferences());

        _bClickLock = false;
    }


    public void OnClickInventory()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        StartCoroutine(CUIManager.Inst.CorInventory());

        _bClickLock = false;
    }

    public void OnClickStore()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        StartCoroutine(CUIManager.Inst.CorStore());

        _bClickLock = false;
    }




    public void OnClickHero()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        StartCoroutine(CUIManager.Inst.CorHero());

        _bClickLock = false;
    }

    public void OnClickTower()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        StartCoroutine(CUIManager.Inst.CorTower());

        _bClickLock = false;
    }

    public void SetMoney(int nCoin)
    {
        _nMoney = nCoin;
        ins_txtMoney.text = _nMoney.ToString();
    }

    public Image SetProfile()
    {
        return ins_ImgProfile;
    }

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestory = true)
    {
        base.Close(bDestory);
    }

}
