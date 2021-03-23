using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09A129_FollowCamera : MonoBehaviour
{
    [Header("Player")] public Transform _player;
    void Update()
    {
        Vector3 temp = transform.position;

        temp.y = _player.position.y;

        if(temp.y < 0)
            temp.y = 0;
        if(temp.y > 18.5f)
            temp.y = 18.5f;

        transform.position = temp;
    }
}
