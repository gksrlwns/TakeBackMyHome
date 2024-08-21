using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountObjcetController : ObjectDataController
{
    [SerializeField] CountObject[] countObjects;

    private void Awake()
    {
        countObjects = GetComponentsInChildren<CountObject>(true);
        for (int i = 0; i < countObjects.Length; i++) countObjects[i].InitiailizeComponent(this);
    }

    public void SetCountValue(int value, int value2)
    {
        countObjects[0].InitiailizeValue(value);
        countObjects[1].InitiailizeValue(value2);
    }
    public void SetCollider(bool isTrig)
    {
        for(int i = 0; i < countObjects.Length; i++)
        {
            if (!countObjects[i].gameObject.activeSelf) continue;
            countObjects[i].SetTrigger(isTrig);
        }
    }

    
}
