using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

#region Event Classes
public class UEOnScoreChange : UnityEvent { }
public class UECoinCollected : UnityEvent { }
public class UECountDownEnd : UnityEvent { }
public class UENextLevel : UnityEvent { }

#endregion
public class UIManager : Singleton<UIManager>
{
    [SerializeField] 
    GameObject _mainMenu;
    [SerializeField]
    Button _startButton;
    [SerializeField]
    GameObject _scorePopUp;

    #region Events
    public UEOnScoreChange ScoreUpdateEvent = new UEOnScoreChange();
    public UECoinCollected CoinCollectedEvent = new UECoinCollected();
    public UECountDownEnd CountDownEndEvent = new UECountDownEnd();
    public UENextLevel NextLevelEvent = new UENextLevel();

    #endregion
    public int Score { get { return GameManager.Instance.Score; } }
    public string CurrentLevel { get { return GameManager.Instance.CurrentLevel; } }
    public int CoinNumber { get { return GameManager.Instance.CoinNumber; } }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (_startButton != null)
        {
            _startButton.onClick.AddListener(OnStartGame); 
        }
        CountDownEndEvent.AddListener(HandleCountDownEnd); 
    }
    public void PopScore(Vector3 position, int score)
    {
        GameObject scoreObj = Instantiate(_scorePopUp);
        scoreObj.transform.position = position;
        TextMeshPro text = scoreObj.GetComponent<TextMeshPro>();
        if (text != null)
        {
            text.text = score.ToString();
        }
    }
    void OnStartGame()
    {
        GameManager.Instance.StartGame();
        _mainMenu.SetActive(false);
    }

    void HandleCountDownEnd()
    {
        GameManager.Instance.GameOver();
    }
}
