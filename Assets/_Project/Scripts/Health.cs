using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;

    protected virtual void OnDamaged()
    {

    }
    protected virtual void Dead()
    {

    }

}
