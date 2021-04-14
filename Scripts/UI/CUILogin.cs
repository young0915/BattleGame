using System;
using UnityEngine;
using UnityEngine.UI;

public class CUILogin : CBaseUI
{

    [SerializeField] private Text ins_txtTitle;
    [SerializeField] private Text ins_txtSubTitle;
    [SerializeField] private Text ins_txtNickName;
    [SerializeField] private Text ins_txtBtnLogin;

    [SerializeField] private InputField ins_InputFieldNickName;
    public InputField m_InputFieldNickName { get { return ins_InputFieldNickName; } }


    private bool _bClickLock = false;

    public void Initialization()
    {
        SetTextInfo();
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtTitle.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 3).ToString();
        ins_txtSubTitle.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 4).ToString();

        ins_txtNickName.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 5).ToString();
        ins_txtBtnLogin.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 7).ToString();
    }


    public void OnClickLogin()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        // 검색할 때는 글자수 검사.
        if(!SetTextCheck())
            return;

        if (ins_InputFieldNickName.text.Length < 2 || ins_InputFieldNickName.text.Length >= 8)
        {
            StartCoroutine(CUIManager.Inst.CorAction(EmUIActionType.NicknameNotallowed));
            ins_InputFieldNickName.text = String.Empty;
            _bClickLock = false;
            return;
        }

        CDBPlayerInfo.Inst.AddPlayerInfo(ins_InputFieldNickName.text, 1000);

        StartCoroutine(CUIManager.Inst.CorLoding("CLobbyScene"));


        _bClickLock = false;
    }


    // 검색할 때는 글자수 검사.
    private bool SetTextCheck()
    {
        if (ins_InputFieldNickName.text == "")
            return false;
        else  return true;
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

