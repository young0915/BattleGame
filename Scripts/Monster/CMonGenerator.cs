using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Mon은 Monster.
public abstract class CMonGenerator : MonoBehaviour
{
    private List<CMonster> _listMon = new List<CMonster>();
    protected readonly string strPath = "Prefab/Monster/";


    [HideInInspector] public List<CMonster> m_listMon { get { return _listMon; } }

    public abstract IEnumerator CorCreateMon();
}

// 첫번째 씬에서는 호출되는 몬스터.
public class CMonsterFirstRound : CMonGenerator
{
    public override IEnumerator CorCreateMon()
    {
        yield return new WaitForSeconds(7.0f);

        for(int i =0; i<3; i++)
        {
            yield return new WaitForSeconds(1.0f);
            m_listMon.Add(new CFlightMon(strPath + "Beholder", new Vector3(-47.0f, 1.0f, 9.0f)));
            yield return new WaitForSeconds(1.5f);
            m_listMon.Add(new CNaturalMon(strPath + "CrabMonster", new Vector3(53.0f, 1.0f, 19.0f)));
            yield return new WaitForSeconds(2.0f);
            m_listMon.Add(new CNaturalMon(strPath + "RatAssassin", new Vector3(13.0f, 1.0f, 19.0f)));
            yield return new WaitForSeconds(1.5f);
            m_listMon.Add(new CFlightMon(strPath + "Specter", new Vector3(-47.0f, 1.0f, 9.0f)));

        }

    }
  
}

public class CMonsterSecRound : CMonGenerator
{
    public override IEnumerator CorCreateMon()
    {
        yield return new WaitForSeconds(0.2f);
        m_listMon.Add(new CNaturalMon(strPath + "Werewolf", Vector3.zero));
        m_listMon.Add(new CFlightMon(strPath + "FlyingDemon", Vector3.zero));

    }
}

