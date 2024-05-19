using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject endController;
    private EndGameController endGameController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        endGameController = endController.GetComponent<EndGameController>();
    }

    public void ChangeExitToEnd()
    {
        isEnd = true;
    }

    private bool isEnd = false;

    public void EndGame()
    {
        endGameController.EndGame();
    }

    public void Push()
    {
        animator.SetTrigger("Push");
    }

    public void ExitGame()
    {
        if(isEnd)
        {
            EndGame();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
