using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public NavMeshAgent agent; //Declaracao de variaveis

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>(); //Pega o component do NavMesh do agente
    }
}
