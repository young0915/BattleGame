using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CBarracksTower : CTowerUnit
{

    [Header("CBarracksTower")]
    [SerializeField] private List<CSoldier> ins_listSolider = new List<CSoldier>();
    [Space]
    [SerializeField] private GameObject ins_objGroupSolider = null;

    protected override void Awake()
    {
        cTowerInfo = new CTowerInfo(
            CDBPlayerInfo.Inst.m_listTowerInfo[1].m_nId,
            CDBPlayerInfo.Inst.m_listTowerInfo[1].m_eTowerType,
            CDBPlayerInfo.Inst.m_listTowerInfo[1].m_nLv,
            CDBPlayerInfo.Inst.m_listTowerInfo[1].m_strName,
            CDBPlayerInfo.Inst.m_listTowerInfo[1].m_nMoney,
            CDBPlayerInfo.Inst.m_listTowerInfo[1].m_nAttack,
            CDBPlayerInfo.Inst.m_listTowerInfo[1].m_strProfile);

    }

    private const string strAssignSolider = "SetMonsterTracking";

    protected override void Start()
    {
        base.Start();

        InvokeRepeating(strAssignSolider, 0.0f, 0.5f);
    }

    private void SetMonsterTracking()
    {
        GameObject Monster = GameObject.FindGameObjectWithTag("Monster");
        float fShortDistan = Mathf.Infinity;
        GameObject objNearsMonster = null;

        if (Monster == null)
            return;

        float DistToMon = Vector3.Distance(transform.position, Monster.transform.position);

        if (DistToMon < fShortDistan)
        {
            fShortDistan = DistToMon;
            objNearsMonster = Monster;
        }

        if (objNearsMonster != null && fShortDistan <= fRange)
        {
            TraTarget = objNearsMonster.transform;
            StartCoroutine(CorSortieSoldier(true));
        }
    }

    private bool _bIsMove = false;
    private IEnumerator CorSortieSoldier(bool bIsMove)
    {
        this._bIsMove = bIsMove;

        while (_bIsMove)
        {
            Vector3 dir = TraTarget.position - ins_objGroupSolider.transform.position;
            float distanceThisFrame = 10 * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                ins_listSolider[0].Tracking(TraTarget, cTowerInfo.m_nAttack);
                ins_listSolider[1].Tracking(TraTarget, (cTowerInfo.m_nAttack-2));
                ins_listSolider[2].Tracking(TraTarget, (cTowerInfo.m_nAttack - 2));

                _bIsMove = false;
                yield break;

            }
            ins_objGroupSolider.transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }



}
