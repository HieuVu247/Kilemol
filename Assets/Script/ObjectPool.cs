using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();
    private Func<T> createFunc;
    private int initialSize;

    public ObjectPool(Func<T> createFunc, int initialSize)
    {
        this.createFunc = createFunc;
        this.initialSize = initialSize;
        for (int i = 0; i < initialSize; i++)
        {
            T obj = createFunc();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            T obj = createFunc();
            obj.gameObject.SetActive(true);
            return obj;
        }
        T pooledObj = pool.Dequeue();
        pooledObj.gameObject.SetActive(true);
        return pooledObj;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}