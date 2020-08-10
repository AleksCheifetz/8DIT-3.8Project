using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SimulationManager : MonoBehaviour
{
    public GameObject agentPrefab;

    public Transform spawningBoundary;

    public Slider slider;
    static float numOfAgents;

    List<GameObject> agents = new List<GameObject>();
    List<NavMeshPath> agentPaths = new List<NavMeshPath>();

    void Start()
    {
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
            float x = -Random.Range(0, spawningBoundary.transform.localScale.x);
            float z = -Random.Range(0, spawningBoundary.transform.localScale.z);
            float y;
            if (Random.value > 0.5f)
            {
                y = 0f;
            }
            else
            {
                y = 3.71f;
            }

            GameObject agent = Instantiate(agentPrefab, new Vector3(x, y, z), Quaternion.identity);

            Vector3 dest = agent.GetComponent<AgentController>().FindDestination(i);
            agent.GetComponent<AgentController>().destination = dest;

            agents.Add(agent);
        }
    }

    public void SeparateFloors()
    {
        Debug.Log("Serparate");
    }
}
