using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    Button MyButton;
    public Button myButton { get => MyButton; set { MyButton = value; } }
    [SerializeField] bool ignore; //기본 UI 효과음을 사용하지 않는 버튼을 위함
    private void Awake()
    {
        MyButton = GetComponent<Button>();
        if(!ignore) MyButton.onClick.AddListener(() => AudioManager.Instance.PlaySFX(SFX.Button_Click));
    }
}
