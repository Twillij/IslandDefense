using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image energyBar;

    public float startEnergy = 100;
    public float energy;

    public void UseLaser()
    {
        energyBar.fillAmount = energy/startEnergy;
    }
}
