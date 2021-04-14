using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CUIInventory : CBaseUI
{
    [SerializeField] private Text ins_txtInventory;
    [SerializeField] private CUIInventoryGridView ins_cUIInvenotoryGridView;

    private bool _bClickLock = false;

    public void Initialization()
    {
        SetTextInfo();
        CDBPlayerInfo.Inst.Inven();

    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtInventory.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 18).ToString();
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




