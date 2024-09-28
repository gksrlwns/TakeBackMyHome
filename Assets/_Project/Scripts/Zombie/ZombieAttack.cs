using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour, IAttackable
{
    ZombieAnimator zombieAnimator;
    SoldierHealth target;

    float attackSpeed;
    float attackRange;
    public float damage;
    public bool isAttacking;

    public void InitializeComponents(ZombieAnimator _zombieAnimator) => zombieAnimator = _zombieAnimator;

    public void InitializeSetUp(ZombieStats zombieStats)
    {
        attackRange = zombieStats.attackRange;
        attackSpeed = zombieStats.attackSpeed;
        damage = zombieStats.damage;
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
    /// �ִϸ��̼� �̺�Ʈ�� Ȱ���� �ڿ������� Soldier���� �������� ����.
    /// </summary>
    public void Attack()
    {
        if (!target) return;
        target.SufferDamage(damage);
    }
    public void SetTarget(SoldierHealth soldier) => target = soldier;
}
