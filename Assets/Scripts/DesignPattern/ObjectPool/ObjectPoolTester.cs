using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTester : MonoBehaviour
{
    [SerializeField] PooledObject bulletPrefab;
    [SerializeField] PooledObject effectPrefab;

    /*private void Start()
    {
        Manager.Instance.PoolManager.CreatePool(bulletPrefab, 5);
        Manager.Instance.PoolManager.CreatePool(effectPrefab, 5);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PooledObject buulet = Manager.Instance.PoolManager.GetPool(bulletPrefab, Random.insideUnitSphere * 10f, Random.rotation);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PooledObject effect = Manager.Instance.PoolManager.GetPool(effectPrefab, Random.insideUnitSphere * 10f, Random.rotation);
        }
    }*/
}
