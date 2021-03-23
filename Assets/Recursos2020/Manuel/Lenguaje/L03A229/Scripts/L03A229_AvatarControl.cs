using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L03A229_AvatarControl : MonoBehaviour
{
    [Header("Select Avatar")] public L03A229_Instructions _Instructions;
    [Header("Image Avatars")] public List<Image> _avatars;

    // Update is called once per frame
    void Update()
    {
        /* if(_Instructions._avatar != null)
        { */
            foreach (Image i in _avatars)
                i.sprite = _Instructions._avatar;
        //}
    }
}
