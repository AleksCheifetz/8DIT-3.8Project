﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public GameObject agentPrefab;

    public Slider slider;
    static float numOfAgents;

    public int sizeOfBuilding = 50;

    List<GameObject> agents = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        numOfAgents = slider.value;
        SpawnAgents();
    }

    void Update()
    {
        numOfAgents = slider.value;
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

        SpawnAgents();
    }

    void SpawnAgents()
    {
        Debug.Log(agents.Count);

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
        Debug.Log(agents.Count);
    }
}
