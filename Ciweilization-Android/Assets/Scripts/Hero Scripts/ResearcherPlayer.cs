using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearcherPlayer : Player
{
    private int heroPowerCount = 1;

    // Update is called once per frame
    protected override void Update()
    {
        if (heroPowerCount < 1)
        {
            return;
        }

        int blueWonderTotal = ciweilization.BoolToInt(ciweilization.wonderB2) +
                                ciweilization.BoolToInt(ciweilization.wonderB3) +
                                ciweilization.BoolToInt(ciweilization.wonderB4);
        
        if (blueWonderTotal >= 2)
        {
            HeroPower();
        }
    }

    private void HeroPower()
    {
        PlayHeroPowerAudio();
        PlayerBuild("B1");
        PlayerBuild("B2");
        PlayerBuild("B3");
        heroPowerCount -= 1;
    }
}
