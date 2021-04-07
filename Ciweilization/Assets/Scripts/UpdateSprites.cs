using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprites : MonoBehaviour
{

    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private Ciweilization ciweilization;

    // Start is called before the first frame update
    void Start()
    {
        ciweilization = FindObjectOfType<Ciweilization>();

        List<string> deck1 = Ciweilization.GenerateDeck("1", 1);
        List<string> deck2 = Ciweilization.GenerateDeck("2", 1);
        List<string> deck3 = Ciweilization.GenerateDeck("3", 1);
        List<string> deck4 = Ciweilization.GenerateDeck("4", 1);
        List<string> deckHeroes = ciweilization.GenerateHeroDeck();
        List<string> deckChances = ciweilization.GenerateChanceDeck();

        int i = 0;
        foreach (string card in deck1)
        {
            if (this.name == card)
            {
                cardFace = ciweilization.cardFaces1[i];
                break;
            }
            i++;
        }

        i = 0;
        foreach (string card in deck2)
        {
            if (this.name == card)
            {
                cardFace = ciweilization.cardFaces2[i];
                break;
            }
            i++;
        }

        i = 0;
        foreach (string card in deck3)
        {
            if (this.name == card)
            {
                cardFace = ciweilization.cardFaces3[i];
                break;
            }
            i++;
        }

        i = 0;
        foreach (string card in deck4)
        {
            if (this.name == card)
            {
                cardFace = ciweilization.cardFaces4[i];
                break;
            }
            i++;
        }

        i = 0;
        foreach (string card in deckHeroes)
        {
            if (this.name == card)
            {
                cardFace = ciweilization.cardFacesHeroes[i];
                break;
            }
            i++;
        }

        i = 0;
        foreach (string card in deckChances)
        {
            if (this.name == card)
            {
                cardFace = ciweilization.cardFacesChances[i];
                break;
            }
            i++;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = cardFace;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
