using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator animator;
    float jumpForce = 480.0f;
    float walkForce = 80.0f;
    float maxWalkSpeed = 2.0f;
    void Start()
    {
        Application.targetFrameRate = 60;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public IEnumerator TriggerStatus()
    {
        animator.SetTrigger("RunTrigger");
        yield return new WaitForSeconds(1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.AddForce(transform.up * jumpForce);
            animator.SetTrigger("JumpTrigger");
        }
        int Key = 0;
        if (Input.GetKey(KeyCode.D))
        {
            Key = 1;
            animator.SetTrigger("RunTrigger");
            //StartCoroutine(TriggerStatus());
        }
        if (Input.GetKeyUp(KeyCode.D)) 
        {
            animator.ResetTrigger("RunTrigger");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.2f, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Key = -1;
            animator.SetTrigger("RunTrigger");
            //StartCoroutine(TriggerStatus());
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.ResetTrigger("RunTrigger");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.2f, 0, 0);
        }
        float speedx = Mathf.Abs(rigidbody2D.velocity.x);
        if (speedx < maxWalkSpeed)
        {
            rigidbody2D.AddForce(transform.right * Key * walkForce);
        }
        if (Key != 0)
        {
            transform.localScale = new Vector2(Key * 4f, 4f);
        }
        if (rigidbody2D.velocity.y == 0)
        {
            animator.speed = speedx / 2.0f;
        }
        else
        {
            animator.speed = 1.0f;
        }

    }
}

