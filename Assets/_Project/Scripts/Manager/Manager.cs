using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Manager: MonoBehaviour
{
    static Manager instance;

    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<Manager>();
                go.name = instance.GetType().ToString();
            }
            return instance;
        }
    }

    protected void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }
}
