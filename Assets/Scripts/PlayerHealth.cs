using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public List<GameObject> Health;
    public bool isDead;

    public void MinusHealth(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (damage > 1)
            {
                for (int i = (int)health + (int)damage -1; i >= health; i--)
                {
                    Debug.Log(i);
                    Health[i].gameObject.active = false;
                }
            }
            else
            {
                Health[(int)health].gameObject.active = false;
            }
        }

        if(health == 0)
        {
            isDead = true;
        }
    }
    private void Start()
    {
        for (int i = 0; i < Health.Count; i++)
        {
            Health[i].gameObject.active = true;
        }
    }


}
