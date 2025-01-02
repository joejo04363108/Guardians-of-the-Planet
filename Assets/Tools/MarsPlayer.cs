using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;

public class MarsPlayer : MonoBehaviour
{
    FlowerSystem flowerSys;
    // Start is called before the first frame update
    void Start()
    {
        this.flowerSys = FlowerManager.Instance.CreateFlowerSystem("Flower1",false);
        this.flowerSys.SetupDialog();
        this.flowerSys.SetTextList(new List<string>{"[show]NPC(基地人員):太空人你好，你要的金屬可能會出現在野外。[w][hide]"});
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
