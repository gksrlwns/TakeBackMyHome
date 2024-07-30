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
            Debug.Log("Å½»ö...");
            yield return CoroutineManager.DelaySeconds(2f);
        }
    }

    public Transform NearTarget()
    {
        if (colliders.Length == 0) return null;
        Transform nearTarget = null;
        Debug.Log("°¡Àå °¡±î¿î Àû Å½»ö..");
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
        

        return nearTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}
