using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public int scene;

    public Queue<string> sentences;
    public Queue<string> names;

    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Mouse0))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string name = names.Dequeue();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(name, sentence));
    }

    public void DisplayNextName()
    {
        string name = names.Dequeue();
    }

    IEnumerator TypeSentence (string name, string sentence)
    {
        nameText.text = name;
        dialogueText.text = "";
        char[] carray = sentence.ToCharArray();
        for(int i=0;i<carray.Length;i++)
        {
            dialogueText.text += carray[i];
            if (i % 2 == 0)
                yield return null;
        }

        //foreach(char letter in sentence.ToCharArray())
        //{
            //dialogueText.text += letter;
           // yield return null;
        //}
    }

    public void EndDialogue()
    {
        SceneManager.LoadScene(scene);
    }
}