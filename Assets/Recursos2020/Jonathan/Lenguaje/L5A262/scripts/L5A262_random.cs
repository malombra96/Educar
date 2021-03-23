using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5A262_random : MonoBehaviour
{
    public List<GameObject> _TargetsList;

    private GameObject randomTemp;

    private Vector3 targetTemp;

    private int statePos, placeTemp;

    private void Awake()
    {
        statePos = 0;

        for (int j = 0; j < _TargetsList.Count; j++)
        {
            int posnew = Random.Range(0, _TargetsList.Count - 1);

            if (statePos != posnew)
            {
                targetTemp = _TargetsList[j].transform.localPosition;
                placeTemp = j;

                randomTemp = _TargetsList[j].gameObject;

                _TargetsList[j].transform.localPosition = _TargetsList[posnew].transform.localPosition;

                _TargetsList[posnew].transform.localPosition = targetTemp;
                _TargetsList[j] = _TargetsList[posnew];
                _TargetsList[posnew] = randomTemp;
            }

        }
    }
    private void Start()
    {
        
    }
}
