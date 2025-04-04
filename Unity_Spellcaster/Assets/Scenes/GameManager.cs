using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public Text deckSizeText;

    private void Start()
    {
        // Ensure all slots are marked available
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            availableCardSlots[i] = true;
        }

        UpdateDeckSizeText(); // Initialize UI on start

        // Ensure deck has enough cards before drawing
        if (deck.Count >= 10)
        {
            StartCoroutine(DrawInitialCards(10, 0.5f)); // Draw 10 cards with a 0.5s delay
        }
        else
        {
            Debug.LogWarning("Not enough cards in the deck to draw 10!");
        }
    }

    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i]) // Ensure slot is available
                {
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);

                    // Prevent UI blocking
                    randCard.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

                    UpdateDeckSizeText();
                    return;
                }
            }
        }
        else
        {
            Debug.LogWarning("Deck is empty! No cards to draw.");
        }
    }

    private IEnumerator DrawInitialCards(int numberOfCards, float delay)
    {
        yield return new WaitForSeconds(0.1f); // Prevents first card being skipped

        for (int i = 0; i < numberOfCards; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(delay);
        }
    }

    private void UpdateDeckSizeText()
    {
        if (deckSizeText != null)
        {
            deckSizeText.text = deck.Count.ToString();
        }
    }
}