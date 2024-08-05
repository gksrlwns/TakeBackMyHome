using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearch : MonoBehaviour
{
    [Header("TargetSearch Info")]
    [SerializeField] float searchRange = 10;
    
    [SerializeField] LayerMask targetLayer;
    public List<Collider> enemies = new List<Collider>();
    Collider[] enemiesBuffer = new Collider[100];

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
            enemies.Clear();

            //OverlapSphereNonAlloc을 사용하여 메모리 할당 감소 및 배열을 재사용하여 메모리 사용이 효율적
            int count = Physics.OverlapSphereNonAlloc(transform.position, searchRange, enemiesBuffer, targetLayer);

            //미리 할당된 배열의 크기보다 커지면 배열의 크기 수정
            if (enemiesBuffer.Length < count) enemiesBuffer = new Collider[count];

            //자주 변경되는 크기로 인해 배열보다는 동적 배열인 List를 사용
            for (int i = 0; i < count; i++) enemies.Add(enemiesBuffer[i]);

            Debug.Log("탐색...");
            yield return CoroutineManager.DelaySeconds(1f);
        }
    }
    /// <summary>
    /// Transform이 아닌 Component를 가져와야 하는 경우 사용
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="myPos"></param>
    /// <returns></returns>
    public T NearTarget<T>(Transform myPos) where T : Component
    {
        Transform nearTarget = NearTarget(myPos);
        if(nearTarget == null) return null;
        return nearTarget.GetComponent<T>();
    }

    public Transform NearTarget(Transform myPos)
    {
        if (enemies.Count == 0) return null;
        Transform nearTarget = null;
        Debug.Log("가장 가까운 적 탐색..");
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!CheckExistEnemyCollider(enemies[i])) continue;
            float distanceToTarget = (myPos.position - enemies[i].transform.position).sqrMagnitude;
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearTarget = enemies[i].transform;
            }
        }

        return nearTarget;
    }

    /// <summary>
    /// MissingReferenceException 에러 해결을 위한 함수
    /// Collider가 파괴되었음에도 참조하려고 하여 발생하는 에러
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    bool CheckExistEnemyCollider(Collider collider)
    {
        if (collider == null || collider.gameObject == null) return false;
        else return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}
