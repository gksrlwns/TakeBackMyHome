using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountObjcetController : ObjectData
{
    [SerializeField] CountObject[] countObjects;

    private void Awake()
    {
        countObjects = GetComponentsInChildren<CountObject>(true);
    }

    public void SetCountValue()
    {

    }
}
