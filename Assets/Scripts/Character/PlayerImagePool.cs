using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImagePool : Singleton<PlayerImagePool>, IObjectPooler
{
    public ObjectPoolItem itemToPool;
    Queue<GameObject> pooledObjects = new Queue<GameObject>();

    void Start()
    {
        CreatePool();
    }
    public void CreatePool()
    {
        for (int i = 0; i < itemToPool.amountToPool; i++)
        {
            GameObject obj = Instantiate(itemToPool.objectToPool, transform);
            obj.SetActive(false);
            pooledObjects.Enqueue(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        if (pooledObjects.Count == 0)
        {
            if(itemToPool.shouldExpand)
            {
                GameObject obj = Instantiate(itemToPool.objectToPool, transform);
                obj.SetActive(false);
                pooledObjects.Enqueue(obj);
                return obj;
            }
            Debug.LogError("[ObjectPooler] Not found pooled object.");
            return null;
        }

        return pooledObjects.Dequeue();
    }
    public void AddPool(GameObject obj)
    {
        obj.SetActive(false);
        pooledObjects.Enqueue(obj);
    }

    public void ActivatePooledObject()
    {
        GameObject obj = GetPooledObject();
        obj.SetActive(true);
    }
}
