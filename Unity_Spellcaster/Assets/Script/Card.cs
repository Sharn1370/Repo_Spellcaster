using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public char assignedLetter;  // Public variable to hold the letter assigned to the card

    // Set this assigned letter manually instead of choosing randomly.
    public void SetAssignedLetter(char letter)
    {
        assignedLetter = letter;
        // Optionally, you can display the assigned letter on the card if desired
        // GetComponentInChildren<TMP_Text>().text = assignedLetter.ToString();
    }

    // If you want to allow changing the letter externally, you can still use this method for other purposes.
    private char GetRandomAllowedLetter()
    {
        string allowedLetters = "AEIOTRSPLC";  // Allowed letters
        // Instead of random selection, just return the assignedLetter.
        return assignedLetter;  // Just return the assigned letter
    }

    public void OnCardClicked()
    {
        // Log the click and notify GameManager with the assigned letter
        Debug.Log($"Card {assignedLetter} clicked!");
        GameManager.Instance.OnCardPressed(assignedLetter);  // Notify GameManager to process the letter
    }
}