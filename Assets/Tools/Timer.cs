using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int cnt = 0;

    public int tree_cnt = 0;

    public bool goodend = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tree_cnt >= 3){
            goodend = true;
        }
    }
}
