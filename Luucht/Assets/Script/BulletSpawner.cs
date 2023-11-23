using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner bulletPoolInstance;

    [SerializeField] private GameObject pooledBullet;
    private bool notEnoughtBulletInPool = true;

    private List<GameObject> bullet;

    private void Awake()
    {
        bulletPoolInstance = this;
    }

    private void Start()
    {
        bullet = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        if (bullet.Count > 0)
        {
            for (int i = 0; i < bullet.Count; i++)
            {
                if (!bullet[i].activeInHierarchy)
                {
                    return bullet[i];
                }
            }
        }

        if (notEnoughtBulletInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullet.Add(bul);
            return bul;
        }
        return null;
    }
}
