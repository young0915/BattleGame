using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CUITowerBattle : CBaseUI
{

    [SerializeField] private List<Button> ins_listBtnTower = new List<Button>();
    public List<Button> m_listBtnTower { get { return ins_listBtnTower; } }
    [SerializeField] private Button ins_btnrepair;
    public Button m_btnrepair { get { return ins_btnrepair; } }

    private EmTowerType _eTowerType = EmTowerType.None;
    public EmTowerType m_eTowerType { get { return _eTowerType; } }

   private bool _bClickLock = false;
    private bool _bTowerPosY = false;
    private Transform _traPos;

    #region [code] Tower Contain.
    private const string _strPath = "Prefab/Tower/";
    #endregion

    public void Initialization(Transform TraPos, bool bTowerPosY)
    {
        this._traPos = TraPos;
        this._bTowerPosY = bTowerPosY;


        for (int i = 0; i < CDBPlayerInfo.Inst.m_listTowerInfo.Count; i++)
        {
            ins_listBtnTower[i].gameObject.SetActive(false);
            if (CDBPlayerInfo.Inst.m_listTowerInfo[i].m_eTowerType != EmTowerType.None)
            {
                ins_listBtnTower[i].gameObject.SetActive(true);
            }
        }

        // 타워 지형이 높은 곳이라면(약 Pos Y 20이상이면).
        ins_listBtnTower[1].gameObject.SetActive(_bTowerPosY);
    }


    public void OnClickTower(int nIdx)
    {
        if (_bClickLock)
            return;

        EmTowerType eTower = (EmTowerType)nIdx;

        if (_eTowerType == eTower)
            return;

        SetTower(eTower);
    }

    
    private void SetTower(EmTowerType eTowerType)
    {
        this._eTowerType = eTowerType;
   
        switch (_eTowerType)
        {
            case EmTowerType.Archer:
                CTowerUnit cTowerArcher = new CArcherTower();
                cTowerArcher = CResourceLoader.Load<CArcherTower>(_strPath + CDBPlayerInfo.Inst.m_listTowerInfo[0].m_strProfile);
                Instantiate(cTowerArcher, _traPos);

                break;

            case EmTowerType.Barracks:
                CTowerUnit cTowerBarracks = new CBarracksTower();
                 cTowerBarracks = CResourceLoader.Load<CBarracksTower>(_strPath + CDBPlayerInfo.Inst.m_listTowerInfo[1].m_strProfile);
                Instantiate(cTowerBarracks, _traPos);

                break;

            case EmTowerType.Cannon:
                CTowerUnit cTowerCannon = new CCannonTower();
                cTowerCannon = CResourceLoader.Load<CCannonTower>(_strPath + CDBPlayerInfo.Inst.m_listTowerInfo[2].m_strProfile);
                Instantiate(cTowerCannon, _traPos);

                break;

            case EmTowerType.Mage:
                CTowerUnit cTowerMage = new CMageTower();
                cTowerMage = CResourceLoader.Load<CMageTower>(_strPath + CDBPlayerInfo.Inst.m_listTowerInfo[3].m_strProfile);
                Instantiate(cTowerMage, _traPos);

                break;
        }

    }

    public void OnClickRepair()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        Debug.Log("수리중");

        _bClickLock = false;
    }


    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestory = true)
    {
        base.Close(bDestory);
//        _eTowerType = EmTowerType.None;
    }


}
