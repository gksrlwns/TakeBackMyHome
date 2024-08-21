using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public bool isPause;
    [SerializeField]int startingSoldierCount = 5;

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
        OnGameClear?.Invoke();
    }

    public void FailedGame()
    {
        isPause = true;
        OnGameFail?.Invoke();
    }

    #endregion
}
