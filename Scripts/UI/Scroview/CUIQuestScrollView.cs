using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;

public class CUIQuestScrollView : OSA<CQuestModelParams, CQuestViewHolder>
{

    #region OSA Implementation

    protected override void Awake()
    {
        base.Awake();
        SetTestDataModdel();
    }

    protected override CQuestViewHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CQuestViewHolder();
        instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
        return instance;
    }

    protected override void UpdateViewsHolder(CQuestViewHolder newOrRecycled)
    {
        newOrRecycled.UpdateView(_Params.Data[newOrRecycled.ItemIndex]);
    }

    #endregion


    public void SetTestDataModdel()
    {
        _Params.Data.Clear();
        StartCoroutine(CorQuestInstall());
        
    }

    private IEnumerator CorQuestInstall()
    {
        yield return new WaitForSeconds(.2f);

        for(int i =0; i<3; i++)
        {
            CQuestModel model = new CQuestModel(i);
            _Params.Data.Add(model);
        }
        ResetItems(_Params.Data.Count);
    }

    public void RemoveQuestFrom(int nIdx, int nCnt)
    {
        _Params.Data.RemoveAt(nIdx);
        RemoveItems(nIdx, nCnt);
    }
}

public class CQuestModel
{
    public int m_nId;

    public CQuestModel(int nId)
    {
        this.m_nId = nId;
    }
}

[System.Serializable]
public class CQuestModelParams : BaseParamsWithPrefabAndData<CQuestModel> { }

public class CQuestViewHolder : BaseItemViewsHolder
{
    private CUIQuestCell _cUIQuestCell;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUIQuestCell = root.GetComponent<CUIQuestCell>();
    }

    public void UpdateView(CQuestModel cModel)
    {
        _cUIQuestCell.SetData(cModel);
    }
}