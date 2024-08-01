using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHealth : Health
{
    public void Init(SoldierData soldierData)
    {
        maxHp = soldierData.MaxHp;
    }

    protected override void OnDamaged()
    {
        base.OnDamaged();

    }

    protected override void Dead()
    {
        base.Dead();
    }

}
