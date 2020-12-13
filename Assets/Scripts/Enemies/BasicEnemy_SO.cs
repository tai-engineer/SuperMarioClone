using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="", menuName = "Enemy/BasicEnemy")]
public class BasicEnemy_SO : ScriptableObject
{
    public int health = 0;
    public float speed = 0f;
    public int score = 0;

}
