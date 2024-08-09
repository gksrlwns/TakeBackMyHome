using System.Collections;
using UnityEngine;

public class SoldierHealth : BaseHealth
{
    SoldierAnimator soldierAnimator;
    SoldierAttack soldierAttack;
    public void InitializeComponents(SoldierAnimator _soldierAnimator, SoldierAttack _soldierAttack, CapsuleCollider _capsuleCollider)
    {
        soldierAnimator = _soldierAnimator;
        myCollider = _capsuleCollider;
        soldierAttack = _soldierAttack;
    }
    public void InitializeSetUp(SoldierData soldierData)
    {
        maxHp = soldierData.MaxHp;
        curHp = maxHp;
        isDead = false;
        layer = LayerMask.NameToLayer("Soldier");
        myCollider.enabled = true;
    }

    protected override void Dead()
    {
        base.Dead();
        soldierAttack.GetIsDead();
        soldierAnimator.OnDead(isDead);
        StartCoroutine(RetunrPool());
    }
    /// <summary>
    /// �ִϸ��̼� ���̸� ������ �� �ִϸ��̼��� ���� �� Return�ϵ��� ����
    /// </summary>
    IEnumerator RetunrPool()
    {
        yield return CoroutineManager.DelaySeconds(soldierAnimator.GetAnimationSeconds(SoldierAnimationName.m_weapon_death_A));
        PoolManager.instance.ReturnObject(PoolType.Soldier, gameObject);
    }

}
