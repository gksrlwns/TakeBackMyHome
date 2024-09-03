using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public bool isPause;
    public int obtainCoin = 0;
    [SerializeField] int startingSoldierCount = 5;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        isPause = false;
        player.CreateSoldier(startingSoldierCount);
        StageManager.Instance.CreateStage();
    }
    #region Game Over Event

    public event Action OnGameClear;
    public event Action OnGameFail;
    public void CompleteGame()
    {
        isPause = true;
        StageManager.Instance.level++;
        DataBaseManager.Instance.playerData.coin += obtainCoin;
        DataBaseManager.Instance.SavePlayerData();
        OnGameClear?.Invoke();
    }

    public void FailedGame()
    {
        isPause = true;
        DataBaseManager.Instance.playerData.coin += obtainCoin;
        DataBaseManager.Instance.SavePlayerData();
        OnGameFail?.Invoke();
    }

    #endregion
}
