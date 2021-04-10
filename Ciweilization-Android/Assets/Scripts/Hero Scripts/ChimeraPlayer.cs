using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraPlayer : Player
{
    /* Trigger Chimera Special Wonder G3 ability for the player. */
    protected override IEnumerator WonderG3Ability()
    {
        PlayWonderAbilityAudio();
        PlayerBuildRandom(3);
        yield return new WaitForSeconds(abilityTriggerInterval);
        PlayWonderAbilityAudio();
        PlayerBuildRandom(3);
        yield return new WaitForSeconds(abilityTriggerInterval);
        PlayWonderAbilityAudio();
        PlayerBuildRandom(3);
        yield return new WaitForSeconds(abilityTriggerInterval);

        yield return 0;
    }

    /* Trigger Chimera Special Wonder R3 ability for the player. */
    protected override void WonderR3Ability()
    {
        PlayWonderAbilityAudio();
        defaultMoves += 0.75;  
    }

    /* Trigger Chimera Special Wonder Y3 ability for the player. */
    protected override void WonderY3Ability()
    {
        PlayWonderAbilityAudio();
        savedMoves = 3f * (moves - 1f);
        moves = 0;
    }

    /* Trigger Chimara Special Wonder B3 ability for the player. */
    protected override IEnumerator WonderB3Ability()
    {
        for (int i = 1; i <= G1; i++)
        {
            PlayWonderAbilityAudio();
            PlayerBuild("G2");
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 1; i <= R1; i++)
        {
            PlayWonderAbilityAudio();
            PlayerBuild("R2");
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 1; i <= Y1; i++)
        {
            PlayWonderAbilityAudio();
            PlayerBuild("Y2");
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 1; i <= B1; i++)
        {
            PlayWonderAbilityAudio();
            PlayerBuild("B2");
            yield return new WaitForSeconds(0.2f);
        }

        yield return 0;
    }
}
