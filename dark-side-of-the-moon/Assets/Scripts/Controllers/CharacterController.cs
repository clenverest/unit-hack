using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;
    private float movementSpeed;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private DialogueManager dialogueManager;
    private bool isActive = true;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isActive && !isFreeze)
        {
            direction = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        movementSpeed = Mathf.Clamp(direction.magnitude, 0.0f, 1.0f);
        direction.Normalize();
        FlipPlayer();
        Animate();
        if (dialogueManager != null)
        {
            InteractionWithDialog();
        }

        if (isEndGame)
        {
            MoveToPosition();
        }
    }

    private void FixedUpdate()
    {
        if (isActive && !isFreeze)
            rigidbody.MovePosition(rigidbody.position + direction * speed * Time.fixedDeltaTime);
    }

    private bool facingRight = false;
    private void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void FlipPlayer()
    {
        if (facingRight == false && direction.x > 0)
        {
            Flip();
        }
        else if (facingRight == true && direction.x < 0)
        {
            Flip();
        }
    }

    private void Animate()
    {
        if (direction != Vector2.zero)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        animator.SetFloat("Speed", movementSpeed);
    }

    private void InteractionWithDialog()
    {
        if (dialogueManager.IsDialogueActive())
        {
            isActive = false;
            direction = Vector2.zero;
        }
        //else if (direction != Vector2.zero)
        //{
        //    animator.SetBool("IsDialogueActive", false);
        //}
        else
        {
            isActive = true;
        }
    }

    [SerializeField] private bool isFreeze = true;

    public bool IsFreeze() => isFreeze;

    public void Freeze()
    {
        isFreeze = true;
        direction = Vector2.zero;
    }

    public void Unfreeze()
    {
        isFreeze = false;
    }

    private bool isEndGame = false;
    private Vector2 endPosition;

    public void EndGame(Vector2 position)
    {
        isEndGame = true;
        endPosition = position;
    }

    private void MoveToPosition()
    {
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        transform.position = Vector2.MoveTowards(transform.position, endPosition, Time.fixedDeltaTime);
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);
        animator.SetFloat("Speed", 1);
    }
}
