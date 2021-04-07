using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantPlayer : Player
{
    /* Count the number of extra moves the player should get in Spring.*/
    protected override double CountMovesSpring()
    {
        int level1 = PlayerGetG1() + PlayerGetR1() +
                        PlayerGetY1() + PlayerGetB1();
        int level2 = PlayerGetG2() + PlayerGetR2() +
                        PlayerGetY2() + PlayerGetB2();

        double count = 0f;
        count += level1 * 0.25f;
        count += level2 * 0.5f;
        count += PlayerGetG1() * 0.25f;
        count += PlayerGetG2() * 0.5f;
        count += PlayerGetY1() * 0.25f;
        count += PlayerGetY2() * 0.5f;

        return count;
    }

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
        count += PlayerGetR2() * 0.25f;
        count += PlayerGetR3() * 0.5f;
        count += PlayerGetY2() * 0.25f;
        count += PlayerGetY3() * 0.5f;

        return count;
    }

    /* Count the number of extra moves the player should get in Fall.*/
    protected override double CountMovesFall()
    {
        int level3 = PlayerGetG3() + PlayerGetR3() +
                        PlayerGetY3() + PlayerGetB3();
        int level4 = PlayerGetG4() + PlayerGetR4() +
                        PlayerGetY4() + PlayerGetB4();

        double count = 0f;
        count += level3 * 0.25f;
        count += level4 * 0.5f;
        count += PlayerGetY3() * 0.25f;
        count += PlayerGetY4() * 0.5f;

        return count;
    }

    /* Count the number of extra moves the player should get in Winter.*/
    protected override double CountMovesWinter()
    {
        int level4 = PlayerGetG4() + PlayerGetR4() +
                        PlayerGetY4() + PlayerGetB4();

        double count = 0f;
        count += level4 * 0.5f;
        count += PlayerGetB4() * 0.5f;
        count += PlayerGetB3() * 0.25f;
        count += PlayerGetY4() * 0.5f;
        count += PlayerGetY3() * 0.25f;

        return count;

    }
}
