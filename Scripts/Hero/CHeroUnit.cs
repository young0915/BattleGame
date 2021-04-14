using UnityEngine;
using System.Collections;

public abstract class CHeroUnit : MonoBehaviour
{
    [SerializeField] protected Transform TraTarget;
    [SerializeField] protected CCharacterAni ins_cCharacterAni;
    protected CMonster cTargetMonster = null;

    protected CHeroInfo cHeroInfo = null;
    protected float fRange = 15.0f;                                     // 공격 범위.
    protected int nId = 0;
    protected int nSkill = 0;
    protected EmCharacterAction _eHeroAction = EmCharacterAction.Idle;
    private EmHeroType _emHeroType = EmHeroType.None;



    #region [code] const Variable
    private const string _strUpdateTarget = "UpdateTarget";
    private const string _strEffect = "Prefab/Effect/CFX3_MagicAura_B_Runic";
    private const string _strHillEffect = "Prefab/Effect/CFX3_MagicHill";
    #endregion


    protected void Start()
    {
        InvokeRepeating(_strUpdateTarget, 0.0f, 0.5f);
    }

    protected void UpdateTarget()
    {
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        float fShortDistan = Mathf.Infinity;
        GameObject objNearsMonster = null;
        foreach (GameObject Mon in Monsters)
        {
            float DistToMon = Vector3.Distance(transform.position, Mon.transform.position);
            if (DistToMon < fShortDistan)
            {
                fShortDistan = DistToMon;
                objNearsMonster = Mon;
            }
        }

        if (objNearsMonster != null && fShortDistan <= fRange)
        {
            TraTarget = objNearsMonster.transform;

            // cTargetMonster 삽입.
            cTargetMonster = objNearsMonster.GetComponent<CMonster>();

            // 몬스터가 플레이어 공격.
            TatkeHp(cTargetMonster.m_cMonInfo.m_nAttack);
            // 플레이어가 몬스터 공격.
            cTargetMonster.TatkeDamage(cHeroInfo.m_nAttack);

        }
        else
        {
            TraTarget = null;
        }

        // 타이머 오버시 게임 오버.
        if (CUIManager.Inst.m_cUIBattle.GetTimerCheck() <= 0.0f)
        {
            ins_cCharacterAni.SetAction(EmCharacterAction.Die);
        }


    }

    protected void SetEffectSkill(EmHeroType eHeroType)
    {
        ParticleSystem SkillParticle = new ParticleSystem();
        SkillParticle = CResourceLoader.Load<ParticleSystem>(_strEffect);
        Instantiate(SkillParticle, gameObject.transform);
    }


    protected void TatkeHp(int amount, EmCharacterAction eCharacterAction = EmCharacterAction.Attack)
    {
        if(eCharacterAction == EmCharacterAction.Attack)
        {
            cHeroInfo.m_nHp -= amount;
        }
        else
        {
            cHeroInfo.m_nHp += amount;
        }

        int nHeroId = 0;
        nHeroId = ((int)cHeroInfo.m_eHeroType - 1);
        CUIManager.Inst.m_cUIBattle.m_listHeroSlot[nHeroId].m_HpSlider.value
                = cHeroInfo.m_nHp;



        if (cHeroInfo.m_nHp <= 0)
        {
            ins_cCharacterAni.SetAction(EmCharacterAction.Die);
        }
        else
        {
            if (cTargetMonster.m_cMonInfo.m_nAttack > cHeroInfo.m_nAttack)
            {
                ins_cCharacterAni.SetAction(EmCharacterAction.GetHit);
            }
            else
            {
                ins_cCharacterAni.SetAction(EmCharacterAction.Attack);
            }

        }
        return;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fRange);
    }

    public abstract void Awake();
    public abstract void OnMouseUp();

}
