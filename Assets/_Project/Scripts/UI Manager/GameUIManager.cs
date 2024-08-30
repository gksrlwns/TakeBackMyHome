using System.Collections;
using System.Collections.Generic;
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
        //settingsUI.backBtn.onClick.AddListener()
        nextStageBtn.onClick.AddListener(SceneLoadManager.Instance.ReloadCurrentScene);
        failRetryBtn.onClick.AddListener(SceneLoadManager.Instance.ReloadCurrentScene);
        successBackToMenuBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene("MenuScene"));
        failedBackToMenuBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene("MenuScene"));
    }

    private void Start()
    {
        //이벤트 구독
        GameManager.instance.OnGameClear += ShowGameClearUI;
        GameManager.instance.OnGameFail += ShowGameFailedUI;

        failUI.SetActive(false);
        successUI.SetActive(false);
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

    #region End Game
    void ShowGameClearUI()
    {
        Debug.Log("CompleteGame 함수 실행");
        successUI.SetActive(true);
    }

    void ShowGameFailedUI()
    {
        Debug.Log("FailedGame 함수 실행");
        failUI.SetActive(true);
    }
    #endregion

}
