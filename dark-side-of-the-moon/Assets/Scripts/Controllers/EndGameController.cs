using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    [SerializeField] private GameObject character1;
    [SerializeField] private GameObject character2;
    private CharacterController controller1;
    private CharacterController controller2;
    [SerializeField] private GameObject backlight1;
    [SerializeField] private GameObject backlight2;
    [SerializeField] private Transform endPosition;
    private Animator animator;
    [SerializeField] private GameObject exit;

    private void Start()
    {
        controller1 = character1.GetComponent<CharacterController>();
        controller2 = character2.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private bool isEndGame = false;
    public bool IsEndGame() => isEndGame;

    public void EndGame()
    {
        exit.SetActive(false);
        isEndGame = true;
        if (controller1.IsFreeze())
        {
            controller2.Freeze();
            backlight1.SetActive(true);
            backlight2.SetActive(false);
        }
        else
        {
            controller1.Freeze();
        }
        StartCoroutine(ChangePosition());
    }


    //private void Update()
    //{
    //    if (isEndGame)
    //    {
    //        isEndGame = false;
    //        StartCoroutine(ChangePosition());
    //    }
    //}

    private bool isCoroutineWork = false;

    private IEnumerator ChangePosition()
    {
        isCoroutineWork = true;
        yield return new WaitForSeconds(1);
        var endVector = new Vector2(character1.transform.position.x, endPosition.position.y);
        controller1.EndGame(endVector);
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Next");
        isCoroutineWork = false;
    }


    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
