using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * DataInitializer: clase que inicializa los datos de los 
 * sliders de los menús de configuración.
 */
public class DataInitializer : MonoBehaviour
{
    [SerializeField] private PopulationInstantiator pI;
    [SerializeField] private Slider sl;
    [SerializeField] private int code;


    private void Start()
    {
        InitValue();
    }
    public void InitValue()
    {
        sl.interactable = false;
        switch (code) 
        {
            case 100:
                sl.value = pI.numeroHuevosPez;
                break;
            case 101:
                sl.value = pI.numeroHuevosRana;
                break;
            case 103:
                sl.value = pI.numeroMoscas;
                break;
            case 104:
                sl.value = pI.numeroPlantas;
                break;
            case 20:
                sl.value = pI.maxFishVel;
                break;
            case 21:
                sl.value = pI.minFishVel;
                break;
            case 22:
                sl.value = pI.maxFishAcceleration;
                break;
            case 23:
                sl.value = pI.minFishAcceleration;
                break;
            case 26:
                sl.value = pI.maxFishOffspring;
                break;
            case 27:
                sl.value = pI.minFishOffspring;
                break; 
            case 210:
                sl.value = pI.maxFishGrowingTime;
                break;
            case 211:
                sl.value = pI.minFishGrowingTime;
                break;
            case 212:
                sl.value = pI.maxFishHatchingTime;
                break;
            case 213:
                sl.value = pI.minFishHatchingTime;
                break;
            case 216:
                sl.value = pI.maxFishLifespan;
                break;
            case 217:
                sl.value = pI.minFishLifespan;
                break;
            case 30:
                sl.value = pI.maxFrogVel;
                break;
            case 31:
                sl.value = pI.minFrogVel;
                break;
            case 32:
                sl.value = pI.maxFrogAcceleration;
                break;
            case 33:
                sl.value = pI.minFrogAcceleration;
                break;
            case 36:
                sl.value = pI.maxFrogOffspring;
                break;
            case 37:
                sl.value = pI.minFrogOffspring;
                break;
            case 310:
                sl.value = pI.maxFrogGrowingTime;
                break;
            case 311:
                sl.value = pI.minFrogGrowingTime;
                break;
            case 312:
                sl.value = pI.maxFrogHatchingTime;
                break;
            case 313:
                sl.value = pI.minFrogHatchingTime;
                break;
            case 316:
                sl.value = pI.maxFrogLifespan;
                break;
            case 317:
                sl.value = pI.minFrogLifespan;
                break;
            case 40:
                sl.value = pI.flyDelay;
                break;
            case 41:
                sl.value = pI.maxFlyVel;
                break;
            case 42:
                sl.value = pI.minFlyVel;
                break;
            case 43:
                sl.value = pI.maxFlyAcceleration;
                break;
            case 44:
                sl.value = pI.minFlyAcceleration;
                break;
            case 45:
                sl.value = pI.maxFlyLifespan;
                break;
            case 46:
                sl.value = pI.minFlyLifespan;
                break;
        }
        sl.interactable = true;
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
