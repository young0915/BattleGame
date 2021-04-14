using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIDialog : CBaseUI
{

    [SerializeField] private Text ins_txtDialogName;
    [SerializeField] private Text ins_txtDialogContent;

    public void Initialization(string strName, string strContent)
    {
        SetTextInfo();
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();


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
