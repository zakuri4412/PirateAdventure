using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterController characterSrpit;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject winCanvas;

    PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        characterSrpit.enabled = false;
        gameOverCanvas.active = false;
        winCanvas.active = false;
        health = FindAnyObjectByType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("EnableScript", 1.03f);
        if (health.isDead)
        {
            gameOverCanvas.active = true;
        }
        if (characterSrpit.isWin)
        {
            winCanvas.active = true;
        }
    }

    private void EnableScript()
    {
        characterSrpit.enabled = true;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Home");
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount)
        {
            MainMenu();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
