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
    public GameObject SoldierBluePrefab;
    public GameObject SoldierRedPrefab;

    private enum SoilderSelectType
    {
        SOLDIER_BLUE,
        SOLDIER_RED
    };

    private int currentSelectedSoilder;

    // Start is called before the first frame update
    void Start()
    {
        currentSelectedSoilder = (int) SoilderSelectType.SOLDIER_BLUE;
    }

    void Awake()
    {
        pawnGeneratorComp.GetComponent<PawnManipulator>().PawnPrefab = SoldierBluePrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoilderBlueBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_BLUE;
        Debug.Log("Blue");
    }

    public void SoilderRedBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_RED;
        Debug.Log("Red");
    }
}
