using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;
using UnityEngine.SceneManagement;

public class TextsScript : MonoBehaviour
{
    // Start is called before the first frame update
    FlowerSystem flowerSys;

    public string str;
    public string flowers;

    bool isGameEnd = false;
    void Start()
    {
        this.flowerSys = FlowerManager.Instance.CreateFlowerSystem(flowers);
        this.flowerSys.SetupDialog();
        this.flowerSys.SetTextList(new List<string>{str});

        Debug.Log("flower");
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (!isGameEnd)
        {

            if(Input.GetKeyDown(KeyCode.Escape)){
                // Continue the messages, stoping by [w] or [lr] keywords.
                flowerSys.RemoveDialog();
            }
            
            if(Input.GetKeyDown(KeyCode.Space)){
                // Continue the messages, stoping by [w] or [lr] keywords.
                flowerSys.Next();
            }
            if(Input.GetKeyDown(KeyCode.R)){
                flowerSys.Resume();
            }
        }

        
    }

    bool IsSceneLoaded(string name)
    {
        Scene scene = SceneManager.GetSceneByName(name);
        return scene.isLoaded;
    }
}
