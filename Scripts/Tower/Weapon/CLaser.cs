using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLaser : MonoBehaviour
{
    private Transform _traTarget;
    private int _nAttack = 0;
    private float _fExplosionRadius = 2;
    private float _fSpeed = 50.0f;


    public void Lazer(Transform target, int nAttack)
    {
        this._traTarget = target;
        this._nAttack = nAttack;
        Explode();
    }
  

    //private void HitTarget()
    //{
    //    if (_fExplosionRadius > 0.0f)
    //    {
    //    }
    //    else
    //    {
    //        Damage(_traTarget);
    //    }
    //    Destroy(gameObject, 2f);
    //}

    private void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _fExplosionRadius);

        foreach (Collider col in cols)
        {
            if (col.CompareTag("Monster"))
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _fExplosionRadius);
    }


}
