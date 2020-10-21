using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager: Singleton<LevelManager>
{
    int _score = 0;
    int _coinNumber = 0;
    int _mainLevel = 1;
    int _subLevel = 1;

    #region Getter/Setter
    public int Score { get { return _score; } }
    public int CoinNumber { get { return _coinNumber; } }
    public int MainLevel { get { return _mainLevel; } }
    public int SubLevel { get { return _subLevel; } }
    #endregion
}
