using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject icon;
    [SerializeField] GameObject dialogue;
    [SerializeField] private TMP_InputField inputField;
    Animator animatorIcon;
    Animator animatorDialogue;
    private bool isDialogueActive = false;

    private Queue<DialogueNode> speeches;

    private void Start()
    {
        speeches = new Queue<DialogueNode>();
        animatorIcon = icon.GetComponent<Animator>();
        animatorDialogue = dialogue.GetComponent<Animator>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inputField.interactable = false;
        DialogueOn();
        speeches.Clear();
        animatorDialogue.SetBool(Animator.StringToHash("Start"), true);

        foreach (var speech in dialogue.Speeches())
        {
            speeches.Enqueue(speech);
        }
        DisplayNextSpeech();
    }

    private DialogueNode dialogueNode;

    public void DisplayNextSpeech()
    {
        if (isSentenceActive)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueNode.Text();
            animatorIcon.SetTrigger(dialogueNode.Mood());
            isSentenceActive = false;
            return;
        }

        if (speeches.Count == 0)
        {
            EndDialogue();
            return;
        }


        dialogueNode = speeches.Dequeue();
        nameText.text = dialogueNode.Name();
        animatorIcon.SetTrigger(dialogueNode.Mood());
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogueNode.Text()));
    }

    private bool isSentenceActive = false;

    IEnumerator TypeSentence(string sentence)
    {
        isSentenceActive = true;
        dialogueText.text = "";
        foreach (var letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        isSentenceActive = false;
    }

    public void EndDialogue()
    {
        DialogueOff();
        animatorDialogue.SetBool(Animator.StringToHash("Start"), false);
        StartCoroutine(OnInputField());
    }

    private IEnumerator OnInputField()
    {
        yield return new WaitForSeconds(3f);
        inputField.interactable = true;
    }

    public void DialogueOn()
    {
        isDialogueActive = true;
    }

    public void DialogueOff()
    {
        isDialogueActive = false;
    }

    public bool IsDialogueActive() => isDialogueActive;

    public bool IsSpeechesCountIsZero() => speeches.Count == 0;
    public bool IsSpeechesCountIsOne() => speeches.Count == 1;
}
