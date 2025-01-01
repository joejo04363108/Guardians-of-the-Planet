using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestoryOnload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private static DestoryOnload instance;
    void Awake()
    {
            DontDestroyOnLoad(gameObject); // 設置為不銷毀
    }
    
}
