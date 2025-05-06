using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Transform choiceGroup;
    [SerializeField] private Button choiceButtonPrefab;

    private Queue<string> lines = new Queue<string>();
    private bool waitingForChoice = false;
    private System.Action<int> choiceCallback;

    public bool IsDialogueActive => dialoguePanel.activeSelf;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string[] dialogueLines, List<string> choices = null, System.Action<int> onChoiceSelected = null)
    {
        dialoguePanel.SetActive(true);
        lines.Clear();

        foreach(var line in dialogueLines)
        {
            lines.Enqueue(line);
        }

        choiceCallback = onChoiceSelected;
        waitingForChoice = false;

        dialogueText.text = string.Empty;

        ClearChoices();

        StartCoroutine(ShowLinesThenChoices(choices));
    }

    [SerializeField] private TextMeshProUGUI hintText;  // Inspector¿¡¼­ ÇÒ´ç

    private IEnumerator ShowLinesThenChoices(List<string> choices)
    {
        while (lines.Count > 0)
        {
            dialogueText.text = lines.Dequeue();
            if (hintText != null) hintText.gameObject.SetActive(true);  // ÈùÆ® Ç¥½Ã
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            if (hintText != null) hintText.gameObject.SetActive(false); // ÈùÆ® ¼û±è
        }

        if (choices != null && choices.Count > 0)
        {
            waitingForChoice = true;

            for (int i = 0; i < choices.Count; i++)
            {
                int index = i;
                Button btn = Instantiate(choiceButtonPrefab, choiceGroup);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = $"{index + 1}. {choices[i]}";
                btn.onClick.AddListener(() =>
                {
                    HideDialogue();
                    choiceCallback?.Invoke(index);
                });
            }

            bool choiceMade = false;
            while (!choiceMade)
            {
                for (int i = 0; i < choices.Count; i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    {
                        choiceGroup.GetChild(i).GetComponent<Button>().onClick.Invoke();
                        choiceMade = true;
                        break;
                    }
                }
                yield return null;
            }
        }
        else
        {
            HideDialogue();
        }
    }



    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        ClearChoices();
    }

    private void ClearChoices()
    {
        foreach (Transform child in choiceGroup)
        {
            Destroy(child.gameObject);
        }
    }

}
