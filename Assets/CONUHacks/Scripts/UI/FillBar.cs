using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    // Unity UI References
    public Slider slider;
    public Text displayText;

    //used to try and find values from the player e.g. manna, health
    public GameObject player;
    private PlayerHandler playerHandler;

    //check if health bar
    public bool healthBar;

    // Create a property to handle the slider's value
    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            slider.value = currentValue;
            displayText.text = (slider.value * 100).ToString("0.00") + "%";
        }
    }

    // Use this for initialization
    void Start()
    {
        CurrentValue = 0f;
        playerHandler = player.GetComponent<PlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar)
        {
            CurrentValue = playerHandler.GetHealth();
        }
        else
        {
            CurrentValue = playerHandler.GetMana();
        }
    }

    
}
