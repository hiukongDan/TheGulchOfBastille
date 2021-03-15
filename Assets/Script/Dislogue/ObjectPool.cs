using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Queue<GameObject> queue;
    private int _increaseSize = 10;

    private GameObject objectPoolParent;

    public ObjectPool()
    {
        if (queue == null)
        {
            queue = new Queue<GameObject>();
        }
        else
        {
            queue.Clear();
        }
        objectPoolParent = new GameObject("Object Pool Parent");

        IncreaseItem();
    }
    public ObjectPool(int initSize)
    {
        _increaseSize = initSize > 0 ? initSize : _increaseSize;

        if (queue == null)
        {
            queue = new Queue<GameObject>();
        }
        else
        {
            queue.Clear();
        }

        IncreaseItem();
    }

    public void SetIncreaseSize(int increaseSize) => _increaseSize = increaseSize;

    public GameObject GetItem()
    {
        if(queue.Count == 0)
        {
            IncreaseItem();
        }
        return queue.Dequeue();
    }


    private void IncreaseItem()
    {
        for (int i = 0; i < _increaseSize; i++)
        {
            GameObject GO = new GameObject();
            GO.transform.parent = objectPoolParent.transform;
            queue.Enqueue(GO);
        }
    }

    public void ReturnItem(GameObject GO)
    {
        queue.Enqueue(GO);
    }

    public void DestroyGameObject()
    {
        foreach(GameObject go in queue)
        {
            GameObject.Destroy(go);
        }

        queue.Clear();
    }
}
