using System;
using UnityEngine;
using UnityEngine.UI;

public class CUILobbyPreferences : CBaseUI
{
    [SerializeField] private Text ins_txtPreferences;
    [SerializeField] private Text ins_txtSound;
    [SerializeField] private Slider ins_SoundSlider;

    public void Initialization()
    {
        SetTextInfo();
        SetSound();

        ins_SoundSlider.value = CSoundManager.Inst.GetMusic(1).volume;

    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtPreferences.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 19).ToString();
        ins_txtSound.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 20).ToString();
    }

    public void SetSound()
    {
        CSoundManager.Inst.GetMusic(1).volume = ins_SoundSlider.value;
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
