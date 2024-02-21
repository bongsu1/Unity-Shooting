using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneFlow : MonoBehaviour
{
    [SerializeField] PooledObject bulletPrefab;
    [SerializeField] PooledObject effectPrefab;

    /*private void OnEnable()
    {
        Loading();
    }

    private void OnDisable()
    {
        UnLoading();
    }

    public void Loading()
    {
        Manager.Instance.PoolManager.CreatePool(bulletPrefab, 20);
        Manager.Instance.PoolManager.CreatePool(effectPrefab, 10);
    }

    public void UnLoading()
    {
        Manager.Instance.PoolManager.RemovePool(bulletPrefab);
        Manager.Instance.PoolManager.RemovePool(effectPrefab);
    }*/
}
