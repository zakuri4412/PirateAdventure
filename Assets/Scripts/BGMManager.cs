using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource deadMusic;

    PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        health= FindAnyObjectByType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isDead)
        {
            if (!gameMusic.isPlaying)
            {
                gameMusic.Play();
                deadMusic.Stop();
            }
            
        }
        else
        {
            ;
            if (!deadMusic.isPlaying)
            {
                gameMusic.Stop();
                deadMusic.Play();
            }
            
            
        }
    }
}
