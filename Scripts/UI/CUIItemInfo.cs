using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIItemInfo : CBaseUI
{
    [SerializeField] private Text ins_txtItemInfo;

    private int _nId;                               // int형 변수로 이용하여 아이템 정보 출력하기.
    private string _strItemInfo = string.Empty;

    public void Initialization(Transform traPos, int nId)
    {
        SetTextInfo();
        this._nId = nId;
        this.gameObject.transform.position = traPos.position;


        _strItemInfo = CDBManager.Inst.m_listItemInfo[_nId].m_strContent;



        ins_txtItemInfo.text = string.Format("{0}", _strItemInfo);
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
