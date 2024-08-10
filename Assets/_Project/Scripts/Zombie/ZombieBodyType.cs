using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBodyType : MonoBehaviour
{
    public string bodyName;

    private void OnValidate()
    {
        if (bodyName != this.gameObject.name ) bodyName = this.gameObject.name;
    }
}
