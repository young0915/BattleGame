using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRecoveryHero : CHeroUnit
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
        if (CUIManager.Inst.m_cUIBattle.m_listHeroSlot[2].m_ImgCoolTime.fillAmount != 0)
            return;

        CUIManager.Inst.m_cUIBattle.m_listHeroSlot[2].OnClickHeroSkill(3);

        ins_cCharacterAni.SetAction(EmCharacterAction.Attack);
        SetEffectSkill(EmHeroType.Recovery);

        int nCritical = UnityEngine.Random.Range(0, cHeroInfo.m_nCritical);
        nSkill = cHeroInfo.m_nAttack + nCritical;
       

        // Hp 충전.
        for (int i = 0; i < CUIManager.Inst.m_cUIBattle.m_listHeroSlot.Count; i++)
        {
            // hp가 maxValue 값이 아닌 히어로만 충전한다.
            if (CUIManager.Inst.m_cUIBattle.m_listHeroSlot[i].m_HpSlider.value != CUIManager.Inst.m_cUIBattle.m_listHeroSlot[i].m_HpSlider.maxValue)
            {
                TatkeHp(nSkill, EmCharacterAction.Skill);
            }
        }

    }



}



