using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// StageScene�� UI�� �����ϴ� ��ũ��Ʈ
/// </summary>
public class GameUIManager : MonoBehaviour
{
    [Header("Success UI")]
    [SerializeField] GameObject successUI;
    [SerializeField] TMP_Text obstainCoinText;
    [SerializeField] Button nextStageBtn;
    [SerializeField] Button successBackToMenuBtn;

    [Header("Failed UI")]
    [SerializeField] GameObject failUI;
    [SerializeField] TMP_Text failedObstainCoinText;
    [SerializeField] Button failRetryBtn;
    [SerializeField] Button failedBackToMenuBtn;

    [Header("Guid UI")]
    [SerializeField] GameObject guideUI;

    [Header("Pause UI")]
    [SerializeField] GameObject pauseUI;
    [SerializeField] Button pauseBtn;
    [SerializeField] Button resumeBtn;
    [SerializeField] Button retryBtn;
    [SerializeField] Button settingsBtn;
    [SerializeField] Button backToMenuBtn;

    [Header("Settings UI")]
    [SerializeField] SettingsUIManager settingsUI;

    [Header("Coin UI")]
    [SerializeField] TMP_Text coinText;
    
    private void Awake()
    {
        settingsUI.backBtn.onClick.AddListener(HideSettingsUI);
        nextStageBtn.onClick.AddListener(SceneLoadManager.Instance.ReloadCurrentScene);
        failRetryBtn.onClick.AddListener(SceneLoadManager.Instance.ReloadCurrentScene);
        retryBtn.onClick.AddListener(SceneLoadManager.Instance.ReloadCurrentScene);
        resumeBtn.onClick.AddListener(HidePauseGameUI);
        pauseBtn.onClick.AddListener(ShowPauseGameUI);
        settingsBtn.onClick.AddListener(ShowSettingsUI);
        successBackToMenuBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene("MenuScene"));
        failedBackToMenuBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene("MenuScene"));
        backToMenuBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene("MenuScene"));
    }

    private void Start()
    {
        //�̺�Ʈ ����
        GameManager.instance.OnGameClear += ShowGameClearUI;
        GameManager.instance.OnGameFail += ShowGameFailedUI;
        coinText.text = DataBaseManager.Instance.playerData.coin.ToString();
    }
    private void OnDestroy()
    {
        // �̺�Ʈ ���� ����
        if (GameManager.instance != null)
        {
            GameManager.instance.OnGameClear -= ShowGameClearUI;
            GameManager.instance.OnGameFail -= ShowGameFailedUI;
        }
    }
    #region Settings
    void ShowSettingsUI()
    {
        pauseUI.SetActive(false);
        settingsUI.gameObject.SetActive(true);
    }

    void HideSettingsUI()
    {
        settingsUI.gameObject.SetActive(false);
        pauseUI.SetActive(true);
    }
    #endregion

    #region PauseGame
    void ShowPauseGameUI()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    void HidePauseGameUI()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
    #endregion
    #region End Game
    void ShowGameClearUI()
    {
        successUI.transform.DOScale(1, 1).OnComplete(() => Time.timeScale = 0);
        obstainCoinText.text = GameManager.instance.obtainCoin.ToString();
        //effect ������ ���� ��
    }

    void ShowGameFailedUI()
    {
        Debug.Log("FailedGame �Լ� ����");
        failUI.transform.DOScale(1, 1).OnComplete(() => Time.timeScale = 0);
        failedObstainCoinText.text = GameManager.instance.obtainCoin.ToString();
    }
    #endregion

}
