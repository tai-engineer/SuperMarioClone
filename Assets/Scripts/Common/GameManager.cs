using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    //TODO: Create editor script to get scene names
    string[] _level = new string[]
    {
        "Level_1-1",
        "Level_1-2"
    };
    List<GameObject> _instanceSystemPrefabs; //TODO: Instantiate manager prefabs
    List<AsyncOperation> _loadOperations;
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

    public void StartGame()
    {
        LoadScene(_level[0]);
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
    public void LoadFirstScene()
    {
        LoadScene(_level[0]);
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
            }
        }
    }
    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Comlete.");
    }
}
