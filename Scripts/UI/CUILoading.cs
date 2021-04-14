using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CUILoading : CBaseUI
{

    [SerializeField] private Text ins_txtLoding;
    [SerializeField] private Slider ins_LodingSlider;
    public Slider m_LodingSlider { get { return ins_LodingSlider; } }

    private string strName = string.Empty;

    public void Initialization(string strSceneName)
    {
        SetTextInfo();
        this.strName = strSceneName;
        StartCoroutine(CSceneManager.Inst.CorSceneLoad(strSceneName));
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtLoding.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 2);
    }


    public override void Close(bool bDestory = true)
    {
        base.Close(bDestory);
    }
    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }
}
