using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public NavMeshAgent agent;
    GameObject goal;
    public enum sygdomstilstand
    {
        susceptivle,
        infected,
        recovered,
    }

    public sygdomstilstand minsygdomstilstand = new sygdomstilstand();

    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.Find("Goal");
        agent.SetDestination(goal.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
