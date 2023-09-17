using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (playerHealth.health < 3)
            {
                playerHealth.health++;
                playerHealth.Health[(int)playerHealth.health - 1].gameObject.active = true;
                Destroy(gameObject);
            }
        }
    }
}
