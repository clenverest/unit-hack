using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldBatteryController : MonoBehaviour
{
    [SerializeField] private string password;
    [SerializeField] private GameObject battery;
    [SerializeField] private GameObject score;
    private DraggableItem controller;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        controller = battery.GetComponent<DraggableItem>();
        spriteRenderer = battery.GetComponent<SpriteRenderer>();
    }

    public void PasswordFromInputField(string inputText)
    {
        if (string.Equals(password, inputText.ToLower()))
        {
            score.SetActive(false);
            battery.SetActive(true);
        }
    }


    private void Update()
    {
        if (controller.IsPlaced())
        {
            spriteRenderer.enabled = false;
            enabled = false;
        }

    }
}
