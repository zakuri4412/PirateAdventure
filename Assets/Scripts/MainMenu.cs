using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject gameMenuCanvas;
    [SerializeField] GameObject playerSelectCanvas;
    // Start is called before the first frame update
    void Start()
    {
        gameMenuCanvas.active = true;
        playerSelectCanvas.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void GameSelected()
    {
        gameMenuCanvas.active = false;
        playerSelectCanvas.active = true;
    }

    public void Back()
    {
        gameMenuCanvas.active = true;
        playerSelectCanvas.active = false;
    }
}
