using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float launchSpeed = 5f;
    [SerializeField] float returnTime = 5f;
    float damage;
    Transform target;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnPool());
    }
    IEnumerator ReturnPool()
    {
        yield return CoroutineManager.DelaySeconds(returnTime);
        PoolManager.instance.ReturnObject(PoolType.Projectile, gameObject);
    }

    public void SetProjectile(float _damgage,Transform projectilePos, Transform _target)
    {
        damage = _damgage;
        transform.position = projectilePos.position;
        transform.rotation = projectilePos.rotation;
        target = _target;
    }

    public void LaunchProjectile()
    {
        Vector3 direction = target.position - transform.position;

        direction.Normalize();
        rigid.velocity = direction * launchSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Zombie"))
        {
            var zombie = other.GetComponent<Zombie>();
        }
    }
}
