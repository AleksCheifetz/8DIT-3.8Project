using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SimulationManager : MonoBehaviour
{
    public GameObject agentPrefab;

    public List<Transform> spawningBoundariesList;
    static List<float> spawningBoundariesWeights = new List<float>();
    public GameObject spawningBoundary;
    static float totalArea;
    static Transform currentSpawningArea;

    public Slider slider;
    static float numOfAgents;

    List<GameObject> agents = new List<GameObject>();
    List<NavMeshPath> agentPaths = new List<NavMeshPath>();

    void Start()
    {
        spawningBoundariesList = spawningBoundary.GetComponentsInChildren<Transform>().ToList();
        spawningBoundariesList.RemoveAt(0);

        foreach (Transform boundary in spawningBoundariesList)
        {
            float area = boundary.localScale.x * boundary.localScale.z;
            totalArea += area;
        }

        for (int i = 0; i < spawningBoundariesList.Count; i++)
        {
            float weight = spawningBoundariesList[i].localScale.x * spawningBoundariesList[i].localScale.z / totalArea;

            if (spawningBoundariesWeights.Count == 0)
            {
                spawningBoundariesWeights.Add(weight);
            }
            else
            {
                weight += spawningBoundariesWeights[spawningBoundariesWeights.Count - 1];
                spawningBoundariesWeights.Add(weight);
            }
        }

        numOfAgents = slider.value;
        SpawnAgents();
    }

    public void TimeScaleChange(Slider timeScaleSlider)
    {
        Time.timeScale = timeScaleSlider.value;
    }

    public void Simulate()
    {
        agents.RemoveAll(i => i == null);

        foreach (GameObject agent in agents)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 dest = agent.GetComponent<AgentController>().destination;
            NavMesh.CalculatePath(agent.transform.position, dest, NavMesh.AllAreas, path);
            agentPaths.Add(path);
        }

        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].GetComponent<AgentController>().Navigate(agentPaths[i]);
        }
    }

    public void ResetSimulation()
    {
        foreach (GameObject agent in agents)
        {
            Destroy(agent);
        }
        agentPaths.Clear();

        numOfAgents = slider.value;
        SpawnAgents();
    }

    void SpawnAgents()
    {
        for (int i = 0; i < numOfAgents; i++)
        {
            float spawningArea = Random.value;
            if (spawningArea < spawningBoundariesWeights[0])
            {
                currentSpawningArea = spawningBoundariesList[0];
            }
            else if (spawningArea >= spawningBoundariesWeights[0] && spawningArea < spawningBoundariesWeights[1])
            {
                currentSpawningArea = spawningBoundariesList[1];
            }
            else if (spawningArea >= spawningBoundariesWeights[1] && spawningArea < spawningBoundariesWeights[2])
            {
                currentSpawningArea = spawningBoundariesList[2];
            }
            else if (spawningArea >= spawningBoundariesWeights[2] && spawningArea < spawningBoundariesWeights[3])
            {
                currentSpawningArea = spawningBoundariesList[3];
            }
            else if (spawningArea >= spawningBoundariesWeights[3] && spawningArea < spawningBoundariesWeights[4])
            {
                currentSpawningArea = spawningBoundariesList[4];
            }

            float x = -Random.Range(0, currentSpawningArea.localScale.x) / 2;
            float z = -Random.Range(0, currentSpawningArea.localScale.z) / 2;
            float y;
            float finalXPos;
            float finalZPos;
            if (Random.value > 0.5f)
            {
                y = 0f;
            }
            else
            {
                y = 3.71f;
            }
            if (Random.value > 0.5f)
            {
                finalXPos = currentSpawningArea.position.x - x;
            }
            else
            {
                finalXPos = currentSpawningArea.position.x + x;
            }
            if (Random.value > 0.5f)
            {
                finalZPos = currentSpawningArea.position.z - z;
            }
            else
            {
                finalZPos = currentSpawningArea.position.z + z;
            }

            GameObject agent = Instantiate(agentPrefab, new Vector3(finalXPos, y, finalZPos), Quaternion.identity);

            Vector3 dest = agent.GetComponent<AgentController>().FindDestination(i);
            agent.GetComponent<AgentController>().destination = dest;

            agents.Add(agent);
        }
    }

    public void SeparateFloors()
    {
        agents.RemoveAll(i => i == null);
        foreach (GameObject agent in agents)
        {
            if (agent.transform.position.y > 1)
            {
                Debug.Log("On First Floor");
                agent.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }
}
