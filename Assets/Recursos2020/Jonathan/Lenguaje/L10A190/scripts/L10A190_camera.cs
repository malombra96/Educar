using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L10A190_camera : MonoBehaviour
{
    public Transform target;
    public float xMin, xMax, yMin, yMax;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), transform.position.z);
    }
}
