using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUIManager : MonoBehaviour
{
    [SerializeField] MainSceneUIManager mainSceneUIManager;
    
    [SerializeField] Button damageBtn;
    [SerializeField] Button attackSpeedBtn;
    [SerializeField] Button hpBtn;
    [SerializeField] Button backBtn;

    private void Awake()
    {
        damageBtn.onClick.AddListener(() => DameageUp());
        attackSpeedBtn.onClick.AddListener(() => AttackSpeedUp());
        hpBtn.onClick.AddListener(() => HpUp());
        backBtn.onClick.AddListener(() => mainSceneUIManager.HideStoreUI());
    }

    void DameageUp()
    {

    }

    void AttackSpeedUp()
    {

    }

    void HpUp()
    {

    }

}
