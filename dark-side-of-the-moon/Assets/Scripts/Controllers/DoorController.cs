using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject playerController;
    private PlayerController pController;
    [SerializeField] Dialogue dialogue;

    private void Awake()
    {
        pController = playerController.GetComponent<PlayerController>();
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Character2"))
        {
            if (room1.activeInHierarchy)
            {
                room1.SetActive(false);
                room2.SetActive(true);
            }
            else
            {
                room2.SetActive(false);
                room1.SetActive(true);
            }
        }
        else if (collision.collider.CompareTag("Character1") && pController.IsSwitchUsed())
        {
            TriggerDialogue(dialogue);
        }
    }
}
