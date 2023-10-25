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
    public Animator anim;
    private ScoreUIController scoreManager;

    private bool isJumping = false;
    float horizontalInput;
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }
    private void Start()
    {   
        scoreManager = FindObjectOfType<ScoreUIController>();
        anim = GetComponent<Animator>();
    }
    

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }else{
            anim.SetBool("Grounded", true);
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
        anim.SetBool("Grounded", false);
        soundManager.PlaySFX("Jump");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            anim.SetBool("Hit", false);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            anim.SetBool("Hit", true);
            soundManager.PlaySFX("Croack");
        }
    }

    private void OnCollisionExit(Collision collision){

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            anim.SetBool("Hit", false);
        }
    }

}
