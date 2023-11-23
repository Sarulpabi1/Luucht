using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 10;
    [SerializeField] private float startAngle = 0f, endAngle = 360f;

    private Vector2 bulletMoveDirection;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 bulMoveVector = new Vector2(bulDirX, bulDirY);
            Vector2 bulDir = (bulMoveVector - (Vector2)transform.position).normalized;

            GameObject bul = BulletSpawner.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().setMoveDirection(bulDir);

            angle += angleStep;
        }
    }
}
