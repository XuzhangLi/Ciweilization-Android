using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerPlayer : Player
{
    /* Count the number of extra moves the player should get in Summer.*/
    protected override double CountMovesSummer()
    {

        int level2 = PlayerGetG2() + PlayerGetR2() +
                        PlayerGetY2() + PlayerGetB2();
        int level3 = PlayerGetG3() + PlayerGetR3() +
                        PlayerGetY3() + PlayerGetB3();

        double count = 0f;
        count += level2 * 0.25f;
        count += level3 * 0.5f;
        count += PlayerGetR2() * 0.5f;
        count += PlayerGetR3() * 1f;

        return count;
    }
}
