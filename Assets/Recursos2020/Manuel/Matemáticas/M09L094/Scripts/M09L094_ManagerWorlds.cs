using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09L094_ManagerWorlds : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    public List<M09L094_World> _worlds;

    GameObject _current;

    // Start is called before the first frame update
    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        
        foreach (M09L094_World world in _worlds)
            world.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _current = _controlNavegacion.GetLayoutActual();

        if(_current)
        {
            foreach (M09L094_World world in _worlds)
                world.gameObject.SetActive(world._layouts.Contains(_current.GetComponent<BehaviourLayout>()));
        }
        else
        {
            foreach (M09L094_World world in _worlds)
                world.gameObject.SetActive(false);
        }
    }
}
