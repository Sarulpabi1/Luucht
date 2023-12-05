using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletType2 : PoolableBullet2
{
    private Vector2 moveDirection;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;

    private void OnEnable()
    {
        Invoke("bulletDestroy2", 8f);
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void setMoveDirectionPlayer(Vector2 dir)
    {
        moveDirection = dir;
    }

    public override void bulletDestroy2()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health healthComponent = collision.gameObject.GetComponent<Health>();

            if (healthComponent != null)
            {
                healthComponent.takeDamage(1);
            }
            bulletDestroy2();
        }

    }
}
