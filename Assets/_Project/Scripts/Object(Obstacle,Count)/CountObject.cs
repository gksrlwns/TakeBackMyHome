using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public enum CountType { Add, Multiply };

public class CountObject : MonoBehaviour
{
    public CountType countType;
    public int value;
    [SerializeField] TMP_Text valueText;

    CountObjcetController countObjcetController;
    BoxCollider boxCollider;
    StringBuilder stringBuilder;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        stringBuilder = new StringBuilder();
    }


    private void OnEnable()
    {
        stringBuilder.Clear();

        switch (countType)
        {
            case CountType.Add:
                stringBuilder.Append("* ");
                break;
            case CountType.Multiply:
                stringBuilder.Append("+ ");
                break;
        }

        stringBuilder.Append(value);
        valueText.text = stringBuilder.ToString();
    }

    public void InitiailizeComponent(CountObjcetController _countObjcetController)
    {
        countObjcetController = _countObjcetController;
    }
    public void InitiailizeValue(int _value)
    {
        value = _value;
        countType = SetCountType(value);
    }

    public int CalculateCount(int soldierCount)
    {
        countObjcetController.SetCollider(false);
        int temp = soldierCount;

        switch(countType)
        {
            case CountType.Add: 
                temp += value; 
                break;
            case CountType.Multiply:
                temp *= value;
                break;
        }
        
        return temp - soldierCount;
    }

    public void SetTrigger(bool isTrig) => boxCollider.enabled = isTrig;

    CountType SetCountType(int value)
    {
        if (value > 4) return CountType.Add;
        else return CountType.Multiply;
    }

}
