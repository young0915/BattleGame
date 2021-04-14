using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class CTowerUnit : MonoBehaviour
{
    [SerializeField] protected Transform traPartToRotate;
    [SerializeField] protected Transform ins_friePoint = null;
    [SerializeField] protected GameObject ins_objWeapon = null;

    protected CMonster cTargetMonster;

    [Header("Tower broke down")]

    protected Transform TraTarget;                                          // 몬스터 타겟.
    protected CTowerInfo cTowerInfo;                                    // 타워 정보.

    protected float fRange = 25.0f;                                         // 적을 감지할 수 있는 범위.
    protected float fFrieRate = 10.0f;                                       // 발사속도.
    protected float fFireCountdown = 0.0f;

    protected string strPrefabName = string.Empty;

    private bool _bIsUserLaser = false;

    #region [code] const Variables
    protected const string strUpdateTarget = "UpdateTarget";
    protected const float fTurnSpeed = 30.0f;                   // 회전 스피드.
    #endregion

    protected virtual void Start()
    {
        if (cTowerInfo.m_eTowerType != EmTowerType.Barracks)
        {
            InvokeRepeating(strUpdateTarget, 0.0f, 0.5f);
        }
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
            cTargetMonster = objNearsMonster.GetComponent<CMonster>();

        }
        else
        {
            TraTarget = null;
        }
    }


    protected void FixedUpdate()
    {
        LookOnTarget();
    }

    // 타겟
    protected void LookOnTarget()
    {
        if (TraTarget == null)
            return;

        //traParToRotate
        if (cTowerInfo.m_eTowerType != EmTowerType.Barracks)
        {
            Vector3 dir = TraTarget.position - transform.position;
            Quaternion lookRoatation = Quaternion.LookRotation(-dir);
            Vector3 roation = Quaternion.Lerp(traPartToRotate.rotation, lookRoatation, Time.deltaTime * fTurnSpeed).eulerAngles;
            traPartToRotate.rotation = Quaternion.Euler(0f, roation.y, 0f);

            if (_bIsUserLaser)
            {
                Laser();
            }
            else
            {
                if (fFireCountdown <= 0f)
                {
                    if (cTowerInfo.m_eTowerType == EmTowerType.Mage)
                    {
                        Mage();
                    }
                    else
                    {
                        Shoot();
                    }
                    fFireCountdown = 1.0f / fFrieRate;
                }

                fFireCountdown -= Time.deltaTime;
            }
        }
    }


    // 타워에서 공격.
    protected void Shoot()
    {
        GameObject bullet = Instantiate(ins_objWeapon, ins_friePoint.transform.position, ins_friePoint.transform.rotation);
        CArrow cArrow = bullet.GetComponent<CArrow>();

        if (cArrow != null)
        {
            cArrow.Seek(TraTarget, cTowerInfo.m_nAttack);
        }
    }

    private void Laser()
    {
        // 몬스터 공격.
        cTargetMonster.TatkeDamage(cTowerInfo.m_nAttack, cTowerInfo.m_nAttack);

    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fRange);
    }

    protected abstract void Awake();
    protected virtual void Mage() { }

}
