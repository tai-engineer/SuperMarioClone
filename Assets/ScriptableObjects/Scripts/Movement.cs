using System.Collections;
using UnityEngine;

public abstract class Movement : ScriptableObject
{
    [Range(0.0f, 100.0f)]
    public float speed;

    public float gravity;

    [HideInInspector]
    public Vector3 direction = Vector3.right;

    public abstract void Patrol(MonoBehaviour monoBehaviour);
    public virtual IEnumerator MoveUpward(MonoBehaviour monoBehaviour)
    {
        return null;
    }
}
