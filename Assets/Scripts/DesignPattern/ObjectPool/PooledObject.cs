using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPooler pooler;
    [SerializeField] bool autoRelease;
    [SerializeField] float releaseTime;

    Coroutine releaseRoutine;

    private void OnEnable()
    {
        if (autoRelease)
        {
            releaseRoutine = StartCoroutine(ReleaseRoutine());
        }
    }

    private void OnDisable()
    {
        if (autoRelease)
        {
            StopCoroutine(releaseRoutine);
        }
    }

    IEnumerator ReleaseRoutine()
    {
        yield return new WaitForSeconds(releaseTime);
        Release();
    }

    public void Release()
    {
        if (pooler != null)
        {
            pooler.ReturnPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
