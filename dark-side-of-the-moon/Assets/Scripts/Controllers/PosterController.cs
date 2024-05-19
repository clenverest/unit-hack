using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PosterController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Dialogue dialogue1;
    [SerializeField] private AudioSource posterSound;
    [SerializeField] private AudioSource takeSound;

    private int pushCounter = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnMouseDown()
    {
        animator.SetTrigger("Next");
        pushCounter++;

        if (pushCounter == 1)
        {
            posterSound.Play();
        }
        else if (pushCounter == 2)
        {
            takeSound.Play();
            TriggerDialogue(dialogue1);
        }
    }

    public bool IsToyActivated() => pushCounter >= 2;
}
