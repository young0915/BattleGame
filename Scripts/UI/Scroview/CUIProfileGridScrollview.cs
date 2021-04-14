using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.DataHelpers;
using Com.TheFallenGames.OSA.CustomAdapters.GridView;

public class CUIProfileGridScrollview : GridAdapter<GridParams, CProfileViewHolder>
{
    public SimpleDataHelper<CProfileGridModel> Data { get; private set; }

    protected override void Start()
    {
        Data = new SimpleDataHelper<CProfileGridModel>(this);

        base.Start();

        StartCoroutine(CorMakeData(CDBManager.Inst.m_listHeroInfo.Count));

    }

    protected override void UpdateCellViewsHolder(CProfileViewHolder viewsHolder)
    {
        CProfileGridModel model = Data[viewsHolder.ItemIndex];

        viewsHolder.UpdateView(model);

    }



    private IEnumerator CorMakeData(int nCnt)
    {
        yield return new WaitForSeconds(.5f);

        var newItems = new CProfileGridModel[nCnt];

        for(int i = 0; i< nCnt; i++)
        {
            var cModel = new CProfileGridModel(i);
            newItems[i] = cModel;
        }

        OnDataRetrieved(newItems);
    }

    private void OnDataRetrieved(CProfileGridModel[] newItems)
    {
        Data.InsertItemsAtEnd(newItems);

        Data.NotifyListChangedExternally();
    }

}


public class CProfileGridModel
{

    public int m_nId;

    public CProfileGridModel(int nId)
    {
        this.m_nId = nId;
    }
}

public class CProfileViewHolder : CellViewsHolder
{

    private CUIProfileCell _cUIProfile;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUIProfile = root.GetComponent<CUIProfileCell>();
    }

    public void UpdateView(CProfileGridModel cModel)
    {
        _cUIProfile.SetData(cModel);
    }
}