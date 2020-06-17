using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        Transform start = agent.GetComponent<Transform>();
        Vector3 end = start.position;
        end.x -= 50;

        agent.SetDestination(end);
    }
}
