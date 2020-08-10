using UnityEngine;

public enum MarioType
{
    Small,
    Big
}
public abstract class Mario : MonoBehaviour
{

    protected abstract void BigTransform();
    protected abstract void FireShooter();
    protected abstract void Invincible();
}
