using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TelephoneController : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue1;
    [SerializeField] private Dialogue dialogue2;
    [SerializeField] private Dialogue dialogue3;
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject telephone2;
    private TelephoneTrigger telephone2Controller;
    private PlayerController pController;
    [SerializeField] private GameObject batteryObject;
    private DraggableItem battery;

    private void Awake()
    {
        pController = playerController.GetComponent<PlayerController>();
        telephone2Controller = telephone2.GetComponent<TelephoneTrigger>();
        battery = batteryObject.GetComponent<DraggableItem>();
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    private bool isDialogue1Activated;
    private bool isDialogue2Activated;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Character1") && telephone2Controller.IsTelephoneActive())
        {
            if (!isDialogue2Activated)
            {
                isDialogue1Activated = true;
                isDialogue2Activated = true;
                TriggerDialogue(dialogue2);
            }
            else if (battery.IsPlaced())
            {
                TriggerDialogue(dialogue3);
            }
        }
        else if (!isDialogue1Activated && collision.collider.CompareTag("Character1") && pController.IsSwitchUsed())
        {
            TriggerDialogue(dialogue1);
        }
    }

    public bool IsDialogue2Activated() => isDialogue2Activated;
}
