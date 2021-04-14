using UnityEngine;
using System.Collections.Generic;

public class CHeroFactoryUnit : MonoBehaviour
{
    private const string _strPrefab = "Prefab/Heros/BattleHero/";

    private string _strHeroPrefab = string.Empty;

    private CHeroUnit _unit = null;

    public GameObject CreateUnit(EmHeroType eHeroType, int nId, Vector3 traPos)
    {
        GameObject objUnit = null;

        //CHeroUnit unit = null;
        _strHeroPrefab = _strPrefab + CDBPlayerInfo.Inst.m_listHeroInfo[nId].m_strPrefabPath.ToString();

        switch (eHeroType)
        {
            case EmHeroType.Striker:
                _unit = new CStrikerHero();
                _unit = CResourceLoader.Load<CStrikerHero>(_strHeroPrefab);
                CStrikerHero objstriker = Instantiate(_unit) as CStrikerHero;
                objstriker.transform.localPosition = traPos;
                break;

            case EmHeroType.Penetration:
                _unit = new CPenetrationHero();
                _unit = CResourceLoader.Load<CPenetrationHero>(_strHeroPrefab);
                CHeroUnit objPenetration = Instantiate(_unit) as CPenetrationHero;
                objPenetration.transform.localPosition = traPos;
                break;

            case EmHeroType.Recovery:
                _unit = new CRecoveryHero();
                _unit = CResourceLoader.Load<CRecoveryHero>(_strHeroPrefab);
                CHeroUnit objRecovery = Instantiate(_unit) as CRecoveryHero;
                objRecovery.transform.localPosition = traPos;

                break;

        }

        return objUnit;
    }

}
