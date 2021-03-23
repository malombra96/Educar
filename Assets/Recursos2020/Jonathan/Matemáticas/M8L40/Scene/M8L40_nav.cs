using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8L40_nav : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Navbar;

    private void OnEnable()
    {
        Navbar.SetActive(true);
    }

    private void OnDisable()
    {
        Navbar.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
