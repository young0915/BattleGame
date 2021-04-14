using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArcherTower : CTowerUnit
{
    protected override void Awake()
    {

        fRange = 10.0f;                                         // 적을 감지할 수 있는 범위.


        cTowerInfo = new CTowerInfo(
        CDBPlayerInfo.Inst.m_listTowerInfo[0].m_nId,
          CDBPlayerInfo.Inst.m_listTowerInfo[0].m_eTowerType,
          CDBPlayerInfo.Inst.m_listTowerInfo[0].m_nLv,
          CDBPlayerInfo.Inst.m_listTowerInfo[0].m_strName,
          CDBPlayerInfo.Inst.m_listTowerInfo[0].m_nMoney,
          CDBPlayerInfo.Inst.m_listTowerInfo[0].m_nAttack,
          CDBPlayerInfo.Inst.m_listTowerInfo[0].m_strProfile);
    }
}
