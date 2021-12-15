using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressTest : MonoBehaviour
{
    public GameObject prefab;
    // Start i s called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(prefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
