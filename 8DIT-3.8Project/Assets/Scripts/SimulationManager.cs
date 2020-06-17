using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numOfAgents = 50;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numOfAgents; i++)
        {
            int x = Random.Range(-40, 0);
            int z = Random.Range(-40, 0);

            GameObject agent = Instantiate(agentPrefab, new Vector3(x, 1, z), new Quaternion(0, 0, 0, 0));
        }
    }
}
