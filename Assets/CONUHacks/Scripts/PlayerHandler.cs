using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public Button soilderBlueBtn; 

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoilderBlueBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_BLUE;
    }

    public void SoilderRedBtnClick()
    {
        currentSelectedSoilder = (int)SoilderSelectType.SOLDIER_RED;
    }
}
