using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    [Header("Projectile Info")]
    [SerializeField] Transform projectilePos;
    [SerializeField] GameObject projectilePrefab;
    
    float attackSpeed;

    Transform target;

    TargetSearch targetSearch;
    SoldierAnimator soldierAnimator;
    SoldierMovement soldierMovement;


    public void Init(SoldierAnimator _soldierAnimator, SoldierData soldierData, SoldierMovement _soldierMovement)
    {
        soldierMovement = _soldierMovement;
        soldierAnimator = _soldierAnimator;
        attackSpeed = soldierData.AttackSpeed;
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
        Debug.Log($"{this.name} ����");
    }
    
    public void GetTargetSearch(TargetSearch _targetSearch)
    {
        targetSearch = _targetSearch;
    }

}
