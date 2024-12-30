using UnityEngine;

public class aController : MonoBehaviour
{
    public void SendMessageToSword()
    {
        Debug.Log("開始尋找物件 B");
        GameObject bObject = GameObject.FindWithTag("bob");
        if (bObject != null)
        {
            Debug.Log("找到物件 B：" + bObject.name);

            Transform swordTransform = bObject.transform.Find("sword");
            if (swordTransform != null)
            {
                Debug.Log("找到子物件 sword：" + swordTransform.name);

                swordController swordScript = swordTransform.GetComponent<swordController>();
                if (swordScript != null)
                {
                    Debug.Log("找到子物件的腳本，執行動作！");
                    swordScript.Start();
                }
                else
                {
                    Debug.LogWarning("swordController 腳本未找到！");
                }
            }
            else
            {
                Debug.LogWarning("未找到子物件 'sword'！");
            }
        }
        else
        {
            Debug.LogWarning("未找到 Tag 為 'btag' 的物件！");
        }
    }

    private void Update()
    {
        // 測試：按下空白鍵發送訊息給 B 的子物件
        if (Input.GetKeyDown(KeyCode.L))
        {
            SendMessageToSword();
        }
    }
}
