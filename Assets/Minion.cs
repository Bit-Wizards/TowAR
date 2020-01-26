using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private int teamID = 0;

    public GameObject[] Enemy_Team;

    GameObject bestTarget = null;
    // Start is called before the first frame update

    public Minion()
    {
        this.AssignTag(teamID);
    }

    private void AssignTag(int team)
    {
        if (team == 0)
        {
            this.tag = "Red_Team";
        }
        else if (team == 1)
        {
            this.tag = "Blue_Team";
        }
    }

    private void FindClosestEnemy()
    {
        if (Enemy_Team.Length <= 0)
        {
            Enemy_Team = (this.tag == "Blue_Team") ? GameObject.FindGameObjectsWithTag("Red_Team") : GameObject.FindGameObjectsWithTag("Blue_Team");
            Debug.Log("found");
        }
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 position = transform.position; //OUR POSITION X-Z plane
        foreach (GameObject enemy in Enemy_Team)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < closestDistanceSqr)
            {
                bestTarget = enemy;
                closestDistanceSqr = curDistance;
            }
        }
    }

    private void MoveTo(GameObject bestTarget)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, bestTarget.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.tag != this.tag)
        {
            Attack(other.gameObject);
        }
    }

    private void Attack(GameObject enemy)
    {

    }

    void Start()
    {
        InvokeRepeating("FindClosestEnemy", 1.0f, 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        if (bestTarget != null)
        {
            MoveTo(bestTarget);
        }

    }
}
