using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct Link //Cria��o de um struct para declarar vari�veis 
{
    public enum direction {UNI, BI}
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    // Declara��o dos arrays dos waypoints e os links
    public GameObject[] waypoints; 
    public Link[] links; // Conex�o entre dois nodes

    // Cast no script do Graph
    public Graph graph = new Graph();

    private void Start()
    {
        if(waypoints.Length > 0)
        {
            foreach(GameObject wp in waypoints)
            {
                graph.AddNode(wp); // Adciona os Waypoints na rota
            }
            foreach(Link I in links) // Interliga��o dos links
            {
                graph.AddEdge(I.node1, I.node2); 
                if (I.dir == Link.direction.BI) // Determina se � UNI ou BI
                {
                    graph.AddEdge(I.node1, I.node2);
                }
            }
        }
    }

    private void Update()
    {
        graph.debugDraw(); // Desenha o trajeto para os Waypoints
    }
}
