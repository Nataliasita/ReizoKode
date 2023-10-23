using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField] private float speed = 5;
    [SerializeField] private Rigidbody rb;
    [SerializeField] float horizontalMultiplier = 2;
    [SerializeField] private float jumpForce = 10f;

    private ScoreUIController scoreManager;

    private bool isJumping = false;
    float horizontalInput;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreUIController>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }
}
