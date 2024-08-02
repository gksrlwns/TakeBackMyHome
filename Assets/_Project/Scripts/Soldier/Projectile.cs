using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float damage;
    Transform target;

    public void Init(float _damgage)
    {
        damage = _damgage;
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
