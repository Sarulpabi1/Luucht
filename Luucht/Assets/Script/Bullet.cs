using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : PoolableBullet
{
    private Vector2 moveDirection;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;

    private void OnEnable()
    {
        Invoke("bulletDestroy", 8f);
    }

  //private void Start()
  //{
  //    moveSpeed = 5f;
  //}

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void setMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    public override void bulletDestroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health healthComponent = collision.gameObject.GetComponent<Health>();

            if (healthComponent != null)
            {
                healthComponent.takeDamage(1);
            }
            bulletDestroy();
        }

    }
}
