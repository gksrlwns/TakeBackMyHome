using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

public class BladeMove : MonoBehaviour
{
    WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(1.5f);

    private void Start()
    {
        StartCoroutine(Rotatoin());
    }
    IEnumerator Rotatoin()
    {

        transform.DORotate(transform.position,3);
        yield return waitForSeconds;

        StartCoroutine(Rotatoin());
    }
}
