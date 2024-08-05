using System.Collections;
using UnityEngine;

public class SoldierHealth : BaseHealth
{
    SoldierAnimator soldierAnimator;

    public void InitializeComponents(SoldierAnimator _soldierAnimator,CapsuleCollider _capsuleCollider)
    {
        soldierAnimator = _soldierAnimator;
        myCollider = _capsuleCollider;
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
        isDead = true;
        soldierAnimator.OnDead(isDead);
        StartCoroutine(RetunrPool());
    }
    /// <summary>
    /// �ִϸ��̼� ���̸� ������ �� �ִϸ��̼��� ���� �� Return�ϵ��� ����
    /// </summary>
    IEnumerator RetunrPool()
    {
        yield return CoroutineManager.DelaySeconds(soldierAnimator.AnimationDeleay[SoldierAnimationName.Death]);
        PoolManager.instance.ReturnObject(PoolType.Soldier, gameObject);
    }

}
