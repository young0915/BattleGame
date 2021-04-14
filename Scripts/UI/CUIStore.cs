using System;
using UnityEngine;

public class CUIStore : CBaseUI
{
    [SerializeField] private CUIStoreScrollView ins_cStoreScrollView;

    private EmItem _eItem;
    private bool _bClickLock = false;

    public void Initialization()
    {
        // 초기화로 히어로 먼저 생성하기.
        ins_cStoreScrollView.SetMakeDataModel(EmItem.Hero);
    }

    public void OnClickTab(int nIdx)
    {
        if (_bClickLock)
            return;

        EmItem eItem = (EmItem)nIdx;

        if (_eItem == eItem)
            return;
      
        SetStore(eItem);

    }

    private void SetStore(EmItem eStoreType)
    {
        switch (eStoreType)
        {
            case EmItem.Hero:
                ins_cStoreScrollView.SetMakeDataModel(EmItem.Hero);

                break;

            case EmItem.Tower:
                ins_cStoreScrollView.SetMakeDataModel(EmItem.Tower);

                break;

            case EmItem.Etc:
                ins_cStoreScrollView.SetMakeDataModel(EmItem.Etc);

                break;
        }
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
