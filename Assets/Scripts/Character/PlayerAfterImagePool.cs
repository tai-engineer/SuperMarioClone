using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : Singleton<PlayerAfterImagePool>
{
     public GameObject afterImagePrefab;

    Queue<GameObject> availableObjects = new Queue<GameObject>();
    public int objectPoolElements = 10;

    void Start()
    {
        GrowPool();
    }
    void GrowPool()
    {
        for (int i = 0; i < objectPoolElements; i++)
        {
            GameObject instanceToAdd = Instantiate(afterImagePrefab, transform);
            AddPool(instanceToAdd);
        }
    }

    public void AddPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if(availableObjects.Count == 0)
        {
            GrowPool();
        }

        GameObject instance = availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;

    }
}
