using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LightSwitchController : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue1;
    [SerializeField] private GameObject telephone;
    [SerializeField] private GameObject dark;
    private TelephoneController telephoneController;

    private void Awake()
    {
        telephoneController = telephone.GetComponent<TelephoneController>();
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    private bool isDialogue1Activated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character2"))
        {
            if(telephoneController.IsDialogue2Activated())
            {
                if(dark.activeInHierarchy)
                {
                    dark.SetActive(false);
                }
                else
                {
                    dark.SetActive(true);
                }
            }
            else
            {
                TriggerDialogue(dialogue1);
            }
        }
    }
}
