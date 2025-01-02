using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private Transform target;       // Bob
    private Vector2 minBounds; // 最小邊界  = new Vector2(-34f,-10f)
    private Vector2 maxBounds;// 最大邊界 = new Vector2(1f,10f)
    public Tilemap tilemap;

    void Start()
    {
        // 嘗試找到 Bob 作為目標
        FindTarget();
    }

    void Update(){
        FindTarget();
    }
    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 newPosition = transform.position;
        // 獲取 Tilemap 的世界邊界
        BoundsInt cellBounds = tilemap.cellBounds;
        Vector3Int min = cellBounds.min;
        Vector3Int max = cellBounds.max;
        // 將格子座標轉為世界座標
        minBounds = tilemap.CellToWorld(min);
        maxBounds = tilemap.CellToWorld(max);
        // 修正邊界以考慮 Tilemap 尺寸
        /*Vector3 tileSize = tilemap.layoutGrid.cellSize;
        minBounds += new Vector2(tileSize.x / 2, tileSize.y / 2);
        maxBounds -= new Vector2(tileSize.x / 2, tileSize.y / 2);*/
        // 進一步調整邊界偏移
        minBounds += new Vector2(9f, 7.6f);
        maxBounds += new Vector2(-11.5f, -5.4f);
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
