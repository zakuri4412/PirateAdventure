using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject windowDialog;
    [SerializeField] private List<string> dialogues;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private float dialogSpeed;

    private int index;
    private int charIndex;
    private bool isStart;
    private bool waitNextsentece;
    DialogTrigger dialogTrigger;

    private void Start()
    {
        windowDialog.SetActive(false);
        dialogTrigger = FindAnyObjectByType<DialogTrigger>();
    }
    public void StartDialog()
    {
        if (isStart)
        {
            return;
        }
        isStart = true;
        windowDialog.SetActive(true);
        GetDialog(0);
    }

    private void GetDialog(int i)
    {
            index = i;
            charIndex = 0;
            dialogText.text = string.Empty;
            StartCoroutine(WritingDialog());
        
        
    }

    public void EndDialog()
    {
        index = 0; 
        dialogText.text = string.Empty;
        charIndex = 0;
        isStart = false;
        windowDialog.SetActive(false);
    }

    private IEnumerator WritingDialog()
    {
        if (dialogTrigger.playerIsIn && index >= 0 )
        {
            string currentDialog = dialogues[index];
            dialogText.text += currentDialog[charIndex];
            charIndex++;
            if (charIndex < currentDialog.Length)
            {
                yield return new WaitForSeconds(dialogSpeed);
                StartCoroutine(WritingDialog());
            }
            else
            {
                waitNextsentece = true;
            }
        }
        
        
    }

    private void Update()
    {
        if (!isStart)
        {
            return;
        }
        
        if (waitNextsentece)
        {
            waitNextsentece = false;
            index++;
            StartCoroutine(NextDialog());
        }
    }

    IEnumerator NextDialog()
    {
        yield return new WaitForSeconds(1f);
        if (index < dialogues.Count)
        {
            GetDialog(index);
        }
    }

}
