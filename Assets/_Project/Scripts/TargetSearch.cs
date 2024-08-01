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

            //OverlapSphereNonAlloc�� ����Ͽ� �޸� �Ҵ� ���� �� �迭�� �����Ͽ� �޸� ����� ȿ����
            int count = Physics.OverlapSphereNonAlloc(transform.position, searchRange, enemiesBuffer, targetLayer);

            //�̸� �Ҵ�� �迭�� ũ�⺸�� Ŀ���� �迭�� ũ�� ����
            if (enemiesBuffer.Length < count) enemiesBuffer = new Collider[count];

            //���� ����Ǵ� ũ��� ���� �迭���ٴ� ���� �迭�� List�� ���
            for (int i = 0; i < count; i++) enemies.Add(enemiesBuffer[i]);

            Debug.Log("Ž��...");
            yield return CoroutineManager.DelaySeconds(1f);
        }
    }

    public Transform NearTarget(Transform myPos)
    {
        if (enemies.Count == 0) return null;
        Transform nearTarget = null;
        Debug.Log("���� ����� �� Ž��..");
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < enemies.Count; i++)
        {
            float distanceToTarget = (myPos.position - enemies[i].transform.position).sqrMagnitude;
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearTarget = enemies[i].transform;
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
