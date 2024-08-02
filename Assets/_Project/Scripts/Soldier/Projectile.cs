using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float launchSpeed = 1f;
    float damage;
    Transform target;

    public void SetProjectile(float _damgage,Vector3 projectilePos, Transform _target)
    {
        damage = _damgage;
        transform.position = projectilePos;
        target = _target;
    }

    public void LaunchProjectile()
    {
        Vector3 direction = target.position - transform.position;

        direction.Normalize();
        transform.Translate(direction * launchSpeed * Time.deltaTime);
    }

}
