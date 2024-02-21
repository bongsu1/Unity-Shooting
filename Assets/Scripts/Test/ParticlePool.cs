using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    Stack<ParticleSystem> effects;
    [SerializeField] ParticleSystem effect;
    [SerializeField] int size;

    private void Awake()
    {
        CreatePool();
    }

    // Ǯ ����
    public void CreatePool()
    {
        effects = new Stack<ParticleSystem>(size);

        for (int i = 0; i < size; i++)
        {
            GameObject particleObject = Instantiate(effect.gameObject);
            ParticleSystem instance = particleObject.GetComponent<ParticleSystem>();
            instance.gameObject.SetActive(false);
            instance.transform.parent = transform;

            effects.Push(instance);
        }
    }

    // Ǯ���� ������Ʈ ��������
    public ParticleSystem GetPool(Vector3 position, Quaternion rotation)
    {
        if (effects.Count > 0)
        {
            ParticleSystem instance = effects.Pop();
            instance.transform.parent = null;
            instance.transform.position = position;
            instance.transform.rotation = rotation;

            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            GameObject particleObject = Instantiate(effect.gameObject, position, rotation);
            return particleObject.GetComponent<ParticleSystem>();
        }
    }

    // Ǯ�� �ݳ�
    public void ReturnPool(ParticleSystem instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.parent = transform;

        effects.Push(instance);
    }
}
