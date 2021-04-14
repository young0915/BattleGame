using System;
using UnityEngine;
using UnityEngine.UI;


public class CUIStart : CBaseUI
{

    [SerializeField] private Text ins_txtGameStart;

    public void Initialization()
    {
        SetTextInfo();
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtGameStart.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 1);
    }

    public void OnClickGameStart()
    {
        StartCoroutine(CUIManager.Inst.CorLogin());
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
