using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableBullet2 : MonoBehaviour
{
    public abstract void bulletDestroy2();
}

public class BulletPool2 : MonoBehaviour
{
    public static BulletPool2 bulletPoolInstance2;

    public PoolableBullet2 pooledBullet2;
    public int pooledAmount2;
    public bool willGrow2;

    private List<PoolableBullet2> playerbullet;

    private void Awake()
    {
        bulletPoolInstance2 = this;
    }
    private void Start()
    {
        playerbullet = new List<PoolableBullet2>();
        for (int i = 0; i < pooledAmount2; i++)
        {
            PoolableBullet2 pbu = Instantiate(pooledBullet2);
            pbu.bulletDestroy2();
            playerbullet.Add(pbu);
            pbu.gameObject.SetActive(false);

        }

    }

    public PoolableBullet2 GetBullet()
    {

        for (int i = 0; i < playerbullet.Count; i++)
        {
            if (!playerbullet[i].gameObject.activeInHierarchy)
            {
                return playerbullet[i];
            }
        }


        if (willGrow2)
        {
            PoolableBullet2 pbu = Instantiate(pooledBullet2);
            playerbullet.Add(pbu);
            return pbu;
        }
        return null;
    }
}
