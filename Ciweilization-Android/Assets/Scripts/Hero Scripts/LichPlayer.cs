using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPlayer : Player
{
    private int heroPowerCount = 1;

    // Update is called once per frame
    protected override void Update()
    {
        if (G3 + R3 + Y3 + B3 <= 2)
        {
            return;
        }

        if (G3 > 0 & R3 > 0 & Y3 > 0)
        {
            if (heroPowerCount > 0)
            {
                HeroPower();
            }
        }
        else if (G3 > 0 & R3 > 0 & B3 > 0)
        {
            if (heroPowerCount > 0)
            {
                HeroPower();
            }
        }
        else if (G3 > 0 & Y3 > 0 & B3 > 0)
        {
            if (heroPowerCount > 0)
            {
                HeroPower();
            }
        }
        else if (R3 > 0 & Y3 > 0 & B3 > 0)
        {
            if (heroPowerCount > 0)
            {
                HeroPower();
            }
        }
    }

    private void HeroPower()
    {
        PlayHeroPowerAudio();
        moves += 3;
        heroPowerCount -= 1;
    }
}
