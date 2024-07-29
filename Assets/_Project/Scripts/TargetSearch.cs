using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TargetSearch : MonoBehaviour
{
    [Header("TargetSearch Info")]
    [SerializeField] float searchRange = 10;
    public Collider[] colliders;
    [SerializeField] LayerMask targetLayer;
    public GameObject target;

    Coroutine searchCoroutine;

    private void OnEnable()
    {
        searchCoroutine = StartCoroutine(SurroundTargetSearch());
    }

    private void OnDisable()
    {
        if(searchCoroutine != null) StopCoroutine(searchCoroutine);
    }

    IEnumerator SurroundTargetSearch()
    {
        while(true)
        {
            colliders = Physics.OverlapSphere(transform.position, searchRange, targetLayer);
            yield return null;
        }
    }

    public Transform NearTarget()
    {
        Transform nearTarget = null;
        float shortestDistance = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            float distanceToTarget = Vector2.Distance(transform.position, colliders[i].transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearTarget = colliders[i].transform;
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
