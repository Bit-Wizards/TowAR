using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soilder : MonoBehaviour
{

    protected float health;

    protected bool dead;

    protected float speed;

    GameObject[] enemyTeam;
    protected string team;
    protected string enemyTeamPrefix; //Blue or Red
    protected string enemyTowerPrefix;

    protected bool isAttacking = false;

    protected bool seekNewTarget = false;

    protected GameObject targetedEnemy;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        speed = 0.1f;
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
        if (Time.time > nextActionTime && !eneimiesDead)
        {
            nextActionTime += period;
            targetedEnemy = FindClosestEnemy();
        }
        if (eneimiesDead)
        {
            targetedEnemy = GameObject.FindGameObjectWithTag(enemyTowerPrefix);
            GotoTarget();
        }
        else if (seekNewTarget)
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

    protected void SetTeam()
    {
        if (gameObject.tag.Equals("Blue_Team"))
        {
            team = "Blue";
            enemyTeamPrefix = "Red_Team";
            enemyTowerPrefix = "Red_Tower";
        }
        else if (gameObject.tag.Equals("Red_Team"))
        {
            team = "Red";
            enemyTeamPrefix = "Blue_Team";
            enemyTowerPrefix = "Blue_Tower";
        }
        else
        {
            Debug.Log("Error");
        }
    }

    bool eneimiesDead = false;

    protected void FindEnemyTeam()
    {

        enemyTeam = GameObject.FindGameObjectsWithTag(enemyTeamPrefix);
        eneimiesDead = true;
        foreach (GameObject enemy in enemyTeam)
        {
            if (!enemy.GetComponent<Soilder>().IsDead())
            {
                eneimiesDead = false;
            }
        }
    }

    protected GameObject FindClosestEnemy()
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



    protected void GotoClosetEnemy()
    {
        if (FindClosestEnemy() != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, step);
        }

    }

    protected void GotoTarget()
    {
        float step = speed * Time.deltaTime;
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
            if (collision.gameObject.tag.Equals(enemyTowerPrefix))
            {
                collision.gameObject.GetComponent<EnemyTower>().ApplyDamage(5.0f);
            }
            else
            {
                isAttacking = true;
            }
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

    protected void Attack()
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

    protected void RecieveDamage(float damage)
    {
        health -= damage * Time.deltaTime;
    }

    protected void CheckHealth()
    {
        if (health <= 0)
        {
            //Die
            dead = true;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    protected void LookForNewTarget()
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