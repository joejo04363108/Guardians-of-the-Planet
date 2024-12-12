using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Transform target;       // Bob
    public Vector2 minBounds = new Vector2(-34f,-10f); // 最小邊界
    public Vector2 maxBounds = new Vector2(1f,10f);// 最大邊界

    void Start()
    {
        // 嘗試找到 Bob 作為目標
        FindTarget();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y);
        // 更新攝影機位置
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }


    private void FindTarget()
    {
        GameObject bob = GameObject.FindWithTag("bob");
        if (bob != null)
        {
            target = bob.transform;
        }
        else
        {
            Debug.LogWarning("Bob not found in the current scene.");
        }
    }
}
