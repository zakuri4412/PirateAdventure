using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScore : MonoBehaviour
{
    [SerializeField] List<GameObject> score;

    private string name;
    // Start is called before the first frame update
    void Start()
    {
        name = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        int _score = PlayerPrefs.GetInt(name);
        if(_score > 0)
        {
            for (int i = 0; i < _score; i++)
            {
                score[i].SetActive(true);
            }

        }
        
    }
}
