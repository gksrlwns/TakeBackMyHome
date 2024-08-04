using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : BaseHealth
{
    
    ZombieAnimator zombieAnimator;
    [Header("Zombie Status Info")]
    [SerializeField] ZombieData zombieData;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float moveSpeed;

    public void Init()
    {
        maxHp = zombieData.MaxHp;
        attackRange = zombieData.AttackRange;
        attackSpeed = zombieData.AttackSpeed;
        moveSpeed = zombieData.MoveSpeed;
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
