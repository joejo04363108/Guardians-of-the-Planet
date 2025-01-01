using UnityEngine;

public class SmoothFollowPlayer : MonoBehaviour
{
    public GameObject player; // 玩家角色的 Transform
    public Vector3 offset;   // 相機與玩家的偏移量
    public float smoothSpeed = 0.125f; // 平滑速度

    void Start(){
        if(player == null) player = GameObject.FindWithTag("bob");
    }
    void LateUpdate()
    {
        // 計算目標位置
        Vector3 targetPosition = player.transform.position + offset;

        // 平滑移動到目標位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}