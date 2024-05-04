using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;
    protected static ObjectPool<T> instance;
    public static ObjectPool<T> Instance { get { return instance; } }
    private Queue<GameObject> queue;


    public void Init(int count)
    {
        if (instance == null)
        {
            instance = this;
            queue = new Queue<GameObject>();
            Spawn(count);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(instance);
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(bulletPref);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            Enqueue(obj);
        }
    }

    public void Enqueue(GameObject obj)
    {
        queue.Enqueue(obj);
    }

    public GameObject Dequeue()
    {
        GameObject obj = queue.Dequeue();
        return obj;
    }

}
