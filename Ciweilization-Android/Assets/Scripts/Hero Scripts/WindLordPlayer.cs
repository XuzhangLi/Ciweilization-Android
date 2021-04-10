using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindLordPlayer : Player
{
    public override void PlayerAtTheStartOfTurn()
    {
        // Dealing with the free chance given by Wonder Y4.
        if (freeChance && photonView.isMine)
        {
            photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Wonder Ability");
            clickChanceOnly = true;
            StartCoroutine(ciweilization.CiweilizationDealChances());
            photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Coin");
        }
        freeChance = false;

        //WindLord Hero Power
        StartCoroutine(ciweilization.CiweilizationResetBoard());
    }
}
