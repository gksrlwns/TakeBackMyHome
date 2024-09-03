using System.Collections;
using UnityEngine;

public class SoldierHealth : BaseHealth
{
    Player player;
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
        maxHp = soldierData.MaxHp + (DataBaseManager.Instance.playerData.soldierStatus.maxHp * 0.1f);
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
        player.soldierCount--;
        StartCoroutine(RetunrPool());
    }
    /// <summary>
    /// 애니메이션 길이를 가져와 그 애니메이션이 끝난 후 Return하도록 구현
    /// </summary>
    IEnumerator RetunrPool()
    {
        yield return CoroutineManager.DelaySeconds(soldierAnimator.GetAnimationSeconds(SoldierAnimationName.m_weapon_death_A));
        PoolManager.instance.ReturnObject(PoolType.Soldier, gameObject);
    }

    public void GetPlayer(Player _player)
    {
        player = _player;
    }
}
