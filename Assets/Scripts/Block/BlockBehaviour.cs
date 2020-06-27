using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class BlockBehaviour : MonoBehaviour
{
    #region Component Variables
    AudioSource _audioPlayer;
    #endregion

    #region Block Variables

    // Object to spawn
    public GameObject SuperMushroom;
    public GameObject FireFlower;
    public GameObject Starman;
    public GameObject OneUpMushroom;
    public GameObject Coin;

    // Coin
    public bool hasCoin;
    public int NumberOfCoin;
    public float CoinAppearTime = 1f;

    // PowerUp
    public bool hasFireFlower;
    public bool hasStarman;
    public bool hasOneUpMushroom;
    #endregion

    //Test
    public enum CharacterType
    {
        Small,
        Big
    }
    public CharacterType type;
    void Awake()
    {
        _audioPlayer = GetComponent<AudioSource>();
        if (hasCoin)
        {
            SpawnCoins(); 
        }
        else
        {
            SpawnPowerUp(type);
        }
    }

    void SpawnCoins()
    {
        for(int i = 0; i < NumberOfCoin; i++)
        {
            GameObject coin = Instantiate(Coin, transform.position, Quaternion.identity, transform);
            Destroy(coin, CoinAppearTime);
        }
    }

    void SpawnPowerUp(CharacterType type)
    {
        GameObject obj;
        if (hasOneUpMushroom)
        {
            obj = OneUpMushroom;
            Instantiate(obj, transform.position, Quaternion.identity);
            return;
        }

        obj = (type == CharacterType.Small) ? SuperMushroom : hasFireFlower ? FireFlower : Starman;
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}
