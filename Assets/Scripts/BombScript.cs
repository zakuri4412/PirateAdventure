using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private float fieldOfImpact;
    [SerializeField] private float force;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private float damage;
    [SerializeField] private float timeExplose;

    Animator animator;
    PlayerHealth playerHealth;
    Enemyhealth enemyHealth;
    float timer;
    CharacterController characterController;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        playerHealth = GameObject.Find("Ducci").GetComponent<PlayerHealth>();
        enemyHealth = FindAnyObjectByType<Enemyhealth>();
        characterController = FindAnyObjectByType<CharacterController>();
        timer = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Explode());
        timer += Time.deltaTime;
        if (timer >= timeExplose)
        {
            Explode();
            timer = 0f;
        }

    }


    public void Explode()
    {
        audioSource.Play();
        animator.SetBool("isExplosion", true);
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            if (obj.gameObject.name.Equals("Ducci"))
            {
                playerHealth.MinusHealth(damage);
                obj.GetComponent<Rigidbody2D>().AddForce(direction * (force + 1500f));
                characterController.isBeingHit = true;
            }
            if (obj.gameObject.name.Contains("Enemy"))
            {
                enemyHealth.minusHealth(damage);
            }
            else
            {
                obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            }

        }
        Destroy(gameObject,0.8f);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
