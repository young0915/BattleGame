using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIBattlePreferences : CBaseUI
{

    [SerializeField] private Text ins_txtRePlay;
    [SerializeField] private Text ins_txtOut;

    private bool _bClickLock = false;

    public void Initialization()
    {
        SetTextInfo();
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtRePlay.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 34).ToString();
        ins_txtOut.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 33).ToString();
    }

    public void OnClickResume()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;
        Time.timeScale = 1;

        _bClickLock = false;
        Close();

    
    }

    public void OnClickEnd()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        CSceneManager.Inst.OnSceneMovement("CLobbyScene");
        Time.timeScale = 1;

        _bClickLock = false;
        Close();
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
