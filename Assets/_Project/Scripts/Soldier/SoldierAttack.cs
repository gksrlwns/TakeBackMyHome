using System.Collections;
using UnityEngine;

public class SoldierAttack : MonoBehaviour, IAttackable
{
    [Header("Projectile Info")]
    [SerializeField] Transform projectilePos;
    [SerializeField] Transform target;
    
    TargetSearch targetSearch;
    SoldierAnimator soldierAnimator;
    SoldierMovement soldierMovement;

    float attackSpeed;
    float damage;

    public void InitializeComponents(SoldierAnimator _soldierAnimator, SoldierMovement _soldierMovement)
    {
        soldierMovement = _soldierMovement;
        soldierAnimator = _soldierAnimator;
    }

    public void InitializeSetUp(SoldierData soldierData)
    {
        attackSpeed = soldierData.AttackSpeed;
        damage = soldierData.Damage;
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
        soldierAnimator.OnAttack();
        Debug.Log($"{this.name} АјАн");
        var projectile = PoolManager.instance.GetPool<Projectile>(PoolType.Projectile);
        projectile.SetProjectile(damage, projectilePos, target);
        projectile.LaunchProjectile();
    }
    public void GetTargetSearch(TargetSearch _targetSearch) => targetSearch = _targetSearch;

    
}
