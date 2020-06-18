using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    public SimulationManager simulationManager;

    // Start is called before the first frame update
    void Start()
    {
        SimulationManager manager = simulationManager.GetComponent<SimulationManager>();

        Transform start = agent.GetComponent<Transform>();
        Vector3 end = start.position;
        end.x -= manager.sizeOfBuilding;

        agent.SetDestination(end);
    }
}
