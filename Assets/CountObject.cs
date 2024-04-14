using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CountType { Add, Min, Mul,Div }

public class CountObject : MonoBehaviour
{
    
    [SerializeField]
    private TMP_Text countText;
    [SerializeField]
    private CountType countType;
    public int count;

    private void Awake()
    {
        countText.text = count.ToString();
        switch (countType)
        {
            case CountType.Add:
                break;
            case CountType.Min:
                break;
            case CountType.Mul:
                break;
            case CountType.Div:
                break;
            default:
                break;
        }
        
    }

}
