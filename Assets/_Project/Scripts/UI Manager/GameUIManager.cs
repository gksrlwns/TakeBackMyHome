using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Success UI")]
    [SerializeField] GameObject successUI;
    [SerializeField] Button nextStageBtn;
    [SerializeField] Button successBackToMenuBtn;

    [Header("Failed UI")]
    [SerializeField] GameObject failUI;
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
        //이벤트 구독
        GameManager.instance.OnGameClear += ShowGameClearUI;
        GameManager.instance.OnGameFail += ShowGameFailedUI;
    }
    private void OnDestroy()
    {
        // 이벤트 구독 해제
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
        
        //effect 넣으면 좋을 듯
    }

    void ShowGameFailedUI()
    {
        Debug.Log("FailedGame 함수 실행");
        failUI.transform.DOScale(1, 1).OnComplete(() => Time.timeScale = 0);
        
    }
    #endregion

}
