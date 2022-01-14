using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePSTEst : MonoBehaviour
{
    public GameObject PS_FireEffect;
    public GameObject PS_SpawnEffect;
    public Transform _specialSpawn;
    public GameObject _titanObject;
    Transform test;

    void Start()
    {
        StartCoroutine(VoidSpawnTitan());
    }

    private IEnumerator EnumOnFire(float seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator VoidSpawnTitan()
    {
        GameObject spawnEffect = (GameObject)Instantiate(PS_SpawnEffect, new Vector3(_specialSpawn.position.x, _specialSpawn.position.y - 27f, _specialSpawn.position.z), Quaternion.identity);
        yield return new WaitForSeconds(4f);
        Instantiate(_titanObject, _specialSpawn.position, Quaternion.identity);
        yield return new WaitForSeconds(3.5f);
        Destroy(spawnEffect);
    }

}
