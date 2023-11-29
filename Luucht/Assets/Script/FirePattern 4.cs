using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePattern4 : MonoBehaviour
{
    [SerializeField] private int petals = 8;
    [SerializeField] private int bulletsPerPetal = 10;
    [SerializeField] private float petalRadius = 5f;

    private void Start()
    {
        InvokeRepeating("Fire", 2f, 2f);
    }

    private void Fire()
    {
        float angleStep = 360f / petals;

        for (int i = 0; i < petals; i++)
        {
            float angle = angleStep * i;

            for (int j = 0; j < bulletsPerPetal; j++)
            {
                float bulletDirX = transform.position.x + Mathf.Sin(Mathf.Deg2Rad * angle) * petalRadius * (j + 1);
                float bulletDirY = transform.position.y + Mathf.Cos(Mathf.Deg2Rad * angle) * petalRadius * (j + 1);

                Vector2 bulletMoveVector = new Vector2(bulletDirX, bulletDirY);
                Vector2 bulletDirection = (bulletMoveVector - (Vector2)transform.position).normalized;

                PoolableBullet bullet = BulletPool.bulletPoolInstance.GetBullet();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
                bullet.gameObject.SetActive(true);
                bullet.GetComponent<Bullet>().setMoveDirection(bulletDirection);
            }
        }
    }
}