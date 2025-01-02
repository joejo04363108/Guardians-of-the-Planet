using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour
{
    public GameObject polluted1;
    public GameObject polluted2;
    public GameObject polluted3;
    private void Start()
    {
        polluted1.SetActive(true);
        polluted2.SetActive(true);
        polluted3.SetActive(true);
    }

    private void Update()
    {
    }

    public void HidePolluted1()
    {
        polluted1.SetActive(false);
    }
    public void HidePolluted2()
    {
        polluted2.SetActive(false);
    }
    public void HidePolluted3()
    {
        polluted3.SetActive(false);
    }
}
