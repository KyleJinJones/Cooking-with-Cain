﻿using System.Collections;
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

    void Start()
    {
        sentences = new Queue<string>();
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
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
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

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
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