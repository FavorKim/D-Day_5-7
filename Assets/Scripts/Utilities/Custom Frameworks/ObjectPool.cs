using UnityEngine;
using UnityEngine.Pool;

public abstract class ObjectPoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private IObjectPool<T> objectPool;

    [SerializeField] protected GameObject objectPrefab;

    private bool collectionCheck = true;
    private int defaultCapacity = 10;
    private int maxSize = 10000;

    private void Awake()
    {
        objectPool = new ObjectPool<T>(CreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, collectionCheck, defaultCapacity, maxSize);
    }

    public abstract T CreateObject();
    /*
    {
        T obj = Instantiate(objectPrefab).GetComponent<T>();
        obj.GetObjectPool(objectPool);

        return obj;
    }
    */

    private void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyObject(T obj)
    {
        Destroy(obj.gameObject);
    }
}
