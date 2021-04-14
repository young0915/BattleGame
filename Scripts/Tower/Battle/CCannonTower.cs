using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCannonTower : CTowerUnit
{
    protected override void Awake()
    {

        cTowerInfo = new CTowerInfo(
            CDBPlayerInfo.Inst.m_listTowerInfo[2].m_nId,
            CDBPlayerInfo.Inst.m_listTowerInfo[2].m_eTowerType,
            CDBPlayerInfo.Inst.m_listTowerInfo[2].m_nLv,
            CDBPlayerInfo.Inst.m_listTowerInfo[2].m_strName,
            CDBPlayerInfo.Inst.m_listTowerInfo[2].m_nMoney,
            CDBPlayerInfo.Inst.m_listTowerInfo[2].m_nAttack,
            CDBPlayerInfo.Inst.m_listTowerInfo[2].m_strProfile);

    }


}
