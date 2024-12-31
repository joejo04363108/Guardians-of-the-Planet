using UnityEngine;

public class aController : MonoBehaviour
{
    private string msg;
    public void SendMessageToBob()
    {
        // 找到具有 Tag "btag" 的物件
        GameObject bObject = GameObject.FindWithTag("bob");
        if (bObject != null)
        {
            // 獲取 bObject 的 bController 腳本
            BobController bScript = bObject.GetComponent<BobController>();
            if (bScript != null)
            {
                msg = "sword";
                // 呼叫 bController 中的執行方法
                bScript.TriggerActionByTag(msg);
            }
            else
            {
                Debug.LogWarning("bController 腳本未找到！");
            }
        }
        else
        {
            Debug.LogWarning("未找到 Tag 為 'bob' 的物件！");
        }
    }

    private void Update()
    {
        // 測試：按下空白鍵發送訊息
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessageToBob();
        }
    }
}
