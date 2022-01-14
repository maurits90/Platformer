using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bullet : MonoBehaviour
{
    public float speed = 8;

    void Update()
    {
        transform.Translate(transform.right * transform.localScale.x * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            return;

        //if (collision.GetComponent<ShootingAction>())
        //    collision.GetComponent<ShootingAction>().Action();

        Destroy(gameObject);
    }
}
