using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Joystick joystick;
    public Rigidbody2D rb;
    Animator animator;

    public float dashBoost = 2f;
    private float dashTime;
    public float DashTime;
    private bool once;

    public Vector3 moveInput;

    public GameObject damPopUp;
    public LosePanel losePanel;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Reset moveInput mỗi frame
        moveInput = Vector3.zero;

        // Nhập từ bàn phím
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput.x -= 1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveInput.x += 1f;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveInput.y += 1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveInput.y -= 1f;
        }

        Debug.Log("Keyboard Input: " + moveInput);

        // Nhập từ joystick (nếu có)
        if (joystick != null)
        {
            moveInput.x += joystick.Horizontal;
            moveInput.y += joystick.Vertical;
        }

        Debug.Log("Combined Input: " + moveInput);

        // Chuẩn hóa đầu vào để tránh tăng tốc khi đi chéo
        moveInput = moveInput.normalized;

        // Áp dụng vận tốc cho Rigidbody2D
        rb.velocity = moveSpeed * moveInput;

        // Điều chỉnh Animator
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        // Dash (lăn)
        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            animator.SetBool("Roll", true);
            moveSpeed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once)
        {
            animator.SetBool("Roll", false);
            moveSpeed -= dashBoost;
            once = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        // Xoay mặt nhân vật
        
    }


    public void TakeDamageEffect(int damage)
    {
        if (damPopUp != null)
        {
            GameObject instance = Instantiate(damPopUp, transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
            Animator animator = instance.GetComponentInChildren<Animator>();
            animator.Play("red");
        }
        if (GetComponent<Health>().isDead)
        {
            losePanel.Show();
        }
    }
}
