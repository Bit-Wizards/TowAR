using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soilder : MonoBehaviour
{

    private float health;

    private bool dead;

    GameObject[] enemyTeam;
    string team;
    string enemyTeamPrefix; //Blue or Red

    bool isAttacking = false;

    bool seekNewTarget = false;

    private GameObject targetedEnemy;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        SetTeam();
        FindEnemyTeam();
        dead = false;
        health = 100; //inital health
    }

    float nextActionTime = 0.0f;
    float period = 6.0f;

    // Update is called once per frame
    void Update()
    {
        FindEnemyTeam();
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            targetedEnemy = FindClosestEnemy();
        }
        if (seekNewTarget)
        {
            LookForNewTarget();
        }
        else
        {

            if (!isAttacking)
            {//Goto Enemy
                GotoClosetEnemy();
            }
            else
            {
                GotoTarget();
            }
            CheckHealth();
            Attack();
        }
    }

    private void SetTeam()
    {
        if (gameObject.tag.Equals("Blue_Team"))
        {
            team = "Blue";
            enemyTeamPrefix = "Red_Team";
            Debug.Log("Blue");
        }
        else if (gameObject.tag.Equals("Red_Team"))
        {
            team = "Red";
            enemyTeamPrefix = "Blue_Team";
            Debug.Log("Red");
        }
        else
        {
            Debug.Log("Error");
        }
    }

    private void FindEnemyTeam()
    {

        enemyTeam = GameObject.FindGameObjectsWithTag(enemyTeamPrefix);
    }

    public GameObject FindClosestEnemy()
    {

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in enemyTeam)
        {
            if (enemy == null) seekNewTarget = true;
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }
        return closest;
    }



    private void GotoClosetEnemy()
    {
        if(FindClosestEnemy() != null)
        {
            float step = 0.1f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, step);
        }
        
    }

    private void GotoTarget()
    {
        float step = 0.1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetedEnemy.transform.position, step);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(enemyTeamPrefix))
        {
            isAttacking = true;
            targetedEnemy = collision.gameObject;
        }
    }

    
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.Equals(targetedEnemy))
        {
            isAttacking = true;
        }
    }


    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals(enemyTeamPrefix))
        {

            //TODO: Attack Phase
            isAttacking = false;
        }
    }

    private void Attack()
    {
        if (isAttacking)
        {
            float damageAmount = UnityEngine.Random.Range(50, 70);
            try
            {
                targetedEnemy.GetComponent<Soilder>().RecieveDamage(damageAmount);
                if (targetedEnemy.GetComponent<Soilder>().IsDead())
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

    private void RecieveDamage(float damage)
    {
        health -= damage * Time.deltaTime;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            //Die
            dead = true;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    private void LookForNewTarget()
    {
        GameObject newTarget = FindClosestEnemy();
        if (newTarget.GetComponent<Soilder>().IsDead())
        {
            seekNewTarget = true;
        }
        else
        {
            targetedEnemy = newTarget;
            seekNewTarget = false;
        }
    }

    public bool IsDead()
    {
        return dead;
    }
}