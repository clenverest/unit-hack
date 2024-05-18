using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private bool dragging;
    private bool placed;
    private Vector2 offset;
    private Vector2 originalPosition;
    private Collider2D collider;
    [SerializeField] private GameObject slot;

    private void Awake()
    {
        originalPosition = transform.position;
        collider = GetComponent<Collider2D>();
    }

    public bool IsPlaced() => placed;

    private void OnMouseDown()
    {
        if(!placed)
        {
            collider.enabled = false;
            dragging = true;
            offset = GetMousePosition() - (Vector2)transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (!placed)
        {
            var mousePos = GetMousePosition();
            transform.position = mousePos - offset;
        }
    }

    private void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, slot.transform.position) < 2)
        {
            transform.position = slot.transform.position;
            placed = true;
        }
        else
        {
            transform.position = originalPosition;
            dragging = false;
        }
        collider.enabled = true;
    }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
