using System.Collections;
using TMPro;
using UnityEngine;

public class IntroCutsceneController : MonoBehaviour
{
    [SerializeField] CanvasGroup cutsceneGroup;
    [SerializeField] TextMeshProUGUI cutsceneText;
    [SerializeField] GameObject promptText; // Press ENTER to continue
    [SerializeField] float typingSpeed = 0.03f;
    [SerializeField] GameObject managers;

    [TextArea(5, 10)]
    public string fullCutsceneText =
        "Welcome to the edge of the galaxy.\n" +
        "You've just opened your first space diner.\n" +
        "Serve robots from every corner of the universe.\n" +
        "Grow your restaurant into a galactic empire.\n" +
        "Let the journey begin.";

    private bool finishedTyping = false;

    private void Start()
    {
        managers.SetActive(false);
        cutsceneGroup.alpha = 1;
        cutsceneGroup.blocksRaycasts = true;
        cutsceneText.text = "";
        promptText.SetActive(false);
        StartCoroutine(TypeText(fullCutsceneText));
    }

    private void Update()
    {
        if (finishedTyping && Input.GetKeyDown(KeyCode.Return))
        {
            EndCutscene();
        }
    }

    IEnumerator TypeText(string fullText)
    {
        foreach (char c in fullText)
        {
            cutsceneText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        finishedTyping = true;
        promptText.SetActive(true);
    }

    private void EndCutscene()
    {
        managers.SetActive(true);
        cutsceneGroup.alpha = 0;
        cutsceneGroup.blocksRaycasts = false;
        promptText.SetActive(false);
        enabled = false;
    }
}
