using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHealth : BaseHealth
{
    public void Init(SoldierData soldierData)
    {
        maxHp = soldierData.MaxHp;
    }

    public override void SufferDamage(float damgage)
    {
        base.SufferDamage(damgage);

    }

    protected override void Dead()
    {
        base.Dead();
    }

}
