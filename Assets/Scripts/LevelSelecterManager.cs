using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecterManager : MonoBehaviour
{
    [SerializeField] private List<Button> lvlBtn;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        for(int i =0 ; i < lvlBtn.Count; i++)
        {
            if (i + 1 > levelAt)
            {
                lvlBtn[i].interactable = false;
            }
        }
    }
}
