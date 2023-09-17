using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    Dialogue dialogue;
    AudioSource audioSource;

    public bool playerIsIn;
    private void Start()
    {
        dialogue = FindAnyObjectByType<Dialogue>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsIn = true;
            dialogue.StartDialog();
            if(audioSource != null)
            {
                audioSource.Play();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsIn = false;
            dialogue.EndDialog();
            audioSource.Stop();
        }
    }
}
