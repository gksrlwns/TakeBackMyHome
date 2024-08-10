using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadType : MonoBehaviour
{
    public string headName;

    private void OnValidate()
    {
        if (headName != this.gameObject.name) headName = this.gameObject.name;
    }
}
