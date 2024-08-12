using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountObjcetController : ObjectDataController
{
    [SerializeField] CountObject[] countObjects;

    private void Awake()
    {
        countObjects = GetComponentsInChildren<CountObject>(true);
    }

    public void SetCountValue(int value, int value2)
    {
        countObjects[0].value = value;
        countObjects[1].value = value2;
    }
}
