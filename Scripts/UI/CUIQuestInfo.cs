using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CUIQuestInfo : CBaseUI
{

    [SerializeField] private TextAsset ins_txtQuestInfo;

    [SerializeField] private Text ins_txtQuestTitle;
    [SerializeField] private Text ins_txtQuestContent;
    [SerializeField] private Text ins_txtReward;
    [SerializeField] private Text ins_txtClose;

    [SerializeField] private List<CUIQuestRewardSlot> _listQuestRewardSlot = new List<CUIQuestRewardSlot>();

    private int _nCoin = 0;
    private string _strImgPath = string.Empty;

    private int _nId = 0;

    public void Initialization(int nId)
    {
        this._nId = nId;

        SetTextInfo();

        ins_txtQuestTitle.text = CQuestManager.Inst.m_listQuestInfo[nId].m_strTitle.ToString();
        ins_txtQuestContent.text = CQuestManager.Inst.m_listQuestInfo[nId].m_strContent.ToString();

        _strImgPath = CQuestManager.Inst.m_listQuestInfo[nId].m_strImgPath;
        _nCoin = CQuestManager.Inst.m_listQuestInfo[nId].m_nCoin;

        _listQuestRewardSlot[0].SetSlot(_strImgPath, _nCoin);
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();

        ins_txtReward.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 16).ToString();
        ins_txtClose.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 21).ToString();

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
