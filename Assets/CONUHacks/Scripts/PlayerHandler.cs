using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore.Examples.ObjectManipulation;


public class PlayerHandler : MonoBehaviour
{

    //Pawn Generator Component
    public GameObject pawnGeneratorComp;

    //Soilder Asset Prefabs TODO: change once chris gives us his prefabs
    public GameObject SoldierLightPrefab;
    public GameObject SoldierHeavyPrefab;
    public GameObject SoldierArcherPrefab;

    private float currentMana;

    private float currentHealth; // player health

    private float manaCost;

    private enum SoilderSelectType
    {
        SOLDIER_LIGHT,
        SOLDIER_HEAVY,
        SOLDIER_ARCHER
    };

    private int currentSelectedSoilder;

    // Start is called before the first frame update
    void Start()
    {
        currentSelectedSoilder = (int) SoilderSelectType.SOLDIER_LIGHT;
        currentMana = 1.0f; //Initial Mana Value 
        currentHealth = 1.0f; //Initial Health Value (0.00 - 1.0 is percentage health)
        manaCost = 2.5f / 100;//inital value for light soldier
    }

    void Awake()
    {
        pawnGeneratorComp.GetComponent<PawnManipulator>().PawnPrefab = SoldierLightPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoilderLightBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_LIGHT;
        pawnGeneratorComp.GetComponent<PawnManipulator>().PawnPrefab = SoldierLightPrefab;
        manaCost = 2.5f / 100;//deduct mana
    }

    public void SoilderHeavyBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_HEAVY;
        pawnGeneratorComp.GetComponent<PawnManipulator>().PawnPrefab = SoldierHeavyPrefab;
        manaCost = 5.0f / 100;//deduct mana
    }

    public void SoilderArcherBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_ARCHER;
        pawnGeneratorComp.GetComponent<PawnManipulator>().PawnPrefab = SoldierArcherPrefab;
        manaCost = 7.5f/ 100;//deduct mana
    }


    public float GetMana()
    {
        return currentMana;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void ApplyDamage(float damageValue)
    {
        currentHealth -= damageValue / 100; //scale down for the 0-1 system
    }

    public void DeductMana()
    {
        currentMana -= manaCost;
    }

    public bool CanAffordMana()
    {
        if((currentMana - manaCost) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
