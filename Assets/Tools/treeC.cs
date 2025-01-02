using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeC : MonoBehaviour
{

    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject tree4;
    public mapController mapController;
    // Start is called before the first frame update
    SpriteRenderer treelayer1, treelayer2, treelayer3, treelayer4; 
    void Start()
    {
        treelayer1 = tree1.GetComponent<SpriteRenderer>();
        treelayer2 = tree2.GetComponent<SpriteRenderer>();
        treelayer3 = tree3.GetComponent<SpriteRenderer>();
        treelayer4 = tree4.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(treelayer1.sortingOrder == 0 && treelayer1.sortingOrder == 0){
            mapController.HidePolluted1();
        }
        if(treelayer3.sortingOrder == 0 && treelayer4.sortingOrder == 0){
            mapController.HidePolluted2();
        }
       
    }
}
