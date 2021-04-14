﻿using System;
using UnityEngine;

public class CBaseUI : MonoBehaviour
{
    protected Action<EmClickState> _callBack = null;
    protected EmClickState _eClickState = EmClickState.Cancel;

    public virtual void Open(Action<EmClickState> callBack)
    {
        CUIManager.Inst.AddUI(this);

        _callBack = callBack;

        gameObject.SetActive(true);
    }

    public virtual void Close(bool bDestory = true)
    {
        CUIManager.Inst.RemoveUI(this);

        _callBack?.Invoke(_eClickState);

        if (bDestory)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    protected virtual void SetTextInfo() {}
}
