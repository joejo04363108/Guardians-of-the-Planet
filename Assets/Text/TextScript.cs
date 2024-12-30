using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;

public class TextsScript : MonoBehaviour
{
    // Start is called before the first frame update
    FlowerSystem flowerSys;
    bool game = true;
    string name = "Text";

    bool isGameEnd = false;
    void Start()
    {
        flowerSys = FlowerManager.Instance.CreateFlowerSystem("FlowerSample",false);
        flowerSys.SetupDialog();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(game){
            flowerSys.ReadTextFromResource(name);
            game = false;
        }
        
        if (!isGameEnd)
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                // Continue the messages, stoping by [w] or [lr] keywords.
                flowerSys.Next();
            }
            if(Input.GetKeyDown(KeyCode.R)){
                // Resume the system that stopped by [stop] or Stop().
                flowerSys.Resume();
            }
        }
    }
}
