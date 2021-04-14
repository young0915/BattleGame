using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIMap : CBaseUI
{
    private bool _bClickLock = false;

    public void OnClickBattleMove(string strBattleScene)
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        StartCoroutine(CUIManager.Inst.CorBattlePreparation(strBattleScene));

        _bClickLock = false;
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
