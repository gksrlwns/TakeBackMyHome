using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : BaseHealth
{
    ZombieAnimator zombieAnimator;

    public void InitializeComponents(ZombieAnimator _zombieAnimator, CapsuleCollider _capsuleCollider)
    {
        zombieAnimator = _zombieAnimator;
        myCollider = _capsuleCollider;
    }
    public void InitializeSetUp(ZombieData zombieData)
    {
        maxHp = zombieData.MaxHp;
        curHp = maxHp;
        isDead = false;
        layer = LayerMask.NameToLayer("Zombie");
        myCollider.enabled = true;
    }
    protected override void Dead()
    {
        base.Dead();
        zombieAnimator.OnDead(isDead);
        StartCoroutine(RetunrPool());
    }
    /// <summary>
    /// 애니메이션 길이를 가져와 그 애니메이션이 끝난 후 Return하도록 구현
    /// </summary>
    IEnumerator RetunrPool()
    {
        yield return CoroutineManager.DelaySeconds(zombieAnimator.GetAnimationSeconds(ZombieAnimationName.Z_death_A));
        PoolManager.instance.ReturnObject(PoolType.Zombie, gameObject);
    }
}
