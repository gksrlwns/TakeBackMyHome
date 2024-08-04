using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;

    public virtual void SufferDamage(float damgage)
    {
        curHp -= damgage;
        if(curHp <= 0)
        {
            Dead();
        }
    }
    protected virtual void Dead()
    {

    }

}
