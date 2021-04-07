using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelerPlayer : Player
{
    private bool tookExtraTurn = false;

    // Update is called once per frame
    protected override void Update()
    {
        if (photonView.isMine && ciweilization.activePlayerNumber == playerNumber)
        {
            if (ciweilization.disconnectPanelOn == false)
            {
                GetHeroPowerClick(); 
            }
        }
    }

    void GetHeroPowerClick()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            photonView.RPC("HeroPower", PhotonTargets.AllBuffered);
        }
    }

    /* Count the number of moves the player should get in the current season.*/
    public override double CountMoves()
    {
        if (tookExtraTurn == true)
        {
            tookExtraTurn = false;
            return 0f;
        }

        double count = defaultMoves;

        if (ciweilization.isSpring == true)
        {
            count += CountMovesSpring();
        }
        else if (ciweilization.isSummer == true)
        {
            count += CountMovesSummer();
        }
        else if (ciweilization.isFall == true)
        {
            count += CountMovesFall();
        }
        else if (ciweilization.isWinter == true)
        {
            count += CountMovesWinter();
        }

        //Adding saved moves and reseting saved moves to 0;
        count += savedMoves;
        savedMoves = 0f;

        //If in any case, the player has less than 1 move for a turn, it will have 1 move instead.
        if (count < 1f)
        {
            count = 1f;
        }
        else if (count > maxMoves)
        {
            count = maxMoves;
        }

        return count;
    }
    
    [PunRPC]
    public void HeroPower()
    {
        PlayHeroPowerAudio();
        moves = CountMoves();
        tookExtraTurn = true;
    }
}
