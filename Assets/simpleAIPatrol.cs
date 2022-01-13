using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleAIPatrol : MonoBehaviour
{
    bool mustPatrol = true;
    bool mustFlip, canShoot;
    public float speed, range, timeBTWNShoots, shootSpeed;
    private float distToPlayer;

    Rigidbody2D rb;

    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;

    public Transform player, shootPos;

    public GameObject bullet;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mustPatrol = true;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            mustPatrol = false;
            rb.velocity = Vector2.zero;

            if(canShoot)
                StartCoroutine(Shoot());
        }
        else
            mustPatrol = true;
    }


    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    private void Patrol()
    {
        if (mustFlip || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        mustPatrol = true;
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        yield return new WaitForSeconds(timeBTWNShoots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * speed * Time.fixedDeltaTime, 0f);
        canShoot = true; 
    }
}
