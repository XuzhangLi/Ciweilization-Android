using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankerPlayer : Player
{
    public override void PlayerBuild(string name)
    {
        ////////////////////////////////////////////// Level 1 buildings
        if (name == "G1")
        {
            G1 += 1;
            Debug.Log("Successfully Built.");
            audioManager.Play("Coin");
            //Change PlayerDisplay function into non-RPC, target player from both 
            //player already calls PlayerBuild. We should only call PlayerDisplay if
            //photonView.isMine is true.
            PlayerDisplay("G1", G1, false);
            if (G1 == 3)
            {
                PlayerWBuild("Wonder_G2");
                Debug.Log("A triple! You get a wonder!");
            }
        }
        else if (name == "R1")
        {
            R1 += 1;
            Debug.Log("Successfully Built.");
            audioManager.Play("Coin");
            PlayerDisplay("R1", R1, false);
            if (R1 == 3)
            {
                PlayerWBuild("Wonder_R2");
                Debug.Log("A triple! You get a wonder!");
            }
        }
        else if (name == "Y1")
        {
            Y1 += 1;
            Debug.Log("Successfully Built.");
            audioManager.Play("Coin");
            PlayerDisplay("Y1", Y1, false);
            if (Y1 == 3)
            {
                PlayerWBuild("Wonder_Y2");
                Debug.Log("A triple! You get a wonder!");
            }
        }
        else if (name == "B1")
        {
            B1 += 1;
            Debug.Log("Successfully Built.");
            audioManager.Play("Coin");
            PlayerDisplay("B1", B1, false);
            if (B1 == 3)
            {
                PlayerWBuild("Wonder_B2");
                Debug.Log("A triple! You get a wonder!");
            }
        }

        //////////////////////////////////////////////  Level 2 buildings

        else if (name == "G2")
        {
            if (G1 + G2 != 0)
            {
                G2 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("G2", G2, false);
                if (G2 == 3)
                {
                    PlayerWBuild("Wonder_G3");
                    Debug.Log("A triple! You get a wonder!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "R2")
        {
            if (R1 + R2 != 0)
            {
                R2 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("R2", R2, false);
                if (R2 == 3)
                {
                    PlayerWBuild("Wonder_R3");
                    Debug.Log("A triple! You get a wonder!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "Y2")
        {
            if (Y1 + Y2 != 0)
            {
                Y2 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("Y2", Y2, false);
                if (Y2 == 3)
                {
                    PlayerWBuild("Wonder_Y3");
                    Debug.Log("A triple! You get a wonder!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "B2")
        {
            if (B1 + B2 != 0)
            {
                B2 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("B2", B2, false);
                if (B2 == 3)
                {
                    PlayerWBuild("Wonder_B3");
                    Debug.Log("A triple! You get a wonder!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }

        //////////////////////////////////////////////  Level 3 buildings

        else if (name == "G3")
        {
            if (G2 + G3 != 0)
            {
                G3 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("G3", G3, false);
                if (G3 == 3)
                {
                    PlayerWBuild("Wonder_G4");
                    Debug.Log("A triple! You get a wonder!");
                }
                PlayHeroPowerAudio();
                PlayerBuild("Y1");
                PlayerBuild("Y2");
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "R3")
        {
            if (R2 + R3 != 0)
            {
                R3 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("R3", R3, false);
                if (R3 == 3)
                {
                    PlayerWBuild("Wonder_R4");
                    Debug.Log("A triple! You get a wonder!");
                }
                PlayHeroPowerAudio();
                PlayerBuild("Y1");
                PlayerBuild("Y2");
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "Y3")
        {
            if (Y2 + Y3 != 0)
            {
                Y3 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("Y3", Y3, false);
                if (Y3 == 3)
                {
                    PlayerWBuild("Wonder_Y4");
                    Debug.Log("A triple! You get a wonder!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "B3")
        {
            if (B2 + B3 != 0)
            {
                B3 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("B3", B3, false);
                if (B3 == 3)
                {
                    PlayerWBuild("Wonder_B4");
                    Debug.Log("A triple! You get a wonder!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }

        //////////////////////////////////////////////  Level 4 buildings

        else if (name == "G4")
        {
            if (G3 + G4 != 0)
            {
                G4 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("G4", G4, false);
                if ((R4 + Y4 + B4 > 0) || (Wonder_G4 == true))
                {
                    EndGameWhenTurnEnds();
                }
                else
                {
                    Debug.Log("You only need another different level-4 to win!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "R4")
        {
            if (R3 + R4 != 0)
            {
                R4 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("R4", R4, false);
                if ((G4 + Y4 + B4 > 0) || (Wonder_R4 == true))
                {
                    EndGameWhenTurnEnds();
                }
                else
                {
                    Debug.Log("You only need another different level-4 to win!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "Y4")
        {
            if (Y3 + Y4 != 0)
            {
                Y4 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("Y4", Y4, false);
                if ((G4 + R4 + B4 > 0) || (Wonder_Y4 == true))
                {
                    EndGameWhenTurnEnds();
                }
                else
                {
                    Debug.Log("You only need another different level-4 to win!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
        else if (name == "B4")
        {
            if (B3 + B4 != 0)
            {
                B4 += 1;
                Debug.Log("Successfully Built.");
                audioManager.Play("Coin");
                PlayerDisplay("B4", B4, false);
                if ((G4 + R4 + Y4 > 0) || (Wonder_B4 == true))
                {
                    EndGameWhenTurnEnds();
                }
                else
                {
                    Debug.Log("You only need another different level-4 to win!");
                }
            }
            else
            {
                audioManager.Play("Discard");
                Debug.Log("You can't build this right now. You need to build the building below this first.");
            }
        }
    }     
}
