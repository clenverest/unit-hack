using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneTrigger : MonoBehaviour
{
    private bool isTelephoneActive;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Character2"))
        {
            isTelephoneActive = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Character2"))
        {
            isTelephoneActive = false;
        }
    }

    public bool IsTelephoneActive() => isTelephoneActive;
}
