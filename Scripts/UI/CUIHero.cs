using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CUIHero : CBaseUI
{
    [SerializeField] private Button ins_btnPageUp;
    [SerializeField] private Button ins_btnPageDown;
    [SerializeField] private List<CUIHeroCell> _listCUIHeroCell = new List<CUIHeroCell>();

    private int _nId = 0;
    private bool _bHeroIsOn = false;
    private bool _bClickLock = false;

    public void Initialization()
    {

        ins_btnPageDown.gameObject.SetActive(false);

        for (int i = 0; i < CDBPlayerInfo.Inst.m_listHeroInfo.Count; i++)
        {
            if (CDBPlayerInfo.Inst.m_listHeroInfo[i].m_eHeroType != EmHeroType.None)
            {
                _listCUIHeroCell[0].SetData(CDBPlayerInfo.Inst.m_listHeroInfo[0].m_nId);
                _listCUIHeroCell[1].SetData(CDBPlayerInfo.Inst.m_listHeroInfo[0].m_nId + 1);
            }
        }

    }


    private void Update()
    {

        for (int i = 0; i < CDBPlayerInfo.Inst.m_listHeroInfo.Count; i++)
        {
            if (CDBPlayerInfo.Inst.m_listHeroInfo[i].m_eHeroType != EmHeroType.None)
            {
                if (_listCUIHeroCell[1].m_eHeroType == EmHeroType.None)
                {
                    ins_btnPageUp.gameObject.SetActive(false);
                    ins_btnPageDown.gameObject.SetActive(true);
                    _listCUIHeroCell[1].gameObject.SetActive(false);
                }
                else
                {
                    if (_listCUIHeroCell[0].m_nId == 0 && _listCUIHeroCell[1].m_nId == 1)
                    {
                        ins_btnPageDown.gameObject.SetActive(false);
                        ins_btnPageUp.gameObject.SetActive(true);
                        _listCUIHeroCell[1].gameObject.SetActive(true);

                    }
                    else
                    {
                        
                        ins_btnPageDown.gameObject.SetActive(true);
                        _listCUIHeroCell[1].gameObject.SetActive(true);


                    }
                }
            
            }
        }
    }

    public void OnClickPageDown()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        _listCUIHeroCell[0].SetData(_listCUIHeroCell[0].m_nId - 2);
        _listCUIHeroCell[1].SetData(_listCUIHeroCell[1].m_nId - 2);

        _bClickLock = false;
    }


    public void OnClickPageUp()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        _listCUIHeroCell[0].SetData(_listCUIHeroCell[0].m_nId + 2);
        _listCUIHeroCell[1].SetData(_listCUIHeroCell[1].m_nId + 2);

        _bClickLock = false;
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
