using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIProfileWin : CBaseUI
{
    [SerializeField] private Text ins_txtClose;
    [SerializeField] private CUIProfileGridScrollview ins_cProfielGridScrollView;


    public void Initialization()
    {
        SetTextInfo();
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtClose.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 31);
    }

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestory = true)
    {
        base.Close(bDestory);
        CUIManager.Inst.m_cUIMap.Open(_callBack);
    }
}
