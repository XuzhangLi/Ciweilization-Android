using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientKittyPlayer : Player
{
    public override void PlayerStartRound()
    {
        chances = defaultChances;

        if (moves == 0)
        {
            moves = CountMoves();
        }
        else //The Patient Kitty saves all used moves with a cost of 0.25 moves.
        {
            moves += CountMoves() - 0.25f;
        }

    }
}
