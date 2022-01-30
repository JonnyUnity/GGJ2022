using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private List<Vector3> _checkpoints;

    private void Awake()
    {
        _checkpoints = new List<Vector3>();

        var containerObj = GameObject.Find("Checkpoints");

        var triggers = containerObj.GetComponentsInChildren<LevelTrigger>();
        
        foreach(var trigger in triggers)
        {
            var pos = trigger.transform.position;
            _checkpoints.Add(pos);
        }

    }


    public Vector3 GetCheckpointPosition(int index)
    {
        return _checkpoints[index];
    }

}
