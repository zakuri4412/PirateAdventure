using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemy : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    [SerializeField] List<Enemyhealth> enemy;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Enemyhealth> enemyHealth = enemy.FindAll(x => x.isDead == true);
        if(enemyHealth.Count == enemy.Count)
        {
            boxCollider2D.isTrigger = true;
        }
        else
        {

        }
    }
}
