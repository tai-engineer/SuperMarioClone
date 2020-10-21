using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    string _currentLevelName;
    List<AsyncOperation> _loadOperations;
    List<GameObject> _instanceSystemPrefabs; //TODO: Instantiate manager prefabs
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //TODO: Destroy prefabs in _instanceSystemPrefabs
    }

    public void LoadScene(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if(ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
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
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if(_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            // Ensure that all load operations are completed.
            if(_loadOperations.Count == 0)
            {
                // Start game after loading scene
            }
        }
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Comlete.");
    }
}
