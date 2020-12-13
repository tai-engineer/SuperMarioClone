using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] 
    GameObject _mainMenu;
    [SerializeField]
    Button _startButton;
    [SerializeField]
    GameObject _scorePopUp;
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        if (_startButton != null)
        {
            _startButton.onClick.AddListener(OnStartGame); 
        }
    }
    void OnStartGame()
    {
        GameManager.Instance.StartGame();
        _mainMenu.SetActive(false);
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

}
