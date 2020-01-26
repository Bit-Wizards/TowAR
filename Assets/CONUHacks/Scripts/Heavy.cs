using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : Soilder
{
    // Start is called before the first frame update

    // Update is called once per frame






    new void Awake()
    {
        speed = 0.05f;
        SetTeam();
        FindEnemyTeam();
        dead = false;
        health = 300; //inital health

    }
    new private void Attack()
    {
        if (isAttacking)
        {
            float damageAmount = UnityEngine.Random.Range(100, 125);
            try
            {
                targetedEnemy.GetComponent<Heavy>().RecieveDamage(damageAmount);
                if (targetedEnemy.GetComponent<Heavy>().IsDead())
                {
                    isAttacking = false;
                    LookForNewTarget();
                }
            }
            catch (MissingReferenceException e)
            {
                LookForNewTarget();
            }
        }
    }

}
