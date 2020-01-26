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

    private GameObject playerSpawn;

    private GameObject playerTower;

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
        //set player spawn
        playerSpawn = GameObject.FindGameObjectWithTag("Player_Spawn");
        //set player health to tower health
        playerTower = GameObject.FindGameObjectWithTag("Blue_Tower");
        //currentHealth = playerTower.GetComponent<EnemyTower>().GetHealth() / 100.0f;
    }

    public void SoilderLightBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_LIGHT;
        manaCost = 2.5f / 100;//deduct mana
        /*if (CanAffordMana())
        {
            Instantiate(SoldierLightPrefab, playerSpawn.gameObject.GetComponent<Transform>().position, playerSpawn.gameObject.GetComponent<Transform>().rotation);
            DeductMana();
        }*/
            pawnGeneratorComp.GetComponent<PawnManipulator>().PawnPrefab = SoldierLightPrefab;
    }

    public void SoilderHeavyBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_HEAVY;
        manaCost = 5.0f / 100;//deduct mana
       /* if (CanAffordMana())
        {
            Instantiate(SoldierHeavyPrefab, playerSpawn.gameObject.GetComponent<Transform>().position, playerSpawn.gameObject.GetComponent<Transform>().rotation);
            DeductMana();
        }*/
            pawnGeneratorComp.GetComponent<PawnManipulator>().PawnPrefab = SoldierHeavyPrefab;
       
    }

    public void SoilderArcherBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_ARCHER;

        manaCost = 7.5f / 100;//deduct mana
        /*if (CanAffordMana())
        {
            Instantiate(SoldierHeavyPrefab, playerSpawn.gameObject.GetComponent<Transform>().position, playerSpawn.gameObject.GetComponent<Transform>().rotation);
            DeductMana();
        }*/
        pawnGeneratorComp.GetComponent<PawnManipulator>().PawnPrefab = SoldierArcherPrefab;
    }


    public float GetMana()
    {
        return currentMana * 100.0f;
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

    public void SetHealth(float health)
    {
        this.currentHealth = health;
    }

    public void SetMana(float mana)
    {
        currentMana = mana;
    }
}
