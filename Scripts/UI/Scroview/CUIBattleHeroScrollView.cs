using UnityEngine;
using System.Collections;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;

public class CUIBattleHeroScrollView : OSA<CBattleModelParams, CBattleViewHolder>
{
    #region OSA Implementation
    protected override CBattleViewHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CBattleViewHolder();
        instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
        return instance;
    }

    protected override void UpdateViewsHolder(CBattleViewHolder newOrRecycled)
    {
        newOrRecycled.UpdateView(_Params.Data[newOrRecycled.ItemIndex]);
    }
    #endregion

    public IEnumerator CorMakeDataModel()
    {
        _Params.Data.Clear();
        yield return new WaitForSeconds(.2f);
        for(int i = 0; i< CDBPlayerInfo.Inst.m_listHeroInfo.Count; i++)
        {
            CBattleModel cModel = new CBattleModel(i);
            _Params.Data.Add(cModel);
        }


        ResetItems(_Params.Data.Count);
    }
}


public class CBattleModel
{
    public int m_nId;
    public EmCellState m_eCellState = EmCellState.NotComplete;

    public CBattleModel(int nId)
    {
        this.m_nId = nId;
    }
}

[System.Serializable]
public class CBattleModelParams : BaseParamsWithPrefabAndData<CBattleModel> { }

public class CBattleViewHolder :BaseItemViewsHolder
{
    private CUIBattleHeroCell _cUIBattleHeroCell;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUIBattleHeroCell = root.GetComponent<CUIBattleHeroCell>();
    }

    public void UpdateView(CBattleModel cModel)
    {
        _cUIBattleHeroCell.SetData(cModel);
    }
}