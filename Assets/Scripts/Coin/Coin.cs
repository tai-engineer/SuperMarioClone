using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    public float bounceForce;
    public float gravity;
    public AudioEvent coinSound;

    Rigidbody2D _rb;
    AudioSource _audioPlayer;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioPlayer = GetComponent<AudioSource>();
    }
    void Start()
    {
        coinSound.Play(_audioPlayer);
        _rb.gravityScale = gravity;
        _rb.AddForce(new Vector2(0f, _rb.position.y + bounceForce), ForceMode2D.Impulse);
    }
}
