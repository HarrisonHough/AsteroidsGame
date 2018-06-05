using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {
    [SerializeField]
    private GameObject prefabToPool;
    [SerializeField]
    private int poolSize = 50;

    private Queue<GameObject> objectsQueue = new Queue<GameObject>();
    private List<GameObject> objectPool = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        GrowPool();

    }

    public GameObject GetObject()
    {
        if (objectsQueue.Count == 0)
        {
            GrowPool();
        }

        var pooledObject = objectsQueue.Dequeue();

        return pooledObject;
    }


    private void GrowPool()
    {
        int lastPoolSize = objectPool.Count;
        for (int i = 0; i < poolSize; i++)
        {
            var pooledObject = Instantiate(prefabToPool);
            pooledObject.name += " " + (i + lastPoolSize );
            pooledObject.transform.parent = transform;
            pooledObject.AddComponent<PoolMember>();

            //TODO maybe set on disable event
            pooledObject.GetComponent<PoolMember>().OnDestroyEvent += () => AddObjectToAvailable(pooledObject);


            //add to pool
            objectPool.Add(pooledObject);

            pooledObject.SetActive(false);
        }
    }

    private void AddObjectToAvailable(GameObject pooledObject)
    {
        objectsQueue.Enqueue(pooledObject);
    }
}
