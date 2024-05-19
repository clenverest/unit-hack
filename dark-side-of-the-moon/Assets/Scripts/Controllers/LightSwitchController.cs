using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LightSwitchController : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue1;
    [SerializeField] private GameObject poster;
    [SerializeField] private GameObject dark;
    [SerializeField] private AudioSource switchOnSound;
    [SerializeField] private AudioSource switchOffSound;
    private PosterController posterController;

    private void Awake()
    {
        posterController = poster.GetComponent<PosterController>();
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character2"))
        {
            if(posterController.IsToyActivated())
            {
                if(dark.activeInHierarchy)
                {
                    switchOnSound.Play();
                    dark.SetActive(false);
                }
                else
                {
                    switchOffSound.Play();
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
