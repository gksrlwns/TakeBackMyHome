using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    [Header("Projectile Info")]
    [SerializeField] Transform projectilePos;
    
    float attackSpeed;
    float damage;

    [SerializeField] Transform target;

    TargetSearch targetSearch;
    SoldierAnimator soldierAnimator;
    SoldierMovement soldierMovement;

    public void Init(SoldierAnimator _soldierAnimator, SoldierData soldierData, SoldierMovement _soldierMovement)
    {
        soldierMovement = _soldierMovement;
        soldierAnimator = _soldierAnimator;
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

    void Attack()
    {
        soldierAnimator.OnAttack();
        Debug.Log($"{this.name} АјАн");
        var projectile = PoolManager.instance.GetPool<Projectile>(PoolType.Projectile);
        projectile.SetProjectile(damage, projectilePos, target);
        projectile.LaunchProjectile();
        
    }


    public void GetTargetSearch(TargetSearch _targetSearch)
    {
        targetSearch = _targetSearch;
    }

}
