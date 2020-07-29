using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public GameObject agentPrefab;

    public Transform spawningBoundary;

    public Slider slider;
    static float numOfAgents;

    List<GameObject> agents = new List<GameObject>();

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
            Vector3 dest = agent.GetComponent<AgentController>().destination;
            agent.GetComponent<AgentController>().Navigate(dest);
        }
    }

    public void ResetSimulation()
    {
        foreach (GameObject agent in agents)
        {
            Destroy(agent);
        }

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
}
