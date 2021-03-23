using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_BehaviourGrid : MonoBehaviour
{
    [Header("Points Right")] public List<string> _points;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<Image>().color = Color.clear;


        AsignCoordenada();
    }

    // Update is called once per frame
    void AsignCoordenada()
    {
        for (int j = 0; j < 17; j++)
        {
            for (int i = 0; i < 17; i++)
            {
                int n = (i + (17 * j));
                var x = transform.GetChild(n);

                x.name = "[" + (i-8) + "," + (8-j) + "]";

            }
        }
    }

    public void SetPoint(string x,string y,bool state)
    {
        string xy = "[" + x + "," + y + "]";

        if(state)
            _points.Add(xy);
        else
            _points.Remove(xy);

        for (int i = 0; i < transform.childCount; i++)
            foreach (string p in _points)
                transform.GetChild(i).GetComponent<Image>().color = _points.Contains(transform.GetChild(i).name)? Color.white : Color.clear;
    }

    public Vector3 GetPositionPoint(string p)
    {
        for (int i = 0; i < transform.childCount; i++)
            if(transform.GetChild(i).name == p)
                return transform.GetChild(i).transform.position;
                
        return Vector3.zero;
    }

    public Vector3 GetAnchoredPosition(string p)
    {
        for (int i = 0; i < transform.childCount; i++)
            if(transform.GetChild(i).name == p)
                return transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
                
        return Vector3.zero;
    }

    public void ResetGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
            foreach (string p in _points)
                transform.GetChild(i).GetComponent<Image>().color = Color.clear;

        _points.Clear();
    }
}
