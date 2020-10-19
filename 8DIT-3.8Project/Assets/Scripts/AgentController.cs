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
    static float startingPointX;
    static float startingPointZ;
    public Vector3 destination;
    public NavMeshPath path;

    public float wanderRadius;
    public float wanderTimer;
    float timer;

    static bool simulationStarted;

    void Awake()
    {
        evacuationBoundary = GameObject.Find("EvacuationBoundary");
        startingPointX = evacuationBoundary.transform.GetChild(0).gameObject.transform.position.x;
        startingPointZ = evacuationBoundary.transform.GetChild(0).gameObject.transform.position.z;

        timer = wanderTimer;
        simulationStarted = false;
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

    void Update()
    {
        if (simulationStarted == false && agent.enabled == true)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public Vector3 FindDestination(int agentIndex)
    {
        float zScale = evacuationBoundary.transform.localScale.z;

        float rotationAngle = Mathf.Round(evacuationBoundary.transform.eulerAngles.y);

        float xOffsetAgent = (agentIndex % zScale) * (Mathf.Sin(rotationAngle * Mathf.PI / 180));
        float zOffsetAgent = (agentIndex % zScale) * (Mathf.Cos(rotationAngle * Mathf.PI / 180));

        float xOffsetSpawn = (agentIndex / zScale) * (Mathf.Cos(rotationAngle * Mathf.PI / 180));
        float zOffsetSpawn = (agentIndex / zScale) * (Mathf.Sin(rotationAngle * Mathf.PI / 180));

        startingPointX -= xOffsetSpawn;
        startingPointZ += zOffsetSpawn;

        return new Vector3(startingPointX - xOffsetAgent, 1, startingPointZ - zOffsetAgent);
    }

    public void Navigate(NavMeshPath path)
    {
        simulationStarted = true;
        agent.SetPath(path);
    }
}
