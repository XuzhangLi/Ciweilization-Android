using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private Player player;

    private double energy;

    // full energy count, which is 6
    public int numOfEnergies = 6;

    public Image[] energies;
    public Sprite fullEnergy;
    public Sprite threeQuarterEnergy;
    public Sprite halfEnergy;
    public Sprite quarterEnergy;
    public Sprite emptyEnergy;

    private void Start()
    {
        player = GetComponent<Player>();
        energy = player.moves;
    }
    void Update()
    {
        if (player.startEnergy == false)
        {
            return;
        }

        energy = player.moves;

        //energy can't exceed max count 
        if (energy > numOfEnergies)
        {
            energy = numOfEnergies;
        }

        for (int i = 0; i < energies.Length; i++)
        {
            //decide whether to display full, half, or empty energy sprite for each spot
            if (i < energy - 0.75f)
            {
                energies[i].sprite = fullEnergy;
            }
            else if (i == energy - 0.75f)
            {
                energies[i].sprite = threeQuarterEnergy;
            }
            else if (i == energy - 0.5f)
            {
                energies[i].sprite = halfEnergy;
            }
            else if (i == energy - 0.25f)
            {
                energies[i].sprite = quarterEnergy;
            }
            else
            {
                energies[i].sprite = emptyEnergy;
            }

            //only display energy spots as many as the full energy count
            if (i < numOfEnergies)
            {
                energies[i].enabled = true;
            }
            else
            {
                energies[i].enabled = false;
            }
        }
    }
}
