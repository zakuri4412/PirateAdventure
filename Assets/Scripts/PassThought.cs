using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThought : MonoBehaviour
{
    private bool playerIsOn;
    private PlatformEffector2D platformEffector;
    // Start is called before the first frame update
    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
        platformEffector.colliderMask = 1600;
    }

    private void Update()
    {
        if (playerIsOn && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            platformEffector.colliderMask = 1088;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.35f);
        platformEffector.colliderMask = 1600;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player")){
            playerIsOn = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            playerIsOn = false;
        }
    }
}
