using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // Declaracao de variaveis
    Transform goal;
    float speed = 7.0f;
    float accuracy = 0.03f;
    float rotSpeed = 0.02f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start() // Chama esses elementos ao comecar
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }

    public void GoToHeli() // Movimentacao até o Heliponto
    {
        g.AStar(currentNode, wps[0]);
        currentWP = 0;
    }

    public void GoToRuin() // Movimentacao até as Ruínas
    {
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
    }

    public void GoToPP() // Movimentacao até a Usina
    {
        g.AStar(currentNode, wps[9]);
        currentWP = 0;
    }

    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        // Node mais proximo atualmente
        currentNode = g.getPathPoint(currentWP);

        // Se move para o proximo node ao se aproximar demais dele
        if (Vector3.Distance(
        g.getPathPoint(currentWP).transform.position,
        transform.position) < accuracy)
        {
            currentWP++;
        }

        if (currentWP < g.getPathLength())
        {
            // Localiza os waypoints para poder se mover ate eles
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,this.transform.position.y,goal.position.z);

            // É responsavel pela rotacao do objeto para os waypoints
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);

            // Responsavel pela movimentacao até os Waypoints
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);    
        }
    }
}
