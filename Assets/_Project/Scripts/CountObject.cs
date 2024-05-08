using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CountType { Add, Min, Mul,Div }

public class CountObject : MonoBehaviour
{
    public int count;
    [SerializeField]
    private TMP_Text countText;
    [SerializeField]
    private CountType countType;


    private void OnEnable()
    {
        countText.text = count.ToString();
    }

}
