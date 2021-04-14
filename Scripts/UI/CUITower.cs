using System;
using UnityEngine;
using UnityEngine.UI;

public class CUITower : CBaseUI
{
    [SerializeField] private Text ins_txtTower;
    [SerializeField] private Text ins_txtBtnClose;
    [SerializeField] private CUITowerScrollView ins_cUITowerScrollView;
    public CUITowerScrollView m_cUITowerScrollView { get { return ins_cUITowerScrollView; } }

    public void Initialization()
    {
        StartCoroutine(ins_cUITowerScrollView.CorMakeDataModel());
        SetTextInfo();
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();

        ins_txtTower.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 22).ToString();
        ins_txtBtnClose.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 21).ToString();
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
