using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        health = 100.0f;

    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(health <= 0)
        {
            if (gameObject.tag.Equals("Blue_Tower"))
            {
                Debug.Log("Player Dead!");
            }
            else
            {
                Debug.Log("Enemy Dead!");
            }
            //TODO: END GAME!
        }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage * Time.deltaTime;
    }
}
