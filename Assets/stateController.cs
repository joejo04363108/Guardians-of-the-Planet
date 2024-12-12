using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class stateController : MonoBehaviour
{
    public GameObject walk;
    public GameObject sword;
    public GameObject gun;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        walk.SetActive(false);
        sword.SetActive(false);
        gun.SetActive(false);
        
    }
    private enum ActionState
    {
        Normal,
        SwordAni,
        GunAni
    }

    private ActionState currentState = ActionState.Normal;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentState == ActionState.SwordAni)
            {
                currentState = ActionState.Normal;
            }
            else
            {
                currentState = ActionState.SwordAni;
            }
            Debug.Log("Switched to " + currentState);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentState == ActionState.GunAni)
            {
                currentState = ActionState.Normal;
            }
            else
            {
                currentState = ActionState.GunAni;
            }
            Debug.Log("Switched to " + currentState);
        }

        // 執行對應的函式
        switch (currentState)
        {
            case ActionState.Normal:
                NormalAction();
                break;
            case ActionState.SwordAni:
                PlaySwordAnimation();
                break;
            case ActionState.GunAni:
                PlayGunAnimation();
                break;
        }
        Vector3 newPosition = transform.position;
        newPosition.x = target.position.x - 7.5f;
        newPosition.y = target.position.y + 3.65f;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    private void NormalAction()
    {
        gun.SetActive(false);
        walk.SetActive(true);
        sword.SetActive(false);
    }
    private void PlayGunAnimation()
    {
        gun.SetActive(true);
        walk.SetActive(false);
        sword.SetActive(false);

    }
    private void PlaySwordAnimation()
    {
        gun.SetActive(false);
        walk.SetActive(false);
        sword.SetActive(true);

    }
    private static stateController instance;

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
