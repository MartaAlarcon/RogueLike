using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [SerializeField] private GameObject prefab; 
    [SerializeField] private int initialPoolSize = 5; 

    private Stack<GameObject> objectPool = new Stack<GameObject>(); 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); 
            objectPool.Push(obj);
        }
    }

    public GameObject GetFromPool()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Pop();
            obj.SetActive(true);

            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;

            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Push(obj);
            return GetFromPool(); 
        }
    }


    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Push(obj);
    }
}
