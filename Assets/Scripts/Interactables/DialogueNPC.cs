using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueNPC : Interactable
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private bool hasInteracted = false;

    private int index;

    IEnumerator TypeLine()
    {
        //type each character 1 by 1
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        if(hasInteracted)
            StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasInteracted)
        {
            
            if(textComponent.text == lines[index])
            {
                if(Input.GetMouseButtonDown(0))
                {
                    NextLine();
                    Debug.Log("I am here");
                }
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                Debug.Log("Lines index: " + index);
                if(Input.GetMouseButtonDown(0))
                    NextLine();
            }
        }
    }


    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());

    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            index = 0;
            hasInteracted = false;
            textComponent.enabled = false; // svinei text
        }
    }

    protected override void Interact()
    {
        if(!this.enabled)
        {
            hasInteracted = true;
            textComponent.enabled = true;
            textComponent.text = "Cant Interact yet";
            StartCoroutine(ShowMessage());
            return;
        }
        Debug.Log("text : interacted");
        hasInteracted = true;
        textComponent.enabled = true; // anoigei text
    }

    public override string getPromptMessage()
    {
        if(hasInteracted)
            return string.Empty;
        else
            return promptMessage;
    }

    public IEnumerator ShowMessage()
    {
        yield return new WaitForSeconds(2);
        textComponent.enabled = false;
        hasInteracted = false;
    }

}
