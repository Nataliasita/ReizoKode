using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField] float horizontalMultiplier = 2;
    private ScoreUIController scoreManager;
    public CharacterController controller;
    public float speed = 6f;
    public float gravity =-9.81f;
    Vector3 velocity;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Transform groundCheck;
    private bool isGrounded;
    private bool activate = false;
    public float jumpForce = 3f;
    public Animator animator;
    public string movementAnimator;
    public string groundAnimator;
    public string hitAnimator;
    public string leftAnimator;
    public string rightAnimator;
    public string startAnimator;
    private SoundManager soundManager;

    public float addSpeed = 5.0f;

    float horizontalInput;
    float verticalInput;

    private void Awake()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }
    private void Start()
    {   
        scoreManager = FindObjectOfType<ScoreUIController>();
        animator = GetComponent<Animator>();

        animator.SetBool(startAnimator, true);
        transform.position = new Vector3(0f, 2.714f, 0f);
        Invoke("StopAnimation", 8f);
    }
    void Update() {
        

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
            soundManager.PlaySFX("Jump");
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {   
            animator.SetBool(leftAnimator , true);
        }else{
            animator.SetBool(leftAnimator , false);
        }
        
        if(Input.GetKey(KeyCode.RightArrow))
        {   
            animator.SetBool(rightAnimator , true);
        }else{
            animator.SetBool(rightAnimator , false);
        }   

    }

    private void FixedUpdate()
    {
        if(activate == true){
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            float actionMove = 1f;
            animator.SetFloat(movementAnimator, actionMove);

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            velocity.y +=gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            animator.SetBool(groundAnimator , controller.isGrounded);



            if(Input.GetKey(KeyCode.UpArrow))
            {   
                    Vector3 forwardMove = transform.forward * addSpeed  * Time.fixedDeltaTime;
                    Vector3 horizontalMove = transform.right * horizontalInput * addSpeed * Time.fixedDeltaTime * horizontalMultiplier;
                    Vector3 moveDirection = forwardMove + horizontalMove;
                    controller.Move(moveDirection);
            }else{
                    Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
                    Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
                    Vector3 moveDirection = forwardMove + horizontalMove;
                    controller.Move(moveDirection);
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            animator.SetBool(hitAnimator, true);
            soundManager.PlaySFX("Croack");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            animator.SetBool(hitAnimator, false);
        }
    }

    void StopAnimation()
    {   
        transform.position = new Vector3(0f, 0f, 0f);
        activate = true;
        animator.SetBool(startAnimator, false);

    }


}
