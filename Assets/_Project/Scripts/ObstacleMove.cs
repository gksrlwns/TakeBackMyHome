using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] ObstacleType obstacleType;
    [SerializeField] float second = 1.5f;

    private void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(second);
        transform.DOMove(new Vector3(90, 0, 0), 3);
        yield return waitForSeconds;
        StartCoroutine(Move());
    }
}
