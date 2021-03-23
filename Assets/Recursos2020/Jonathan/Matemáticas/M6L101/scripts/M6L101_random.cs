using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M6L101_random : MonoBehaviour
{
    public List<GameObject> _TargetsList,elemento1,elemento2;

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
                targetTemp = _TargetsList[j].GetComponent<RectTransform>().position;
                placeTemp = j;

                randomTemp = _TargetsList[j].gameObject;

                _TargetsList[j].GetComponent<RectTransform>().position = _TargetsList[posnew].GetComponent<RectTransform>().position;

                _TargetsList[posnew].GetComponent<RectTransform>().position = targetTemp;
                _TargetsList[j] = _TargetsList[posnew];
                _TargetsList[posnew] = randomTemp;
            }

        }


    }

    private void Start()
    {
        for (int j = 0; j < _TargetsList.Count; j++)
        {
            _TargetsList[j].GetComponent<M6L101_drawToggle>()._point = _TargetsList[j].GetComponent<RectTransform>().position;
            _TargetsList[j].GetComponent<M6L101_drawToggle>()._point.z = -9.6f;
        }
    }
}
