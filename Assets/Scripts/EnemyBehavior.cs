using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float attackDistace;
    [SerializeField] private LayerMask player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timer;
    [SerializeField] private Transform[] points;
    [SerializeField] private BoxCollider2D detectedArea;
    [SerializeField] private BoxCollider2D playerCollider;

    private int current;
    private bool inRange;
    private GameObject target;
    private float distace;
    private Animator animator;
    Enemyhealth enemyhealth;
    Rigidbody2D Rigidbody2D;
    PlayerHealth playerHealth;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        enemyhealth = GetComponent<Enemyhealth>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        characterController = FindAnyObjectByType<CharacterController>();
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyhealth.isDead)
        {
            if (PlayerIsIn() && !playerHealth.isDead)
            {
                animator.SetBool("isMove", true);
                EnemyLogic();
                RotateTowards(target.transform.position);

            }
            else
            {
                Physics2D.IgnoreCollision(playerCollider, gameObject.GetComponent<BoxCollider2D>());
                StopAttack();
                animator.SetBool("isMove", true);
                //transform.position = new Vector2(transform.position.x, transform.position.y);
                if (transform.position != points[current].position)
                {

                    transform.position = Vector3.MoveTowards(transform.position, points[current].position, moveSpeed * Time.deltaTime);

                }
                else
                {
                    current = (current + 1) % points.Length;
                }
                RotateTowards(points[current].position);
            }
        }
        else
        {
            Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            Rigidbody2D.freezeRotation = true;
            animator.SetBool("isDead", enemyhealth.isDead);
            Physics2D.IgnoreCollision(playerCollider,gameObject.GetComponent<BoxCollider2D>());
            
        }
        
    }

    private void RotateTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.up * angle);
    }


    private void EnemyLogic()
    {
        //Debug.Log(transform.parent.position);
        distace = Vector2.Distance(transform.position, target.transform.position);
        if(distace> attackDistace )
        {
            Move();
            StopAttack();
        }
        else if(distace <= attackDistace)
        {
             Attack();
        }

    }

    private void Attack()
    {
        
        animator.SetBool("isMove", false);
        animator.SetBool("isAttack", true);
    }

    private void StopAttack()
    {
        animator.SetBool("isAttack", false);
    }

    private void Move()
    {
        Vector2 targetPos = new Vector2(target.transform.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }


    private bool PlayerIsIn()
    {
        target = playerCollider.gameObject;
        return detectedArea.IsTouching(playerCollider);
    }

    private void DealDamage()
    {
        characterController.isBeingHit = true;
        playerHealth.MinusHealth(1f);
    }
}
