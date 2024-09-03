using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    [Header("Title UI")]
    [SerializeField] GameObject mainUI;
    [SerializeField] TitleMove titleMove;
    [SerializeField] TMP_Text titleText;

    [Header("Menu UI")]
    [SerializeField] GameObject menuUI;
    [SerializeField] Button gameStartBtn;
    [SerializeField] Button storeBtn;
    [SerializeField] Button settingsBtn;
    [SerializeField] Button exitBtn;

    [Header("Store UI")]
    [SerializeField] StoreUIManager storeUI;

    [Header("Settings UI")]
    [SerializeField] SettingsUIManager settingsUI;


    private void Awake()
    {
        gameStartBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene("StageScene"));
        storeBtn.onClick.AddListener(() => ShowStoreUI());
        settingsBtn.onClick.AddListener(() => ShowSettingsUI());
        exitBtn.onClick.AddListener(() => Application.Quit());
        settingsUI.backBtn.onClick.AddListener(() => HideSettingsUI());
    }

    private void Start()
    {
        ShowMainUI();
    }
    
    public void ShowMainUI()
    {
        titleText.transform.DOScale(1, 1);
        menuUI.transform.DOLocalMoveX(0, 1);
    }

    #region Store UI
    void ShowStoreUI()
    {
        titleText.transform.DOScale(0, 1);
        menuUI.transform.DOLocalMoveX(550,1);
        storeUI.transform.DOLocalMoveX(0,1);
        storeUI.UpdateText();
    }

    public void HideStoreUI()
    {
        titleText.transform.DOScale(1, 1);
        menuUI.transform.DOLocalMoveX(0, 1);
        storeUI.transform.DOLocalMoveX(-550, 1);
    }

    #endregion
    #region Settings UI
    void ShowSettingsUI()
    {
        titleText.transform.DOScale(0, 1);
        menuUI.transform.DOLocalMoveX(550, 1);
        settingsUI.transform.DOLocalMoveX(0, 1);
    }

    public void HideSettingsUI()
    {
        titleText.transform.DOScale(1, 1);
        menuUI.transform.DOLocalMoveX(0, 1);
        settingsUI.transform.DOLocalMoveX(-550, 1);
    }
    #endregion
}
