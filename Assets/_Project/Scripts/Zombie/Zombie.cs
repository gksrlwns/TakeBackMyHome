using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : BaseHealth
{
    
    ZombieAnimator zombieAnimator;
    [Header("Zombie Status Info")]
    [SerializeField] ZombieData zombieData;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float moveSpeed;

    [SerializeField] Soldier target;
    NavMeshAgent agent;
    TargetSearch targetSearch;

    private void Awake()
    {
        targetSearch = GetComponent<TargetSearch>();
        zombieAnimator = GetComponent<ZombieAnimator>();
        agent = GetComponent<NavMeshAgent>();
    }

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
