using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour, IAttackable
{
    ZombieAnimator zombieAnimator;
    ZombieMovement zombieMovement;
    SoldierHealth target;
    TargetSearch targetSearch;

    float attackSpeed;
    float attackRange;
    public float damage;
    public bool isAttacking;

    public void InitializeComponents(ZombieAnimator _zombieAnimator, ZombieMovement _zombieMovement)
    {
        zombieAnimator = _zombieAnimator;
        zombieMovement = _zombieMovement;
    }
    public void InitializeSetUp(ZombieData _zombieData)
    {
        attackRange = _zombieData.AttackRange;
        attackSpeed = _zombieData.AttackSpeed;
        damage = _zombieData.Damage;
    }
    
    IEnumerator Attacking()
    {
        isAttacking = true;
        zombieAnimator.OnAttack();
        yield return CoroutineManager.DelaySeconds(attackSpeed);
        isAttacking = false;
    }

    public void AttackMotion()
    {
        if (isAttacking) return;
        StartCoroutine(Attacking());
    }
    /// <summary>
    /// 애니메이션 이벤트를 활용해 자연스럽게 Soldier에게 데미지를 입힘.
    /// </summary>
    public void Attack()
    {
        if (!target) return;
        target.SufferDamage(damage);
    }
    public void SetTarget(SoldierHealth soldier) => target = soldier;
}
