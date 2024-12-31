using UnityEngine;
using System.Collections;

public class BobController : MonoBehaviour
{
    public GameObject gun;
    public GameObject one;
    public GameObject sword;
    public GameObject hammer;
    public GameObject slide;

    public float moveSpeed = 5f;
    private Vector2 movement;
    private string currentTag = "none"; // 當前觸發的 Tag
    private string previousTag = "none"; // 保存滑行前的動畫
    private bool isSliding = false; // 是否正在執行滑行動畫
    private void Start()
    {
        // 確保默認狀態
        gun.SetActive(false);
        sword.SetActive(false);
        one.SetActive(true);
        hammer.SetActive(false);
        slide.SetActive(false);
    }
    /*private enum ActionState
    {
        Normal,
        SwordAnimation,
        BowAnimation,
        HammerAnimation
    }*/

    //private ActionState currentState = ActionState.Normal; // 初始狀態
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // 左右
        movement.y = Input.GetAxisRaw("Vertical");   // 上下

        // 按鍵切換狀態
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentState == ActionState.SwordAnimation)
            {
                currentState = ActionState.Normal;
            }
            else
            {
                currentState = ActionState.SwordAnimation;
            }
            Debug.Log("Switched to " + currentState);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentState == ActionState.BowAnimation)
            {
                currentState = ActionState.Normal;
            }
            else
            {
                currentState = ActionState.BowAnimation;
            }
            Debug.Log("Switched to " + currentState);
        }

        // 執行對應的函式
        switch (currentState)
        {
            case ActionState.Normal:
                NormalAction();
                break;
            case ActionState.SwordAnimation:
                PlaySwordAnimation();
                break;
            case ActionState.BowAnimation:
                PlayBowAnimation();
                break;
        }*/
        // 檢查按鍵來觸發 Tag 動作
        if (Input.GetKeyDown(KeyCode.Alpha6)) // 按下鍵盤上的 "1"
        {
            TriggerActionByTag("sword");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) // 按下鍵盤上的 "2"（可擴展）
        {
            TriggerActionByTag("gun");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            TriggerActionByTag("hammer");
        }

        if (Input.GetKeyDown(KeyCode.E) && !isSliding)     //滑鏟
        {
            TriggerActionByTag("slide");
        }

        // 執行對應的函式
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

    // 根據 Tag 觸發對應的動作
    public void TriggerActionByTag(string tag)
    {
        if(tag == "none"){
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
    }
    public void PlayGunAnimation()
    {
        gun.SetActive(true);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(false);
        slide.SetActive(false);
    }
    public void PlaySwordAnimation()
    {
        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(true);
        hammer.SetActive(false);
        slide.SetActive(false);
    }
    public void PlayHammerAnimation()
    {
        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(true);
        slide.SetActive(false);
    }

    public void PlaySlideAnimation()
    {
        isSliding = true;
        gun.SetActive(false);
        one.SetActive(false);
        sword.SetActive(false);
        hammer.SetActive(false);
        slide.SetActive(true);
        transform.Translate(movement * moveSpeed * Time.deltaTime*2);
        // 啟動協程，延遲返回上一動畫
        StartCoroutine(ReturnToPreviousAnimation(0.4f));
    }
    private IEnumerator ReturnToPreviousAnimation(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的時間

        // 返回到上一個動畫
        TriggerActionByTag(previousTag);

        isSliding = false;
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
