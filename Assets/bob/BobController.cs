using UnityEngine;
using System.Collections;

public class BobController : MonoBehaviour
{
    public GameObject gun;
    public GameObject one;
    public GameObject sword;
    public GameObject hammer;
    public GameObject slide;
    public GameObject hit;
    public GameObject vanish; // 新增消失動畫物件

    public float moveSpeed = 5f;
    private Vector2 movement;
    private string currentTag = "none"; // 當前觸發的 Tag
    private string previousTag = "none"; // 保存滑行前的動畫
    private bool isSliding = false; // 是否正在執行滑行動畫

    public HealthBar healthBar; // 引用健康條

    private void Start()
    {
        // 確保默認狀態
        gun.SetActive(false);
        sword.SetActive(false);
        one.SetActive(true);
        hammer.SetActive(false);
        slide.SetActive(false);
        hit.SetActive(false);
        vanish.SetActive(false); // 初始化消失動畫為禁用
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // 左右
        movement.y = Input.GetAxisRaw("Vertical");   // 上下

        // 動作邏輯
        if (Input.GetKeyDown(KeyCode.Alpha6)) // 按鍵觸發劍動畫
        {
            TriggerActionByTag("sword");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) // 按鍵觸發槍動畫
        {
            TriggerActionByTag("gun");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) // 按鍵觸發槌動畫
        {
            TriggerActionByTag("hammer");
        }
        if (Input.GetKeyDown(KeyCode.E) && !isSliding) // 滑鏟
        {
            TriggerActionByTag("slide");
        }

        // 健康值檢查
        if (healthBar.getHealth() <= 0)
        {
            TriggerActionByTag("vanish");
        }

        // 根據標籤觸發動作
        switch (currentTag)
        {
            case "sword":
                PlaySwordAnimation();
                break;
            case "gun":
                PlayGunAnimation();
                break;
            case "hammer":
                PlayHammerAnimation();
                break;
            case "slide":
                PlaySlideAnimation();
                break;
            case "hit":
                PlayHitAnimation();
                break;
            case "vanish":
                PlayVanishAnimation();
                break;
            default:
                NormalAction();
                break;
        }
    }

    void FixedUpdate()
    {
        // 移動角色
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    public void TriggerActionByTag(string tag)
    {
        if (tag == "none")
        {
            currentTag = "none";
            Debug.Log("Current action triggered by tag: " + currentTag);
            return;
        }

        if (sword.CompareTag(tag))
        {
            currentTag = "sword";
        }
        else if (gun.CompareTag(tag))
        {
            currentTag = "gun";
        }
        else if (hammer.CompareTag(tag))
        {
            currentTag = "hammer";
        }
        else if (slide.CompareTag(tag))
        {
            previousTag = currentTag; // 記錄當前動畫
            currentTag = "slide";
        }
        else if (hit.CompareTag(tag))
        {
            previousTag = currentTag; // 記錄當前動畫
            currentTag = "hit";
        }
        else if (vanish.CompareTag(tag))
        {
            previousTag = currentTag; // 記錄當前動畫
            currentTag = "vanish";
        }
        else
        {
            currentTag = "none";
        }
        Debug.Log("Current action triggered by tag: " + currentTag);
    }

    private void NormalAction()
    {
        gun.SetActive(false);
        one.SetActive(true);
        sword.SetActive(false);
        hammer.SetActive(false);
        slide.SetActive(false);
        hit.SetActive(false);
        vanish.SetActive(false);
    }

    public void PlayVanishAnimation()
    {
        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(false);
        slide.SetActive(false);
        hit.SetActive(false);
        vanish.SetActive(true);

        // 啟動協程，延遲返回上一動畫
        StartCoroutine(EndAnimation(1f));
    }

    public void PlayGunAnimation()
    {
        gun.SetActive(true);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(false);
        slide.SetActive(false);
        hit.SetActive(false);
        vanish.SetActive(false);
    }

    public void PlaySwordAnimation()
    {
        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(true);
        hammer.SetActive(false);
        slide.SetActive(false);
        hit.SetActive(false);
        vanish.SetActive(false);
    }

    public void PlayHammerAnimation()
    {
        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(true);
        slide.SetActive(false);
        hit.SetActive(false);
        vanish.SetActive(false);
    }

    public void PlaySlideAnimation()
    {
        isSliding = true;
        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(false);
        slide.SetActive(true);
        hit.SetActive(false);
        vanish.SetActive(false);
        transform.Translate(movement * moveSpeed * Time.deltaTime*2);
        // 啟動協程，延遲返回上一動畫
        StartCoroutine(ReturnToPreviousAnimation(0.4f));
    }

    public void PlayHitAnimation()
    {
        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(false);
        slide.SetActive(false);
        hit.SetActive(true);
        vanish.SetActive(false);

        // 啟動協程，延遲返回上一動畫
        StartCoroutine(ReturnToPreviousAnimation(0.25f));
    }

    public IEnumerator ReturnToPreviousAnimation(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的時間

        // 返回到上一個動畫
        TriggerActionByTag(previousTag);

        isSliding = false;
    }
    public IEnumerator EndAnimation(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的時間

        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(false);
        slide.SetActive(false);
        hit.SetActive(false);
        vanish.SetActive(false);
    }

    private static BobController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
