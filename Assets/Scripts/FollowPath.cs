using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform tank;
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
        tank= this.GetComponent<Transform>();

    }
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
    }

    public void GoToTank()
    {
        g.AStar(currentNode, wps[2]);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        

        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
        {
            return;
        }
        //O n� que estar� mais pr�ximo neste momento
        currentNode = g.getPathPoint(currentWP);
        //se estivermos mais pr�ximo bastante do n� o tanque se mover� para o pr�ximo
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position,transform.position) < accuracy)
        {
            currentWP++;
        }
        if (currentWP > g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,
            this.transform.position.y,
            goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed);
            
        }

        
    }
}

    
