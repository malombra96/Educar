using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5A259_camera : MonoBehaviour
{
    public bool cameraBounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    public Transform player;


    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(player.localPosition.x, player.localPosition.y, this.transform.localPosition.z);


    }
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),transform.position.z);
    }

    public void borrar() {
        PlayerPrefs.DeleteAll();
    }
}
