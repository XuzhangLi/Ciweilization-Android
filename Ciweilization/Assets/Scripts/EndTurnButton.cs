using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : Photon.MonoBehaviour
{
    Ciweilization ciweilization;
    AudioManager audioManager;


    // Start is called before the first frame update
    private void Start()
    {
        ciweilization = GetComponent<Ciweilization>();

        audioManager = FindObjectOfType<AudioManager>();

        //activePlayerText = GameObject.Find("Canvas/Active Player Text").GetComponent<TextMeshProUGUI>();
        //activePlayerText.text = "player 1's turn!";
    }


    public void OnButtonPress()
    {
        if (ciweilization.activePlayerNumber == 1 && (ciweilization.player1 
            && ciweilization.player1.photonView.isMine))
        {
            photonView.RPC("NextActivePlayer", PhotonTargets.AllBuffered);
        }
        else if (ciweilization.activePlayerNumber == 2 && (ciweilization.player2 
            && ciweilization.player2.photonView.isMine))
        {
            photonView.RPC("NextActivePlayer", PhotonTargets.AllBuffered);
        }
        else if (ciweilization.activePlayerNumber == 3 && (ciweilization.player3 
            && ciweilization.player3.photonView.isMine))
        {
            photonView.RPC("NextActivePlayer", PhotonTargets.AllBuffered);
        }
        else
        {
            Debug.Log("You can't end turn when its not your turn!");
        }
    }

    /* Pass active player to the next player.*/
    [PunRPC]
    public void NextActivePlayer()
    {
        //Play end turn sound for the client.
        audioManager.Play("End Turn");

        // Change active player number.
        ciweilization.activePlayerNumber =
                (ciweilization.activePlayerNumber % ciweilization.playerCount) + 1;

            // Trigger player start of turn abilities;
            //    deal heroes on turn 0.
            if (ciweilization.activePlayerNumber == 1)
            {
                ciweilization.CiweilizationNextTurn();
                ciweilization.player1.PlayerAtTheStartOfTurn();
            }
            else if (ciweilization.activePlayerNumber == 2)
            { 
                if (ciweilization.turn == 0)
                {
                    StartCoroutine(ciweilization.CiweilizationDealHeroes(2));
                }
                ciweilization.player2.PlayerAtTheStartOfTurn();
            }
            else if (ciweilization.activePlayerNumber == 3)
            {
                if (ciweilization.turn == 0)
                {
                    StartCoroutine(ciweilization.CiweilizationDealHeroes(3));
                }
                ciweilization.player3.PlayerAtTheStartOfTurn();
            }
            else
            {
                Debug.Log("Error! Invalid active player number!");
            }
    }

    public void DealHeroesOnTurnZero()
    {
        if (ciweilization.turn == 0)
        {
            if (ciweilization.activePlayerNumber == 1)
            {
                StartCoroutine(ciweilization.CiweilizationDealHeroes(2));
            }
            else if (ciweilization.activePlayerNumber == 2)
            {
                StartCoroutine(ciweilization.CiweilizationDealHeroes(3));
            }
            else if (ciweilization.activePlayerNumber == 3)
            {

            }
        }
    }

}
