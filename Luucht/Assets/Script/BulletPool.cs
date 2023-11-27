using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class PoolableBullet : MonoBehaviour
{
    public abstract void bulletDestroy();
}

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstance;

    public PoolableBullet poolableBullet2;
    public PoolableBullet pooledBullet;
    public int pooledAmount;
    public bool willGrow;

    private List<PoolableBullet> bullet;

    private void Awake()
    {
        bulletPoolInstance = this;
    }
    private void Start()
    {
        bullet = new List<PoolableBullet>();
        for (int i = 0; i < pooledAmount; i++)
        {
            PoolableBullet obj = Instantiate(pooledBullet);
            obj.bulletDestroy();
            bullet.Add(obj);
            obj.gameObject.SetActive(false);

        }
        
    }

    public PoolableBullet GetBullet()
    {
        
        for (int i = 0; i < bullet.Count; i++)
        {
            if (!bullet[i].gameObject.activeInHierarchy)
            {
                return bullet[i];
            }
        }
        

        if (willGrow)
        {
            PoolableBullet obj = Instantiate(pooledBullet);
            bullet.Add(obj);
            return obj;
        }
        return null;
    }
}
