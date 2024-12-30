using UnityEngine;
using UnityEngine.UIElements;



public class keyFunction : MonoBehaviour
{

    public KeyCode Keyboard; 
    public UseTree tools; 

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(Keyboard)){
            //tools.run();
        }
        
    }
}