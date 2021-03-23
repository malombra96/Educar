using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaFinanciero_FollowCamera : MonoBehaviour
{
    [Header("Player")] public Transform _player;
    void Update()
    {
        Vector3 temp = transform.position;

        temp.y = _player.position.y;

        if(temp.y < 0)
            temp.y = 0;
        if(temp.y > 8.85f)
            temp.y = 8.85f;

        transform.position = temp;
    }
}
