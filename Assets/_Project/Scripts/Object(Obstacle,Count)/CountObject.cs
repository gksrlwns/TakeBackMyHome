using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public enum CountType { Add, Multiply, Min, Div };

public class CountObject : MonoBehaviour
{
    public CountType countType;
    public int value;
    [SerializeField] TMP_Text valueText;

    [Header("Effect Controller")]
    [SerializeField] ParticleSystem mainCountEffect;
    [SerializeField] ParticleSystem secondaryCountEffect;
    [SerializeField] Color mainPositiveColor;
    [SerializeField] Color secondaryPositiveColor2;
    [SerializeField] Color mainNegativeColor;
    [SerializeField] Color secondaryNegativeColor2;

    MainModule countEffectMainModule;
    MainModule countEffect2MainModule;

    CountObjcetController countObjcetController;
    BoxCollider boxCollider;
    StringBuilder stringBuilder;
    string symbol;

    private void OnValidate()
    {
        if (mainCountEffect != null)
        {
            countEffectMainModule = mainCountEffect.main;
            mainPositiveColor = countEffectMainModule.startColor.color;

        }
        if (secondaryCountEffect != null)
        {
            countEffect2MainModule = secondaryCountEffect.main;
            secondaryPositiveColor2 = countEffect2MainModule.startColor.color;
        }
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        stringBuilder = new StringBuilder();
        
    }


    private void OnEnable()
    {
        stringBuilder.Clear();
        countEffectMainModule = mainCountEffect.main;
        countEffect2MainModule = secondaryCountEffect.main;
        switch (countType)
        {
            case CountType.Add:
                
                symbol = "+ ";
                countEffectMainModule.startColor = mainPositiveColor;
                countEffect2MainModule.startColor = secondaryPositiveColor2;
                break;
            case CountType.Multiply:
                symbol = "X ";
                countEffectMainModule.startColor = mainPositiveColor;
                countEffect2MainModule.startColor = secondaryPositiveColor2;
                break;
            case CountType.Min:
                symbol = "- ";
                countEffectMainModule.startColor = mainNegativeColor;
                countEffect2MainModule.startColor = secondaryNegativeColor2;
                break;
            case CountType.Div:
                symbol = "¡À ";
                countEffectMainModule.startColor = mainNegativeColor;
                countEffect2MainModule.startColor = secondaryNegativeColor2;
                break;
        }

        stringBuilder.Append(symbol);
        stringBuilder.Append(value);
        valueText.text = stringBuilder.ToString();
    }

    public void InitiailizeComponent(CountObjcetController _countObjcetController)
    {
        countObjcetController = _countObjcetController;
    }
    public void InitiailizeValue(int _value)
    {
        countType = SetCountType(_value);
        value = Mathf.Abs(_value);
    }

    public (int,CountType) CalculateCount()
    {
        countObjcetController.SetCollider(false);
        //int temp = soldierCount;

        //switch(countType)
        //{
        //    case CountType.Add: 
        //        temp += value;
        //        temp -= soldierCount;
        //        break;
        //    case CountType.Multiply:
        //        temp *= value;
        //        temp -= soldierCount;
        //        break;
        //    case CountType.Min:
        //        temp = value;
        //        break;
        //    case CountType.Div:
        //        temp /= value;
        //        break;
        //}
        
        return (value, countType);
    }

    public void SetTrigger(bool isTrig) => boxCollider.enabled = isTrig;

    CountType SetCountType(int value)
    {
        if (value > 4) return CountType.Add;
        else if(value > 0) return CountType.Multiply;
        else if ( value > -4) return CountType.Div;
        else return CountType.Min;
    }

}
