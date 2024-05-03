using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    // IObjectPool
    private IObjectPool<T> objectPool;

    // Prefab
    [SerializeField] private GameObject prefabObject;

    // Parameters of IObjectPool
    private bool collectionCheck = true;
    private int defaultCapacity = 10;
    private int maxSize = 10000;

    private void Awake()
    {
        objectPool = new ObjectPool<T>(CreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, collectionCheck, defaultCapacity, maxSize);
    }

    // Create(Instantiate)
    private T CreateObject()
    {
        T obj = Instantiate(prefabObject).GetComponent<T>();
        return obj;
    }

    // Get(Active)
    private void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    // Release(Inactive)
    private void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    // Destroy
    private void OnDestroyObject(T obj)
    {
        Destroy(obj.gameObject);
    }
}
