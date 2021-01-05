using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataInitializer : MonoBehaviour
{
    [SerializeField] private PopulationInstantiator pI;
    [SerializeField] private Slider sl;
    [SerializeField] private int code;


    private void Start()
    {
        ChangeValue();
    }
    public void ChangeValue()
    {
        switch (code) 
        {
            case 100:
                pI.numeroHuevosPez = (int)sl.value;
                break;
            case 101:
                pI.numeroHuevosRana = (int)sl.value;
                break;
            case 103:
                pI.numeroMoscas = (int)sl.value;
                break;
            case 104:
                pI.numeroPlantas = (int)sl.value;
                break;
            case 20:
                pI.maxFishVel = sl.value;
                break;
            case 21:
                pI.minFishVel = sl.value;
                break;
            case 22:
                pI.maxFishAcceleration = sl.value;
                break;
            case 23:
                pI.minFishAcceleration = sl.value;
                break;
            case 26:
                pI.maxFishOffspring = sl.value;
                break;
            case 27:
                pI.minFishOffspring = sl.value;
                break; 
            case 210:
                pI.maxFishGrowingTime = sl.value;
                break;
            case 211:
                pI.minFishGrowingTime = sl.value;
                break;
            case 212:
                pI.maxFishHatchingTime = sl.value;
                break;
            case 213:
                pI.minFishHatchingTime = sl.value;
                break;
            case 216:
                pI.maxFishLifespan = sl.value;
                break;
            case 217:
                pI.minFishLifespan = sl.value;
                break;
            case 30:
                pI.maxFrogVel = sl.value;
                break;
            case 31:
                pI.minFrogVel = sl.value;
                break;
            case 32:
                pI.maxFrogAcceleration = sl.value;
                break;
            case 33:
                pI.minFrogAcceleration = sl.value;
                break;
            case 36:
                pI.maxFrogOffspring = sl.value;
                break;
            case 37:
                pI.minFrogOffspring = sl.value;
                break;
            case 310:
                pI.maxFrogGrowingTime = sl.value;
                break;
            case 311:
                pI.minFrogGrowingTime = sl.value;
                break;
            case 312:
                pI.maxFrogHatchingTime = sl.value;
                break;
            case 313:
                pI.minFrogHatchingTime = sl.value;
                break;
            case 316:
                pI.maxFrogLifespan = sl.value;
                break;
            case 317:
                pI.minFrogLifespan = sl.value;
                break;
            case 40:
                pI.flyDelay = (int)sl.value;
                break;
            case 41:
                pI.maxFlyVel = sl.value;
                break;
            case 42:
                pI.minFlyVel = sl.value;
                break;
            case 43:
                pI.maxFlyAcceleration = sl.value;
                break;
            case 44:
                pI.minFlyAcceleration = sl.value;
                break;
            case 45:
                pI.maxFlyLifespan = sl.value;
                break;
            case 46:
                pI.minFlyLifespan = sl.value;
                break;
        }
    }
}
