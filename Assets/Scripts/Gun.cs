using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform muzzlePoint;
    [SerializeField] int damage;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] ParticleSystem muzzleFlash;

    enum WeaponType { Gun, Bow }
    [Header("Test")]
    [SerializeField] WeaponType weaponType;

    [Header("Pool")]
    [SerializeField] ParticlePool hitEffects;
    [SerializeField] float returnTime;

    IEnumerator ReturnRoutine(ParticleSystem hitEffect)
    {
        yield return new WaitForSeconds(returnTime);
        hitEffects.ReturnPool(hitEffect);
    }

    public void Fire()
    {
        muzzleFlash.Play();
        if (weaponType == WeaponType.Gun)
        {
            if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance, layerMask))
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.5f);

                IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>();
                damagable?.TakeDamage(damage);

                // 오브젝트 풀링
                ParticleSystem effect = hitEffects.GetPool(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                StartCoroutine(ReturnRoutine(effect));
                /*ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                effect.transform.parent = hitInfo.transform;*/

                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * 10f, hitInfo.point, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.5f);
            }
        }
        else if (weaponType == WeaponType.Bow)
        {
            if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance))
            {
                // 레이캐스트 궤적
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.green, 0.5f);

                // 충돌체가 IDamagable을 포함하고 있다면 데미지 주기
                IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>();
                damagable?.TakeDamage(damage);

                // 충돌한 위치에 ParticleSytem 법선벡터 방향으로 재생
                /*ParticleSystem particle = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                particle.transform.parent = hitInfo.transform;*/

                // 충돌한 물체에 Rigidbody가 있으면 법선벡터 반대방향으로 힘 가해주기
                Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForceAtPosition(-hitInfo.normal * damage, hitInfo.point, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.green, 0.5f);
            }
        }
    }
}
