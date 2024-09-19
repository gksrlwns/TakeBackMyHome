using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfoMaker : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] bool isTrig;
    private void OnValidate()
    {
        if (isTrig)
        {
            buttons = FindObjectsOfType<Button>(true);
            foreach(Button button in buttons)
            {
                if(button.gameObject.GetComponent<ButtonInfo>() == null)
                {
                    button.gameObject.AddComponent<ButtonInfo>();
                }
            }
            isTrig = false;
        } 
        
        
    }
}
