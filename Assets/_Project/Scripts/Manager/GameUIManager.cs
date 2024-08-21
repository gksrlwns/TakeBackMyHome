using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameObject clearUIGroup;
    [SerializeField] GameObject failUIGroup;
    [SerializeField] Button successBtn;
    [SerializeField] Button failedBtn;

    private void Awake()
    {
        successBtn.onClick.AddListener(SceneLoadManager.Instance.ReloadCurrentScene);
        failedBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene("WorldMapScene"));
    }

    private void Start()
    {
        //�̺�Ʈ ����
        GameManager.instance.OnGameClear += ShowGameClearUI;
        GameManager.instance.OnGameFail += ShowGameFailedUI;

        failUIGroup.SetActive(false);
        clearUIGroup.SetActive(false);
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
    void ShowGameClearUI()
    {
        Debug.Log("CompleteGame �Լ� ����");
        clearUIGroup.SetActive(true);
    }

    void ShowGameFailedUI()
    {
        Debug.Log("FailedGame �Լ� ����");
        failUIGroup.SetActive(true);
    }
}
