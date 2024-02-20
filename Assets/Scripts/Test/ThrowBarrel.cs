using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowBarrel : MonoBehaviour
{
    [SerializeField] Transform[] spawns;
    [SerializeField] Barrel barrel;
    [SerializeField] float throwPower;
    [SerializeField] float timeRate;

    Transform spawn;

    private void Start()
    {
        StartCoroutine(Throw());
    }

    IEnumerator Throw()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            spawn = spawns[Random.Range(0, spawns.Length)];

            Rigidbody barrelRigid = Instantiate(barrel, spawn.position, spawn.rotation).GetComponent<Rigidbody>();
            barrelRigid.velocity = spawn.up * throwPower;
            yield return new WaitForSeconds(timeRate);
        }
    }
}
