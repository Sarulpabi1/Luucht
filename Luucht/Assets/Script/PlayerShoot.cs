using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 10;
    [SerializeField] private float startAngle = 0f;
    [SerializeField] private float rotationSpeed = 10f;
    private Vector2 bulletMoveDirection;

    private void Start()
    {
        InvokeRepeating("FireThreeStraight", 0f, 0.2f);
    }

    private void FireThreeStraight()
    {
        float angle = startAngle;

        for (int i = 0; i < 3; i++) 
        {
            
            Vector2 bulDir = Vector2.up;

            
            PoolableBullet2 bul = BulletPool2.bulletPoolInstance2.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.gameObject.SetActive(true);
            bul.GetComponent<BulletType2>().setMoveDirectionPlayer(bulDir);

            angle += 120f; 
        }
    }



}
