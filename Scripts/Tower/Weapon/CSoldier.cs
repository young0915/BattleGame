using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldier : MonoBehaviour
{

    [SerializeField] private Transform _traTarget;

    [SerializeField] private Animator ins_Ani;


    private int _nAttack = 0;
    private float _fExplosionRadius = 2;
    private string _strAniAttack = "bIsAttack";
    private bool _bIsAttack = false;

    public void Tracking(Transform target, int nAttack)
    {
        _traTarget = target;
        _nAttack = nAttack;
        StartCoroutine(CorAttack(true));
        HitTarget();
    }

    private IEnumerator CorAttack(bool bIsAttack)
    {
        this._bIsAttack = bIsAttack;

      

        yield return new WaitForSeconds(0.5f);

        while (_bIsAttack)
        {
            ins_Ani.SetBool(_strAniAttack, _bIsAttack);

            yield break;
        }
    }


    private void HitTarget()
    {

        if(_fExplosionRadius >0.0f)
        {
            Explode();
        }
        else
        {
            Damage(_traTarget);
        }
    }


    private void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _fExplosionRadius);

        foreach (Collider col in cols)
        {
            if (col.tag == "Monster")
            {
                Damage(col.transform);
            }
        }
    }

    private void Damage(Transform Monster)
    {
        CMonster cMon = Monster.GetComponent<CMonster>();

        if (cMon != null)
        {
            cMon.TatkeDamage(_nAttack);
        }
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _fExplosionRadius);
    }

}
