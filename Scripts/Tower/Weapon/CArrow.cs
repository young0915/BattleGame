using UnityEngine;
using System.Collections;

public class CArrow : MonoBehaviour
{

    [SerializeField] private ParticleSystem ins_objRockSystem;

    private Transform _traTarget;
    private int _nAttack = 0;
    private float _fExplosionRadius = 2;
    private float _fSpeed = 50.0f;


    public void Seek(Transform target, int nAttack)
    {
        _traTarget = target;
        _nAttack = nAttack;
    }

    private void Update()
    {

        if (_traTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _traTarget.position - transform.position;
        float distacneThisFrame = _fSpeed * Time.deltaTime;

        if (dir.magnitude <= distacneThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distacneThisFrame, Space.World);
        transform.LookAt(_traTarget);

    }

    private void HitTarget()
    {

        ins_objRockSystem.gameObject.SetActive(true);
        ins_objRockSystem.Play();

        if (_fExplosionRadius > 0.0f)
        {
            Explode();
        }
        else
        {
            Damage(_traTarget);
        }

        Destroy(gameObject, 2f);
    }

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
