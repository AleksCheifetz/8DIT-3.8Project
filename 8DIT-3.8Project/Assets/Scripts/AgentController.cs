using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;

    float heightRange = 0.165f;
    float widthRange = 0.04f;
    float speedRange = 0.128f;

    public SimulationManager simulationManager;
    public Transform evacuationBoundary;
    public Vector3 destination;

    void Start()
    {
        SimulationManager manager = simulationManager.GetComponent<SimulationManager>();

        float heightOffset = Random.Range(-heightRange, heightRange);
        float widthOffset = Random.Range(-widthRange, widthRange);
        float speedOffset = Random.Range(-speedRange, speedRange);

        Vector3 scaleOffset = new Vector3(widthOffset, heightOffset, widthOffset);

        agent.transform.localScale += scaleOffset;
        agent.GetComponent<NavMeshAgent>().speed += speedOffset;
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
