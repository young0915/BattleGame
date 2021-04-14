using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CMageTower : CTowerUnit
{
    [Header("CMageTower")]
    [SerializeField] private CLaser ins_cLaser = null;

    protected override void Awake()
    {

        cTowerInfo = new CTowerInfo(
            CDBPlayerInfo.Inst.m_listTowerInfo[3].m_nId,
            CDBPlayerInfo.Inst.m_listTowerInfo[3].m_eTowerType,
            CDBPlayerInfo.Inst.m_listTowerInfo[3].m_nLv,
            CDBPlayerInfo.Inst.m_listTowerInfo[3].m_strName,
            CDBPlayerInfo.Inst.m_listTowerInfo[3].m_nMoney,
            CDBPlayerInfo.Inst.m_listTowerInfo[3].m_nAttack,
            CDBPlayerInfo.Inst.m_listTowerInfo[3].m_strProfile);
  
    }

    protected override void Mage()
    {
        base.Mage();

        GameObject Lazer = Instantiate(ins_objWeapon, TraTarget.gameObject.transform);
        ins_cLaser = Lazer.GetComponent<CLaser>();

        if (ins_cLaser != null)
        {
            ins_cLaser.Lazer(TraTarget, cTowerInfo.m_nAttack);
        }
    
    }

}
