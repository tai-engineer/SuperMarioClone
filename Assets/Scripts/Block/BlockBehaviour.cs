using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class BlockBehaviour : MonoBehaviour
{
    public enum SpawnObjectType
    {
        SuperMushroom,
        FireFlower,
        Starman,
        OneUpMushroom,
        Coin,
        None
    }

    #region Component Variables
    AudioSource _audioPlayer;
    Rigidbody2D _rb;
    SpriteRenderer _render;
    BoxCollider2D _box;
    #endregion

    #region Block Variables
    #region PowerUp
    public GameObject SuperMushroom;
    public GameObject FireFlower;
    public GameObject Starman;
    public GameObject OneUpMushroom;
    public SpawnObjectType spawnType;
    bool _startSpawnObj = false;
    readonly Vector3 _spawnOffSet = new Vector3(0f, 0.5f, 0f);
    #endregion

    #region Coin
    public GameObject Coin;
    public int numberOfCoins;
    public float coinAppearTime = 1f;
    #endregion

    #region Brick
    public GameObject brick;
    public AudioEvent brickShatterSound;
    List<Vector3> _brickForcesList = new List<Vector3>();
    readonly int _numberOfBricks = 4;
    bool _startShatter = false;
    bool _isShattered = false;
    #endregion

    #region Bounce
    float _bounceDistance = 0.5f;
    Vector3 _bounceVector;
    Vector3 _startPostion;
    bool _bounceStart = false;
    bool _bounceUp = true;
    #endregion

    #region Deactivate
    public Sprite deactivateSprite;
    bool _isDeactivated = false;
    #endregion

    #region Hit
    GameObject _hitObject = null;
    bool _isHitByPlayer = false;
    bool _isColliding = false;
    #endregion
    #endregion
    void Awake()
    {
        _audioPlayer = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        _render = GetComponent<SpriteRenderer>();
        _box = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        Init();
    }
    void Update()
    {
        if (gameObject != null && _isShattered && !_audioPlayer.isPlaying)
        {
            Destroy(gameObject);
        }

        if (_startShatter)
        {
            Shatter();
        }

        if (_startSpawnObj && _isHitByPlayer && !_isColliding)
        {
            SpawnObject(_hitObject.GetComponent<MarioController>().Type);
        }
    }
    void FixedUpdate()
    {
        if (!_isDeactivated && _bounceStart)
        {
            Bounce();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        _isColliding = true;
        _hitObject = collision.gameObject;
        if (!_hitObject.CompareTag("Player"))
        {
            _isHitByPlayer = false;
            return;
        }

        /*|--\/--|  None   |    Coin      |    Others    |
         *|--/\--|---------|--------------|--------------|
         *| Small| Bounce  | Bounce/Spawn | Bounce/Spawn |
         *|------|-------- |--------------|--------------|
         *|  Big | Shatter | Bounce/Spawn | Bounce/Spawn |
         */
        if (!_isDeactivated &&
            _hitObject.GetComponent<MarioController>().Type == MarioType.Big &&
            spawnType == SpawnObjectType.None)
        {
            _startShatter = true;
            return;
        }

        _startSpawnObj = true;
        _bounceStart = true;
        _isHitByPlayer = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Avoid spawning objects continously
        _isColliding = false;
    }
    void Init()
    {
        _startPostion = transform.position;
        _bounceVector = _startPostion + new Vector3(0f, _bounceDistance, 0f);

        CreateBrickForces();
    }

    /* Reset Bounce variables for next bounce */
    void ResetBounceVector()
    {
        _bounceVector = _startPostion + new Vector3(0f, _bounceDistance, 0f);
        _bounceUp = true;
    }
    void Bounce()
    {
        float speed = 10.0f;
        Vector3 moveVector = Vector3.MoveTowards(_rb.position, _bounceVector, speed * Time.fixedDeltaTime);
        if(((Vector2)_bounceVector - _rb.position).sqrMagnitude > Mathf.Epsilon)
        {
            _rb.MovePosition(moveVector);
        }
        else
        {
            _bounceVector = _startPostion;
            _bounceUp = false;
        }

        bool bounceEnd = (_bounceUp == false) && Mathf.Approximately(_rb.position.y, _startPostion.y);
        if (bounceEnd)
        {
            _bounceStart = false;
            ResetBounceVector();
            Deactivate();
        }
    }
    void CreateBrickForces()
    {
        float x = 2.0f;
        float y_min = 4.0f;
        float y_max = 15.0f;

        _brickForcesList.Add(new Vector3(-x, y_max, 0f));
        _brickForcesList.Add(new Vector3( x, y_max, 0f));
        _brickForcesList.Add(new Vector3(-x, y_min, 0f));
        _brickForcesList.Add(new Vector3( x, y_min, 0f));
    }
    void Shatter()
    {
        if (_isShattered)
        {
            return;
        }

        DisableComponents();
        brickShatterSound.Play(_audioPlayer);
        for (int i = 0; i < _numberOfBricks; i++)
        {
            GameObject obj = Instantiate(brick, _startPostion, Quaternion.identity, transform);
            obj.GetComponent<BrickBehaviour>().SetForce(_brickForcesList[i]);
            obj.GetComponent<BrickBehaviour>().StartShatter();
        }
         _isShattered = true;
    }
    void Deactivate()
    {
        if(_isDeactivated)
        {
            return;
        }

        if(spawnType == SpawnObjectType.Coin && numberOfCoins > 0)
        {
            return;
        }
        _render.sprite = deactivateSprite;
        _isDeactivated = true;
    }

    /* Make block invisible until Break sound finishes */
    void DisableComponents()
    {
        _render.enabled = false;
        _box.enabled = false;
    }
    void SpawnCoins()
    {
        if (numberOfCoins > 0)
        {
            GameObject coin = Instantiate(Coin, _startPostion + _spawnOffSet, Quaternion.identity, transform);
            Destroy(coin, coinAppearTime);
            numberOfCoins--;
        }
    }
    void SpawnPowerUp(MarioType type)
    {
        GameObject obj = null;
        switch (spawnType)
        {
            case SpawnObjectType.SuperMushroom:
                obj = SuperMushroom;
                break;
            case SpawnObjectType.OneUpMushroom:
                obj = OneUpMushroom;
                break;
            case SpawnObjectType.Starman:
                obj = (type == MarioType.Small) ? SuperMushroom : Starman;
                break;
            case SpawnObjectType.FireFlower:
                obj = (type == MarioType.Small) ? SuperMushroom : FireFlower;
                break;
        }
        Instantiate(obj, _startPostion + _spawnOffSet, Quaternion.identity);
    }
    void SpawnObject(MarioType type)
    {
        if(_isDeactivated)
        {
            return;
        }

        if (spawnType == SpawnObjectType.Coin)
        {
            SpawnCoins();
        }
        else if(spawnType != SpawnObjectType.None)
        {
            SpawnPowerUp(type);
        }
        _startSpawnObj = false;
    }
}
