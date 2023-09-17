using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] points;
    [SerializeField] private bool isTrap;
    CharacterController characterController;
    PlayerHealth health;
    private int current;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        health = FindAnyObjectByType<PlayerHealth>();
        characterController = FindAnyObjectByType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != points[current].position)
        {

            transform.position = Vector3.MoveTowards(transform.position, points[current].position, moveSpeed * Time.deltaTime);

        }
        else
        {
            current = (current + 1) % points.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isTrap)
        {
            health.MinusHealth(1f);
            characterController.isBeingHit = true;
        }
    }
}
