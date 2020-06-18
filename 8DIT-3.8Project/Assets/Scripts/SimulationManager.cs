using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numOfAgents = 50;

    public int sizeOfBuilding = 50;

    // Start is called before the first frame update
    void Start()
    {
        float limit = Mathf.Ceil(Mathf.Sqrt(numOfAgents));
        int counter = 0;


        for (int r = 0; r < limit; r++)
        {
            for (int c = 0; c < limit; c++)
            {
                if (counter < numOfAgents)
                {
                    float x = -((sizeOfBuilding / limit) + (sizeOfBuilding / limit * r));
                    float z = -((sizeOfBuilding / limit) + (sizeOfBuilding / limit * c));

                    GameObject agent = Instantiate(agentPrefab, new Vector3(x, 1, z), new Quaternion(0, 0, 0, 0));

                    counter++;
                }
            }
        }
    }
}
