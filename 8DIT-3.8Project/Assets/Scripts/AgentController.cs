using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;

    public SimulationManager simulationManager;

    public Transform evacuationBoundary;

    public Vector3 destination;

    void Start()
    {
        SimulationManager manager = simulationManager.GetComponent<SimulationManager>();
    }

    public Vector3 FindDestination(int agentIndex)
    {
        float zBoundary = evacuationBoundary.transform.localScale.z;
        float xBoundary = evacuationBoundary.transform.localScale.x;

        float zPosition = evacuationBoundary.transform.position.z + (zBoundary / 2);
        float xPosition = evacuationBoundary.transform.position.x + (xBoundary / 2);

        return new Vector3(xPosition - (agentIndex / (int)zBoundary), 1, zPosition - (agentIndex % zBoundary));
    }

    public void Navigate(Vector3 dest)
    {
        agent.SetDestination(dest);
    }
}
