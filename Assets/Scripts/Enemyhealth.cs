using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhealth : MonoBehaviour
{
    [SerializeField] public float health;
    Animator animator;
    public bool isDead;

    public void minusHealth(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        if (health == 0)
        {
            isDead = true;
        }
    }
}
