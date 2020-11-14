﻿using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public interface IObjectPooler
{
    void CreatePool();
    GameObject GetPooledObject();
}
