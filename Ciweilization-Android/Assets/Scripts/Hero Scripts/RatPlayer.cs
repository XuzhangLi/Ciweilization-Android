using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPlayer : Player
{
    /* Count the number of moves the player should get in the current season.*/
    public override double CountMoves()
    {
        int wonderLevel2 = PlayerGetWonder_G2() + PlayerGetWonder_R2()
                + PlayerGetWonder_Y2() + PlayerGetWonder_B2();
        int wonderLevel3 = PlayerGetWonder_G3() + PlayerGetWonder_R3()
                + PlayerGetWonder_Y3() + PlayerGetWonder_B3();
        int wonderLevel4 = PlayerGetWonder_G4() + PlayerGetWonder_R4()
                + PlayerGetWonder_Y4() + PlayerGetWonder_B4();
        int wonderTotal = wonderLevel2 + wonderLevel3 + wonderLevel4;

        double count = defaultMoves;

        if (wonderTotal >= 4)
        {
            count += 1;
        }

        if (ciweilization.isSpring == true)
        {
            count += CountMovesSpring();
        }

        else if (ciweilization.isSummer == true)
        {
            count += CountMovesSummer();

            count += PlayerGetWonder_G2() * 0.25f;
            count += PlayerGetWonder_Y2() * 0.25f;
            count += PlayerGetWonder_B2() * 0.25f;
            count += PlayerGetWonder_G3() * 0.5f;
            count += PlayerGetWonder_Y3() * 0.5f;
            count += PlayerGetWonder_B3() * 0.5f;
        }
        else if (ciweilization.isFall == true)
        {
            count += CountMovesFall();

            count += PlayerGetWonder_G3() * 0.25f;
            count += PlayerGetWonder_R3() * 0.25f;
            count += PlayerGetWonder_B3() * 0.25f;
            count += PlayerGetWonder_G4() * 0.5f;
            count += PlayerGetWonder_R4() * 0.5f;
            count += PlayerGetWonder_B4() * 0.5f;
        }
        else if (ciweilization.isWinter == true)
        {
            count += CountMovesWinter();

            count += wonderLevel4 * 0.5f;
            count -= PlayerGetWonder_B4() * 0.5f;
            count += wonderLevel3 * 0.25f;
            count -= PlayerGetWonder_B3() * 0.25f;
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
}
