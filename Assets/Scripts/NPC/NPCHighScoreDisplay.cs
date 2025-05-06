using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCHighScoreDisplay : MonoBehaviour, INPCInteractable
{
    public void Interact()
    {
        string[] lines =
        {
            "If you drop into the hole on the left, you can play Flappy Bird.",
            "Have you tried it already? Want to check your high score?"
        };

        List<string> choices = new List<string>
        {
            "Yeah, show me!",
            "Maybe later."
        };

        DialogueManager.Instance.ShowDialogue(lines, choices, OnChoiceSelected);
    }

    private void OnChoiceSelected(int index)
    {
        if(index == 0)
        {
            int bestScore = PlayerPrefs.GetInt("HighScore", 0);
            DialogueManager.Instance.ShowDialogue(new string[]
            {
                $"Your best score is {bestScore}!"
            });
        }
        else
        {
            DialogueManager.Instance.ShowDialogue(new string[]
            {
                "Alright, come back anytime!"
            });
        }
    }
}
