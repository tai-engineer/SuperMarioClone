using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName ="Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip[] clips;

    public RangedFloat volume;

    [MinMaxRange(0f, 2f)]
    public RangedFloat pitch;
    public override void Play(AudioSource audioPlayer)
    {
        if (clips.Length == 0) return;

        audioPlayer.clip = clips[Random.Range(0, clips.Length)];
        audioPlayer.volume = Random.Range(volume.minValue, volume.maxValue);
        audioPlayer.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        audioPlayer.Play();
    }
}
