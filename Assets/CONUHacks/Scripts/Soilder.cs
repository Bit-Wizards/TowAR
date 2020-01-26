using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soilder : MonoBehaviour
{

    private float health;

    GameObject[] enemyTeam;
    string team;
    string enemyTeamPrefix; //Blue or Red

    bool isAttacking = false;

    private GameObject targetedEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        SetTeam();
        FindEnemyTeam();
    }

    // Update is called once per frame
    void Update()
    {
        GotoClosetEnemy(); //Goto Enemy
        CheckHealth();
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
        float step = 0.1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, step);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(enemyTeamPrefix))
        {
            //TODO: Attack Phase
            isAttacking = true;
            targetedEnemy = collision.gameObject;
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



    private void CheckHealth()
    {
        if(health < 0)
        {
            //Die
            Destroy(gameObject);
        }
    }
}
