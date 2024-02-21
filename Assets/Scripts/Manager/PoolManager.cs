using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PoolManager : MonoBehaviour
{
    /*private Dictionary<string, ObjectPooler> poolDic = new Dictionary<string, ObjectPooler>();

    // 1. 오브젝트 풀 생성
    public void CreatePool(string name, PooledObject prefab, int size)
    {
        GameObject poolObject = new GameObject($"Pool_{name}");
        ObjectPooler pooler = poolObject.AddComponent<ObjectPooler>();
        pooler.CreatePool(prefab, size);

        poolDic.Add(name, pooler);
    }
    
    public void CreatePool(PooledObject prefab, int size)
    {
        GameObject poolObject = new GameObject($"Pool_{prefab.gameObject.name}");
        ObjectPooler pooler = poolObject.AddComponent<ObjectPooler>();
        pooler.CreatePool(prefab, size);

        poolDic.Add(prefab.gameObject.name, pooler);
    }

    // 2. 오브젝트 풀 제거
    public void RemovePool(string name)
    {
        ObjectPooler pooler = poolDic[name];
        Destroy(pooler.gameObject);

        poolDic.Remove(name);
    }
    
    public void RemovePool(PooledObject prefab)
    {
        ObjectPooler pooler = poolDic[prefab.gameObject.name];
        Destroy(pooler.gameObject);

        poolDic.Remove(prefab.gameObject.name);
    }

    // 3. 오브젝트 풀에서 인스턴스 가져오기
    public PooledObject GetPool(string name, Vector3 position, Quaternion rotation)
    {
        return poolDic[name].GetPool(position, rotation);
    }
    
    public PooledObject GetPool(PooledObject prefab, Vector3 position, Quaternion rotation)
    {
        return poolDic[prefab.gameObject.name].GetPool(position, rotation);
    }*/
}
