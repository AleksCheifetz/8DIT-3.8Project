using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;

    public SimulationManager simulationManager;

    public Transform spawnPoint;
    public Vector3 destination;

    void Start()
    {
        SimulationManager manager = simulationManager.GetComponent<SimulationManager>();

        spawnPoint = agent.GetComponent<Transform>();
        destination = spawnPoint.position;
        destination.x -= manager.sizeOfBuilding;
    }

    public void ReturnToSpawn(Transform sp)
    {
        Debug.Log("Reset");
        agent.Warp(sp.position);
    }

    public void Navigate(Vector3 dest)
    {
        agent.SetDestination(dest);
    }
}
