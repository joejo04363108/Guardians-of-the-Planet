using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    // 按鈕點擊事件
    public void TogglButton_1()
    {
         SceneManager.LoadScene("Start1");
        
    }

    public void TogglButton_2()
    {
        Debug.Log("123");
        
    }
    public void TogglButton_3()
    {
        SceneManager.LoadScene("Start0");
        
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("EarthBase");
    }
}