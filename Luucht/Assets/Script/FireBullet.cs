using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[System.Serializable]
public class FireBullet : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 10;
    [SerializeField] private float startAngle = 0f, endAngle = 360f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private int numberOfBranches = 3;

    private Vector2 bulletMoveDirection;

    public void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount; 
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 bulMoveVector = new Vector2(bulDirX, bulDirY);
            Vector2 bulDir = (bulMoveVector - (Vector2)transform.position).normalized;

            PoolableBullet bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.gameObject.SetActive(true);
            bul.GetComponent<Bullet>().setMoveDirection(bulDir);

            angle += angleStep;
        }
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void Fire2()
    {
        int nbOfBranches = 2 * numberOfBranches;
        
        float angle = startAngle;
        for (int i = 0; i < nbOfBranches; i++)
        {
            float currentAngle = angle + (360f / nbOfBranches) * i;

            float bulDirX = transform.position.x + Mathf.Sin((currentAngle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((currentAngle * Mathf.PI) / 180f);

            Vector2 bulMoveVector = new Vector2(bulDirX, bulDirY);
            Vector2 bulDir = (bulMoveVector - (Vector2)transform.position).normalized;

            PoolableBullet bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.gameObject.SetActive(true);
            bul.GetComponent<Bullet>().setMoveDirection(bulDir);

            angle += (360f / nbOfBranches) * i;
        }
        angle += 10f;

        if (angle >= 360f)
        {
            angle = 0f;
        }
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void Fire3()
    {
        float rs = 3f * rotationSpeed;
        float angle = startAngle;
        for (int i = 0; i < numberOfBranches; i++)
        {
            float currentAngle = angle + (360f / numberOfBranches) * i;

            float bulDirX = transform.position.x + Mathf.Sin((currentAngle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((currentAngle * Mathf.PI) / 180f);

            Vector2 bulMoveVector = new Vector2(bulDirX, bulDirY);
            Vector2 bulDir = (bulMoveVector - (Vector2)transform.position).normalized;

            PoolableBullet bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.gameObject.SetActive(true);
            bul.GetComponent<Bullet>().setMoveDirection(bulDir);

            angle += (360f / numberOfBranches) * i;
        }
        angle += 10f;

        if (angle >= 360f)
        {
            angle = 0f;
        }
        transform.Rotate(Vector3.forward * rs * Time.deltaTime);
    }

    public void AttackRandom()
    {
        
        int randomPattern = Random.Range(1, 3);
        CancelInvoke();
        switch (randomPattern)
        {
            case 1:
                InvokeRepeating("Fire2", 0f, 0.2f);
                break;
            case 2:
                InvokeRepeating("Fire3", 0f, 0.2f);
                break;
            default:
                Debug.Log("Pattern non reconnu !");
                break;
        }
    }

}
