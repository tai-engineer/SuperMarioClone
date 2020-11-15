using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public float impactDistance;
    public LayerMask solidLayer;

    public GameObject destroyEffect;
    float _lifeTimeLeft;
    bool hit = false;
    void Start()
    {
        _lifeTimeLeft = lifeTime;
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, impactDistance, solidLayer);
        if(hitInfo.collider != null)
        {
            hit = true;
            Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
            //TODO: Implement take damage
        }

        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        _lifeTimeLeft -= Time.deltaTime;
        DestroyFireBall();
        
    }

    void DestroyFireBall()
    {
        if (hit || _lifeTimeLeft < 0f)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
