using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject[] _levelSpawns;
    private int latestSpawnIndex;


    private void Start()
    {
        latestSpawnIndex = 0;
    }

    public GameObject GetSpawn(int index)
    {
        return _levelSpawns[index];

    }


}
