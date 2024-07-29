using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TargetSearch : MonoBehaviour
{
    [Header("TargetSearch Info")]
    [SerializeField] float searchRange = 5;
    //public RaycastHit2D[] hits;
    [SerializeField] Collider2D[] colliders;
    [SerializeField] LayerMask targetLayer;
    public GameObject target;

    private void Update()
    {
        //hits = Physics2D.CircleCastAll(transform.position, searchRange, Vector2.zero, 0, targetLayer);
        colliders = Physics2D.OverlapCircleAll(transform.position, searchRange, LayerMask.NameToLayer("Zombie"));
        if (colliders.Length != 0) target = NearTarget();
        else target = null;
    }

    GameObject NearTarget()
    {
        GameObject nearTarget = null;
        float shortestDistance = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            float distanceToTarget = Vector2.Distance(transform.position, colliders[i].transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearTarget = colliders[i].gameObject;
            }
        }

        if (nearTarget && shortestDistance <= searchRange)
        {
            return nearTarget;
        }
        else
        {
            return null;
        }
    }
    
    //Transform NearTarget()
    //{
    //    Transform target = null;
    //    float shortestDistance = Mathf.Infinity;
    //    for(int i = 0; i < hits.Length; i++)
    //    {
    //        float distanceToMonster = Vector2.Distance(transform.position, hits[i].transform.position);
    //        if(distanceToMonster < shortestDistance)
    //        {
    //            shortestDistance = distanceToMonster;
    //            target = hits[i].transform;
    //        }
    //    }
    //    return target;
    //}


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}
