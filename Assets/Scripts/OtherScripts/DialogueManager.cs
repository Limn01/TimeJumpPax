using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public bool dialogueActive;
    public float typeSpeed;

    public string[] sentences;
    public int currentLine;

    bool isTyping = false;
    bool cancelTyping = false;
    Player player;
 
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (dialogueActive && Input.GetButtonDown("YButton"))
        {
            if (!isTyping)
            {
                currentLine++;

                if (currentLine >= sentences.Length)
                {
                    dialogueBox.SetActive(false);
                    dialogueActive = false;

                    currentLine = 0;
                    player.enabled = true;
                }
                else
                {
                    StartCoroutine(TextScroll(sentences[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        dialogueText.text = "";
        isTyping = true;
        cancelTyping = false;

        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            dialogueText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        dialogueText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
        StartCoroutine(TextScroll(sentences[currentLine]));
    }

    public void ShowDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        player.enabled = false;
    }
}
