using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class CMonster : MonoBehaviour
{
    [SerializeField] protected Slider ins_HpSlider;
    [SerializeField] protected CCharacterAni ins_cCharacterAni;
   [SerializeField] protected EmMonPos eMonPos;

    protected EmCharacterAction eCharacterActions;
    [SerializeField] protected CMonInfo cMonInfo;                                                    // Monster의 정보.
    public CMonInfo m_cMonInfo { get { return cMonInfo; } }
    protected Transform TraTarget;


    protected int nWavePointIdx = 0;
    protected float fRange = 15.0f;
    protected string strMonPath = string.Empty;
    protected string strMonAni = string.Empty;



    private int speed = 0;

    protected virtual void Start()
    {
        // 몬스터들의 방향.
        switch (eMonPos)
        {
            case EmMonPos.One:
                nWavePointIdx = 34;

                break;
            case EmMonPos.Two:
                nWavePointIdx = 20;

                break;
            case EmMonPos.Three:
                nWavePointIdx = 0;

                break;
        }

        TraTarget = CWaitPoints.m_traPoints[nWavePointIdx];


    }


    protected virtual void Update()
    {
        Vector3 dir = TraTarget.position - transform.position;
        transform.Translate(dir.normalized * 10 * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, TraTarget.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        if (CUIManager.Inst.m_cUIBattle.GetTimerCheck() <= 0.0f)
        {
            ins_cCharacterAni.SetAction(EmCharacterAction.Victory);
        }

    }


    protected virtual void GetNextWaypoint()
    {
        TraTarget = CWaitPoints.m_traPoints[nWavePointIdx];

        if(nWavePointIdx == 51 || nWavePointIdx ==17)
        {
            StartCoroutine(CorVictory());
        }

        nWavePointIdx++;
        if (eMonPos == EmMonPos.Two)
        {
            if (nWavePointIdx >= 32)
            {
                nWavePointIdx = 17;
            }
        }
    }

    // 도착점까지 도달하면 승리의 빅토리.
    private IEnumerator CorVictory()
    {
        ins_cCharacterAni.SetAction(EmCharacterAction.Victory);

        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);

        CUIManager.Inst.m_cUIBattle.SetHeart();
        yield break;
    }


    // 데미지 저하.
    public void TatkeDamage(int amount, int nAttack = 0)
    {
        ins_cCharacterAni.SetAction(EmCharacterAction.GetHit);

        cMonInfo.m_nHp -= amount;

        ins_HpSlider.value = cMonInfo.m_nHp;

        // 공격 당할시 속도 느리게 만들도록 처리.
        Slow(nAttack);

        if (ins_HpSlider.value <= 0)
        {
            ins_cCharacterAni.SetAction(EmCharacterAction.Die);
            StartCoroutine(CorDeath());
        }

        return;
    }


    // 공격 당할시 속도 느리게 만들도록 처리.
    private void Slow(int pct)
    {
        cMonInfo.m_nSpeed = cMonInfo.m_nSpeed * (1 - pct);
    }


    private IEnumerator CorDeath()
    {
        ins_cCharacterAni.SetAction(EmCharacterAction.Die);
        yield return new WaitForSeconds(2.0f);
        // 몬스터 객체는 비활성화로 처리.
        gameObject.SetActive(false);

        CUIManager.Inst.m_cUIBattle.SetMonCnt(1);

    }



    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fRange);
    }



    protected abstract void Awake();

}
