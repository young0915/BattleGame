using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPenetrationHero : CHeroUnit
{

    public override void Awake()
    {
        cHeroInfo = new CHeroInfo(
     CDBPlayerInfo.Inst.m_listHeroInfo[nId].m_nLv,
     CDBPlayerInfo.Inst.m_listHeroInfo[nId].m_eHeroType,
     CDBPlayerInfo.Inst.m_listHeroInfo[nId].m_nHp,
     CDBPlayerInfo.Inst.m_listHeroInfo[nId].m_nAttack,
     CDBPlayerInfo.Inst.m_listHeroInfo[nId].m_nCritical
     );
    }

    public override void OnMouseUp()
    {
        // 지속적으로 스킬 사용하지 못하도록 방지.
        if (CUIManager.Inst.m_cUIBattle.m_listHeroSlot[1].m_ImgCoolTime.fillAmount != 0)
            return;

        CUIManager.Inst.m_cUIBattle.m_listHeroSlot[1].OnClickHeroSkill(2);

        int nCritical = UnityEngine.Random.Range(0, cHeroInfo.m_nCritical);
        nSkill = cHeroInfo.m_nAttack + nCritical;

        if(cTargetMonster != null)
        {
            cTargetMonster.TatkeDamage(nSkill);
        }

        ins_cCharacterAni.SetAction(EmCharacterAction.Skill);
        SetEffectSkill(EmHeroType.Penetration);
    }


}
