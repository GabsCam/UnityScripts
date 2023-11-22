using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : Singleton<PoolingManager>
{
    private Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();

    public GameObject InstantiatePool(GameObject prefab, Vector3 position, Transform parent = null)
    {
        /*foreach (var item in pooledObjects)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                Debug.Log("key: " + item.Key + " value: " + item.Value[i]);
            }
        }*/
           

        if (!pooledObjects.ContainsKey(prefab))
        {
            pooledObjects[prefab] = new List<GameObject>();
        }

        List<GameObject> pool = pooledObjects[prefab];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                if (parent) pool[i].transform.SetParent(parent); pool[i].transform.localScale = Vector3.one;
                pool[i].transform.localPosition = position;

                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);

        if (parent) newObj.transform.SetParent(parent); newObj.transform.localScale = Vector3.one;
        newObj.transform.localPosition = position;

        newObj.SetActive(true);
        return newObj;
    }

    public void DestroyPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void RemovePool(GameObject obj)
    {
        if (pooledObjects.ContainsKey(obj))
        {
            pooledObjects.Remove(obj);
            Destroy(obj);
        }
    }

    public void ResetPool() {
        pooledObjects.Clear();
    }
}
