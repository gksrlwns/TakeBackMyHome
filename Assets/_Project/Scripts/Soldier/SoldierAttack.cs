using System.Collections;
using UnityEngine;

public class SoldierAttack : MonoBehaviour, IAttackable
{
    [SerializeField] float attackSpeed;
    [SerializeField] float damage;

    [Header("Projectile Info")]
    [SerializeField] Transform projectilePos;
    [SerializeField] Transform target;
    
    TargetSearch targetSearch;
    SoldierAnimator soldierAnimator;
    SoldierMovement soldierMovement;
    SoldierHealth soldierHealth;

    public void InitializeComponents(SoldierAnimator _soldierAnimator,SoldierHealth _soldierHealth , SoldierMovement _soldierMovement)
    {
        soldierMovement = _soldierMovement;
        soldierAnimator = _soldierAnimator;
        soldierHealth = _soldierHealth;
    }

    public void InitializeSetUp(SoldierData soldierData)
    {
        attackSpeed = soldierData.AttackSpeed + (DataBaseManager.Instance.playerData.soldierStatus.attackSpeed * 0.05f);
        damage = soldierData.Damage + (DataBaseManager.Instance.playerData.soldierStatus.damage * 0.1f);
    }

    public IEnumerator AttackLoop()
    {
        while (true)
        {
            target = targetSearch.NearTarget(transform);
            soldierMovement.SetTarget(target);
            if (target != null) Attack();

            yield return CoroutineManager.DelaySeconds(attackSpeed);
        }
    }
    public void Attack()
    {
        if (soldierHealth.isDead) return;
        soldierAnimator.OnAttack();
        Debug.Log($"{this.name} АјАн");
        var projectile = PoolManager.instance.GetPool<Projectile>(PoolType.Projectile);
        projectile.SetProjectile(damage, projectilePos, target);
        projectile.LaunchProjectile();
    }
    public void GetTargetSearch(TargetSearch _targetSearch) => targetSearch = _targetSearch;

    public void GetIsDead()
    {
        StopAllCoroutines();
    }
}
