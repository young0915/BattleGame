using UnityEngine;
using System.Collections;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;

public class CUIStoreScrollView : OSA<CStoreModelParams, CStoreViewHolder>
{
    #region OSA Implementation

    protected override CStoreViewHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CStoreViewHolder();
        instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
        return instance;
    }

    protected override void UpdateViewsHolder(CStoreViewHolder newOrRecycled)
    {
        newOrRecycled.UpdateView(_Params.Data[newOrRecycled.ItemIndex]);
    }

    #endregion


    public void SetMakeDataModel(EmItem eItem)
    {
        _Params.Data.Clear();
    

        switch (eItem)
        {
            case EmItem.Hero:
                StartCoroutine(CorHeroInstall());
                break;

            case EmItem.Tower:
                StartCoroutine(CorTowerInstall());

                break;

            case EmItem.Etc:
                StartCoroutine(CorJewelryInstall());

                break;

        }

    }

    public void AddTower()
    {
        if (_Params.Data.Count<=CDBPlayerInfo.Inst.m_listTowerInfo.Count)
        {
           // CStoreModel cModel = new CStoreModel()
          //  _Params.Data.Add();
        }
    }

    private IEnumerator CorHeroInstall()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < CDBManager.Inst.m_listHeroInfo.Count; i++)
        {
            CStoreModel cModel = new CStoreModel(i, EmItem.Hero);
            _Params.Data.Add(cModel);
        }
        ResetItems(_Params.Data.Count);
    }

  
    private IEnumerator CorTowerInstall()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < CDBManager.Inst.m_listTowerInfo.Count; i++)
        {
          
            if (CDBManager.Inst.m_listTowerInfo[i].m_nLv == 1)
            {
                CStoreModel cModel = new CStoreModel(i, EmItem.Tower);
                _Params.Data.Add(cModel);
            }
        }
        ResetItems(_Params.Data.Count);
    }

    private IEnumerator CorJewelryInstall()
    {
        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < CDBManager.Inst.m_listItemInfo.Count; i++)
        {
            CStoreModel cModel = new CStoreModel(i, EmItem.Etc);
            _Params.Data.Add(cModel);
        }
        ResetItems(_Params.Data.Count);
    }
}


public class CStoreModel
{
    public int m_nId;
    public EmCellState m_eCellState;
    public EmItem m_eItem;
    public CStoreModel(int nId, EmItem eItem)
    {
        this.m_nId = nId;
        this.m_eItem = eItem;
    }
}

[System.Serializable]
public class CStoreModelParams : BaseParamsWithPrefabAndData<CStoreModel> { }

public class CStoreViewHolder : BaseItemViewsHolder
{
    private CUIStoreCell _cUIStoreCell;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUIStoreCell = root.GetComponent<CUIStoreCell>();
    }

    public void UpdateView(CStoreModel cModel)
    {
        _cUIStoreCell.SetData(cModel);
    }

}


