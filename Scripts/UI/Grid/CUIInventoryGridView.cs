using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.CustomAdapters.GridView;
using Com.TheFallenGames.OSA.DataHelpers;



public class CUIInventoryGridView : GridAdapter<GridParams, CInvenViewHolder>
{

    public SimpleDataHelper<CInvenModel> Data { get; private set; }

    private bool _bIsOn = false;
    private bool _bClickLock = false;

    protected override void Start()
    {
        base.Start();

        Data = new SimpleDataHelper<CInvenModel>(this);


        StartCoroutine(CorMakeData(15));

    }

    protected override void UpdateCellViewsHolder(CInvenViewHolder viewsHolder)
    {
        CInvenModel Model = Data[viewsHolder.ItemIndex];

        viewsHolder.UpdateView(Model);
    }
    private IEnumerator CorMakeData(int nCnt)
    {
        yield return new WaitForSeconds(.5f);
        var newItems = new CInvenModel[nCnt];

        for (int i = 0; i < nCnt; i++)
        {
            CInvenModel model = new CInvenModel(i, true);

            newItems[i] = model;
        }
        OnDataRetrieved(newItems);
    }

    private void OnDataRetrieved(CInvenModel[] newItems)
    {
        Data.InsertItemsAtStart(newItems);
        Data.NotifyListChangedExternally();
    }

    public void OnClickDeleteItem()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        // 쓰레기 버튼이 활성화가 되어있다면.
        if (_bIsOn)
        {
            // 쓰레기 닫기.
            // tg는 Toggle의 약자.
            // CUIInvenSlot of Data[i].m_tgDelete is false;
            _bIsOn = false;

            for (int i = 0; i < 15; i++)
            {

                if (!Data[i].m_bIsOn)
                {
                    // 삭제 할 아이템.
                    CDBPlayerInfo.Inst.SetItemUpdate(0, 16, 0, "0", i);
                }

                // 삭제하지 않을 아이템.
                Data[i].m_bIsOn = _bIsOn;
            }
        }
        else
        {
            _bIsOn = true;
            for(int i = 0; i<15; i++)
            {
                Data[i].m_bIsOn = _bIsOn;
            }

        }





        Data.NotifyListChangedExternally();

        _bClickLock = false;
    }

    private void DeleteItem()
    {
        for (int i = 0; i < Data.Count; i++)
        {

            Data[i].m_bIsOn = false;

        }
    }

}


public class CInvenModel
{
    public int m_nId;
    public bool m_bIsOn = false;

    public CInvenModel(int nId, bool bIsOn)
    {
        this.m_nId = nId;
         this.m_bIsOn = bIsOn;
    }
}

public class CInvenViewHolder : CellViewsHolder
{
    private CUIInvenSlot _cUIInvenSlot;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUIInvenSlot = root.GetComponent<CUIInvenSlot>();
    }

    public void UpdateView(CInvenModel cModel)
    {
        _cUIInvenSlot.SetInven(cModel);
    }
}
