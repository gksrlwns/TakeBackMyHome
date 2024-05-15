using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CountObject : MonoBehaviour
{
    public int value;
    [SerializeField]
    private TMP_Text valueText;
    
    public CountType countType;

    private void OnEnable()
    {
        valueText.text = value.ToString();
    }

    public int CalculateCount(int soldierCount)
    {
        int temp = soldierCount;
        if (value < 10) temp *= value;
        else temp += value;
        
        return temp - soldierCount;
    }

}
