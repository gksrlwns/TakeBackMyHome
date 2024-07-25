using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] GameObject finishLinePrefab;

    void Start()
    {
        FinishObjcet finishObjcet = Instantiate(finishLinePrefab, transform).GetComponent<FinishObjcet>();
        finishObjcet.SetPosition(2);
        GameManager.instance.player.finishObjcet = finishObjcet;
    }

    
}
