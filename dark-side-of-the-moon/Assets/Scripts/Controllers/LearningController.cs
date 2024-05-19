using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class LearningController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject telephone;
    private TelephoneController telephoneController;
    [SerializeField] private GameObject dialogueManager;
    private DialogueManager dialogueController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        telephoneController = telephone.GetComponent<TelephoneController>();
        dialogueController = dialogueManager.GetComponent<DialogueManager>();
    }

    private bool WASDactivated = false;
    private bool Qactivated = false;

    private void Update()
    {
        if (!WASDactivated)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("Next");
                WASDactivated = true;
            }
        }
        else if (!Qactivated && telephoneController.IsDialogue1Activated() && !dialogueController.IsDialogueActive())
        {
            animator.SetTrigger("Next");
            Qactivated = true;
        }
        else if (Qactivated && Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Next");
        }
    }

    private void DestroyLearning()
    {
        Destroy(gameObject);
    }
}
