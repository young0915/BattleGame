using UnityEngine;
using System.Collections;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;

public class CUITowerScrollView : OSA<CTowerModelParams, CTowerViewHolder>
{

    #region OSA Implementation

    protected override CTowerViewHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CTowerViewHolder();
        instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
        return instance;
    }

    protected override void UpdateViewsHolder(CTowerViewHolder newOrRecycled)
    {
        newOrRecycled.UpdateView(_Params.Data[newOrRecycled.ItemIndex]);
    }

    #endregion


    public IEnumerator CorMakeDataModel()
    {
        _Params.Data.Clear();

        yield return new WaitForSeconds(0.2f);
        for(int i =0; i <CDBPlayerInfo.Inst.m_listTowerInfo.Count; i++)
        {
            if (CDBPlayerInfo.Inst.m_listTowerInfo[i].m_eTowerType != EmTowerType.None)
            {
                CTowerModel cModel = new CTowerModel(i);
                _Params.Data.Add(cModel);
            }
        }

        ResetItems(_Params.Data.Count);
    }


}

public class CTowerModel
{
    public int m_nId;

    public CTowerModel(int nId)
    {
        this.m_nId = nId;
    }

}

[System.Serializable]
public class CTowerModelParams : BaseParamsWithPrefabAndData<CTowerModel> { }

public class CTowerViewHolder : BaseItemViewsHolder
{
    private CUITowerCell _cUITowerCell;


    public override void CollectViews()
    {
        base.CollectViews();
        _cUITowerCell = root.GetComponent<CUITowerCell>();
    }

    public void UpdateView(CTowerModel cModel)
    {
        _cUITowerCell.SetData(cModel);
    }
}