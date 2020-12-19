using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    //TODO: Create editor script to get scene names
    string[] _level = new string[]
    {
        "MainScene",
        "Level_1-1",
        "Level_1-2"
    };

    int _levelIndex = 0;
    string _currentLevel;
    string _nextLevel;
    int _coinNumber = 0;
    int _score = 0;
    List<GameObject> _instanceSystemPrefabs; //TODO: Instantiate manager prefabs
    List<AsyncOperation> _loadOperations;

    #region Getter/Setter
    public string CurrentLevel { get { return _currentLevel; } }
    public int Score { get { return _score; } }
    public int CoinNumber { get { return _coinNumber; } }
    #endregion

    #region Common
    void Start()
    {
        _currentLevel = "MainScene";
        UIManager.Instance.CountDownEndEvent.AddListener(GameOver);
    }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        _loadOperations = new List<AsyncOperation>();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        //TODO: Destroy prefabs in _instanceSystemPrefabs
    }
    #endregion

    #region Scenes
    public void StartGame()
    {
        NextLevel();
    }
    public void GameOver()
    {
        //TODO: What happen when game is over?
        Debug.Log("Game Over!");
    }
    public void LoadScene(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
    }
    public void UnLoadScene(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            // Ensure that all load operations are completed.
            if (_loadOperations.Count == 0)
            {
                // TODO: Start game after loading scene
                UnLoadScene(_currentLevel); 
                _currentLevel = _nextLevel;
            }
        }
    }
    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Comlete.");
    }
    #endregion

    #region Events
    public void NextLevel()
    {
        _levelIndex++;
        _nextLevel = _level[_levelIndex];
        LoadScene(_nextLevel);
        UIManager.Instance.NextLevelEvent.Invoke();
        Debug.Log("_levelIndex: " + _levelIndex);
    }
    public void IncreaseCoin()
    {
        _coinNumber++;
        UIManager.Instance.CoinCollectedEvent.Invoke();
    }
    public void AddScore(int score)
    {
        _score += score;
        UIManager.Instance.ScoreUpdateEvent.Invoke();
    }
    #endregion
}
