using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L11A269Random : MonoBehaviour
{
    public List<GameObject> _TargetsList;

    private GameObject randomTemp;

    private Vector3 targetTemp;

    private int statePos, placeTemp;

    private int indexTemp;

    private void Awake() => RandomIndex();

    public void RandomIndex()
    {
        statePos = 0;

        for (int j = 0; j < _TargetsList.Count; j++)
        {
            int posnew = Random.Range(0, _TargetsList.Count);

            if (statePos != posnew)
            {
                indexTemp = _TargetsList[j].transform.GetSiblingIndex();

                targetTemp = _TargetsList[j].transform.localPosition;
                placeTemp = j;

                randomTemp = _TargetsList[j].gameObject;

                _TargetsList[j].transform.SetSiblingIndex(_TargetsList[posnew].transform.GetSiblingIndex());

                _TargetsList[j].transform.localPosition = _TargetsList[posnew].transform.localPosition;


                _TargetsList[posnew].transform.SetSiblingIndex(indexTemp);

                _TargetsList[posnew].transform.localPosition = targetTemp;
                _TargetsList[j] = _TargetsList[posnew];
                _TargetsList[posnew] = randomTemp;
            }

        }
    }
}
