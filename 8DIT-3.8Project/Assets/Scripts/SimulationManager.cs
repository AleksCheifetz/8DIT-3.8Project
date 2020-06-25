using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public GameObject agentPrefab;

    public Transform spawnBoundary;

    public Slider slider;
    static float numOfAgents;

    List<GameObject> agents = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        numOfAgents = slider.value;
        SpawnAgents();
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
            float x = -Random.Range(0, spawnBoundary.transform.localScale.x);
            float z = -Random.Range(0, spawnBoundary.transform.localScale.z);

            GameObject agent = Instantiate(agentPrefab, new Vector3(x, 1, z), Quaternion.identity);
            agents.Add(agent);

        }

        /*
        float limit = Mathf.Ceil(Mathf.Sqrt(numOfAgents));
        float interval = sizeOfBuilding / limit;
        int counter = 0;

        for (int r = 0; r < limit; r++)
        {
            for (int c = 0; c < limit; c++)
            {
                if (counter < numOfAgents)
                {
                    float x = -((interval * r) + (interval / 2));
                    float z = -((interval * c) + (interval / 2));

                    GameObject agent = Instantiate(agentPrefab, new Vector3(x, 1, z), new Quaternion(0, 0, 0, 0));
                    agents.Add(agent);

                    counter++;
                }
            }
        }
        */
    }
}
