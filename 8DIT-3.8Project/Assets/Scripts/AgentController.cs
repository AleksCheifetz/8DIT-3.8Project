using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;

    float femaleHeight = 0.825f;
    float femaleWidth = 0.4f;

    float heightRange = 0.165f;
    float widthRange = 0.04f;
    float speedRange = 0.128f;

    static GameObject evacuationBoundary;
    public Vector3 destination;

    void Awake()
    {
        evacuationBoundary = GameObject.Find("EvacuationBoundary");
    }

    void Start()
    {
        float heightOffset = Random.Range(-heightRange, heightRange);
        float widthOffset = Random.Range(-widthRange, widthRange);
        float speedOffset = Random.Range(-speedRange, speedRange);

        Vector3 scaleOffset = new Vector3(widthOffset, heightOffset, widthOffset);

        bool isFemale = (Random.value > 0.5f);

        if (isFemale)
        {
            agent.transform.localScale = new Vector3(femaleWidth, femaleHeight, femaleWidth);
        }

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
