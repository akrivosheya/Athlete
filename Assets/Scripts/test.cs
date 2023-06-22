using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Race;

public class test : MonoBehaviour
{
    private RunnerScanner _scan;
    void Start()
    {
        _scan = GetComponent<RunnerScanner>();
    }

    void Update()
    {
        if(!_scan.CanRunToFirstRoad())
        {
            Debug.Log("Is");
        }
    }
}
