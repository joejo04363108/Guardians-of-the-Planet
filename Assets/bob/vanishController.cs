using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vanishController : MonoBehaviour
{
    public GameObject vanish; 
    void Start()
    {
        vanish.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        vanish.SetActive(true);
    }
}
