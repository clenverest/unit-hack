using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class TelephoneController : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue1;
    [SerializeField] private Dialogue dialogue2;
    [SerializeField] private Dialogue dialogue3;
    [SerializeField] private GameObject telephone2;
    private TelephoneTrigger telephone2Controller;
    [SerializeField] private GameObject batteryObject;
    private DraggableItem battery;
    [SerializeField] private AudioSource callSound;
    [SerializeField] private AudioSource droppedCallSound;
    private DialogueManager dialogueController;
    [SerializeField] private GameObject dialogueManager;
    [SerializeField] private Button button;
    [SerializeField] private GameObject exit;
    private ExitController exitController;

    private void Awake()
    {
        telephone2Controller = telephone2.GetComponent<TelephoneTrigger>();
        battery = batteryObject.GetComponent<DraggableItem>();
        dialogueController = dialogueManager.GetComponent<DialogueManager>();
        exitController = exit.GetComponent<ExitController>(); 
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    private bool isDialogue1Active;
    private bool isDialogue1Activated;
    private bool isDialogue2Activated;
    private bool isDialogue3Activated;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Character1") && telephone2Controller.IsTelephoneActive())
        {
            if (!isDialogue2Activated)
            {
                isDialogue1Activated = true;
                isDialogue2Activated = true;
                callSound.Play();
                TriggerDialogue(dialogue2);
            }
            else if (battery.IsPlaced())
            {
                isDialogue3Activated = true;
                exitController.ChangeExitToEnd();
                callSound.Play();
                TriggerDialogue(dialogue3);
            }
        }
        else if (!isDialogue1Activated && collision.collider.CompareTag("Character1"))
        {
            callSound.Play();
            TriggerDialogue(dialogue1);
            isDialogue1Active = true;
        }
    }

    public bool IsDialogue1Activated() => isDialogue1Active;
    public bool IsDialogue2Activated() => isDialogue2Activated;
    public bool IsDialogue3Activated() => isDialogue3Activated;

}
