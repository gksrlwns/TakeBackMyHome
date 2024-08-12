using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CountType { Add, Multiply };

public class CountObject : MonoBehaviour
{
    public CountType countType;
    public int value;
    [SerializeField] TMP_Text valueText;

    BoxCollider boxCollider;

    private void Awake() => boxCollider = GetComponent<BoxCollider>();

    private void OnEnable()
    {
        valueText.text = value.ToString();
    }

    public int CalculateCount(int soldierCount)
    {
        int temp = soldierCount;
        if (value < 4) temp *= value;
        else temp += value;
        
        return temp - soldierCount;
    }

    public void OffTrigger() => boxCollider.enabled = false;
    

}
