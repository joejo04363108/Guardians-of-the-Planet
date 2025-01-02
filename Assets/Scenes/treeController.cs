using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeController : MonoBehaviour
{
    public GameObject[] treeParts; // 將 t01 ~ t06 拖到此陣列

    void Start()
    {
        // 初始隱藏所有 TilemapRenderer
        foreach (var part in treeParts)
        {
            var renderer = part.GetComponent<TilemapRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false; // 隱藏物件渲染
            }
        }
    }

    void Update()
    {
        // 按下 F 鍵並檢測是否有碰撞的子物件
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (var part in treeParts)
            {
                var renderer = part.GetComponent<TilemapRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = true; // 隱藏物件渲染
                }
            }
        }
    }

}
