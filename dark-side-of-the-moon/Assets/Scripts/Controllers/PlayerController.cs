using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject character1;
    [SerializeField] private GameObject character2;
    private CharacterController controller1;
    private CharacterController controller2;
    [SerializeField] private GameObject backlight1;
    [SerializeField] private GameObject backlight2;
    private bool isSwitchUsed;
    [SerializeField] private GameObject telephone;
    private TelephoneController telephoneController;
    [SerializeField] private GameObject endController;
    private EndGameController endGameController;

    private void Start()
    {
        controller1 = character1.GetComponent<CharacterController>();
        controller2 = character2.GetComponent<CharacterController>();
        telephoneController = telephone.GetComponent<TelephoneController>();
        endGameController = endController.GetComponent<EndGameController>();
    }

    private void Update()
    {
        if (!telephoneController.IsDialogue1Activated() || endGameController.IsEndGame())
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            isSwitchUsed = true;
            if (controller1.IsFreeze())
            {
                controller1.Unfreeze();
                controller2.Freeze();
                backlight1.SetActive(true);
                backlight2.SetActive(false);
            }
            else
            {
                controller2.Unfreeze();
                controller1.Freeze();
                backlight2.SetActive(true);
                backlight1.SetActive(false);
            }
        }
    }

    public bool IsSwitchUsed() => isSwitchUsed;
}
