using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUIManager : MonoBehaviour
{
    [SerializeField] MainSceneUIManager mainSceneUIManager;

    [SerializeField] TMP_Text hasCoinText;

    [Header("Damage")]
    [SerializeField] TMP_Text d_LevelText;
    [SerializeField] TMP_Text d_RequiredCoin_Text;
    [SerializeField] int d_RequiredCoin = 0;

    [Header("Attack Speed")]
    [SerializeField] TMP_Text a_LevelText;
    [SerializeField] TMP_Text a_RequiredCoin_Text;
    [SerializeField] int a_RequiredCoin = 0;

    [Header("Hp")]
    [SerializeField] TMP_Text h_LevelText;
    [SerializeField] TMP_Text h_RequiredCoin_Text;
    [SerializeField] int h_RequiredCoin = 0;

    [Header("Buttons")]
    [SerializeField] Button damageBtn;
    [SerializeField] Button attackSpeedBtn;
    [SerializeField] Button hpBtn;
    [SerializeField] Button backBtn;

    [SerializeField] PlayerData playerData;


    enum UpgradeType { Damage, AttackSpeed, Hp}
    private void Awake()
    {
        damageBtn.onClick.AddListener(() => DameageUp());
        attackSpeedBtn.onClick.AddListener(() => AttackSpeedUp());
        hpBtn.onClick.AddListener(() => HpUp());
        backBtn.onClick.AddListener(() => mainSceneUIManager.HideStoreUI());
    }

    void DameageUp()
    {
        if(DataBaseManager.Instance.playerData.coin < d_RequiredCoin)
        {
            Debug.Log("µ·ÀÌ ºÎÁ·ÇÔ");
            AudioManager.Instance.PlaySFX(SFX.Upgrade_None);
            return;
        }
        AudioManager.Instance.PlaySFX(SFX.Upgrade);
        DataBaseManager.Instance.playerData.coin -= d_RequiredCoin;
        DataBaseManager.Instance.playerData.soldierStatus.damage++;
        DataBaseManager.Instance.SavePlayerData();
        UpdateCointText(UpgradeType.Damage);

    }

    void AttackSpeedUp()
    {
        if (DataBaseManager.Instance.playerData.coin < a_RequiredCoin)
        {
            Debug.Log("µ·ÀÌ ºÎÁ·ÇÔ");
            AudioManager.Instance.PlaySFX(SFX.Upgrade_None);
            return;
        }
        AudioManager.Instance.PlaySFX(SFX.Upgrade);
        DataBaseManager.Instance.playerData.coin -= a_RequiredCoin;
        DataBaseManager.Instance.playerData.soldierStatus.attackSpeed++;
        DataBaseManager.Instance.SavePlayerData();
        UpdateCointText(UpgradeType.AttackSpeed);
    }

    void HpUp()
    {
        if (DataBaseManager.Instance.playerData.coin < h_RequiredCoin)
        {
            Debug.Log("µ·ÀÌ ºÎÁ·ÇÔ");
            AudioManager.Instance.PlaySFX(SFX.Upgrade_None);
            return;
        }
        AudioManager.Instance.PlaySFX(SFX.Upgrade);
        DataBaseManager.Instance.playerData.coin -= h_RequiredCoin;
        DataBaseManager.Instance.playerData.soldierStatus.maxHp++;
        DataBaseManager.Instance.SavePlayerData();
        UpdateCointText(UpgradeType.Hp);
    }

    public void UpdateText()
    {
        UpdateCointText(UpgradeType.Damage);
        UpdateCointText(UpgradeType.AttackSpeed);
        UpdateCointText(UpgradeType.Hp);
    }

    void UpdateCointText(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                d_LevelText.text = $"Level : {DataBaseManager.Instance.playerData.soldierStatus.damage}";
                d_RequiredCoin = (int)Mathf.Pow(2, DataBaseManager.Instance.playerData.soldierStatus.damage);
                d_RequiredCoin_Text.text = d_RequiredCoin.ToString();
                break;
            case UpgradeType.AttackSpeed:
                a_LevelText.text = $"Level : {DataBaseManager.Instance.playerData.soldierStatus.attackSpeed}";
                a_RequiredCoin = (int)Mathf.Pow(2, DataBaseManager.Instance.playerData.soldierStatus.attackSpeed);
                a_RequiredCoin_Text.text = a_RequiredCoin.ToString();
                break;
            case UpgradeType.Hp:
                h_LevelText.text = $"Level : {DataBaseManager.Instance.playerData.soldierStatus.maxHp}";
                h_RequiredCoin = (int)Mathf.Pow(2, DataBaseManager.Instance.playerData.soldierStatus.maxHp);
                h_RequiredCoin_Text.text = h_RequiredCoin.ToString();
                break;
        }
        hasCoinText.text = DataBaseManager.Instance.playerData.coin.ToString();
    }

}
