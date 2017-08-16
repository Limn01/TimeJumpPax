using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPCDialogue : MonoBehaviour
{
    public Text inputText;
    DialogueManager dm;
    public GameObject dmBox;

    [TextArea(3,10)]
    public string[] sentences;

    public Transform player;

    private void Awake()
    {
        dm = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inputText.text = ("Y");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("YButton"))
            {
                
                if (!dm.dialogueActive)
                {
                    dm.sentences = sentences;
                    dm.currentLine = 0;
                    dm.ShowDialogue();
                    inputText.text = (" ");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inputText.text = (" ");
        }
    }
}
