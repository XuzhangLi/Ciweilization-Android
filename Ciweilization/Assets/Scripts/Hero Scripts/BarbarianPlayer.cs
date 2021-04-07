using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarbarianPlayer : Player
{
    // Start is called before the first frame update
    protected override void Start()
    {
        defaultMoves = 1.25f; //The Barbarian has 0.25 extra base move.
        moves = 0f;
        savedMoves = 0f;
        defaultChances = 2;
        chances = 2;
        chanceCount = 0;

        G1 = G2 = G3 = G4 = R1 = R2 = R3 = R4 = Y1 = Y2 = Y3 = Y4 = B1 = B2 = B3 = B4 = 0;
        Wonder_G1 = Wonder_G2 = Wonder_G3 = Wonder_G4 =
            Wonder_R1 = Wonder_R2 = Wonder_R3 = Wonder_R4 =
            Wonder_Y1 = Wonder_Y2 = Wonder_Y3 = Wonder_Y4 =
            Wonder_B1 = Wonder_B2 = Wonder_B3 = Wonder_B4 = false;

        audioManager = FindObjectOfType<AudioManager>();

        ciweilization = FindObjectOfType<Ciweilization>();
    }
}
