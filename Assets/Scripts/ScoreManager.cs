using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int _score;
    [SerializeField] List<GameObject> Coins;

    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
        characterController = FindAnyObjectByType<CharacterController>();   


    }
    public void AddScore(int score)
    {
        _score += score;
    }
    // Update is called once per frame
    void Update()
    {
        if (characterController.isWin)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) < _score)
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, _score);
            }
        }

        for (int i = 0; i < _score; i++)
        {
            Coins[i].SetActive(true);
        }
    }
}
