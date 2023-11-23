using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FirePattern2 : MonoBehaviour
{
    [SerializeField] private float angle = 0f; 
    private Vector2 bulletMoveDirection;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, 0.2f);
    }

    private void Fire()
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

        angle += 10f;
    }
}
