using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09A082_Model3D : MonoBehaviour
{
    void LateUpdate()
    {
        transform.Rotate(0f, 0f, -SimpleInput.GetAxis("Horizontal"), Space.World);
        transform.Rotate(SimpleInput.GetAxis("Vertical"),  0f, 0f, Space.Self);
    }

    public void Restart() => transform.eulerAngles = Vector3.zero;
}
