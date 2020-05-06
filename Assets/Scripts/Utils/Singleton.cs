using UnityEngine;

public class Singleton<T>: MonoBehaviour where T : Singleton<T>
{
    static T _instance;

    public static T Instance
    {
        get { return _instance; }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = (T) this;
        else
        {
            Debug.LogError("[SingleTon] Trying to instatiate a second singleton class");
        }
    }

    protected virtual void OnDestroy()
    {
        if(_instance != null)
        {
            _instance = null;
        }
    }
}
