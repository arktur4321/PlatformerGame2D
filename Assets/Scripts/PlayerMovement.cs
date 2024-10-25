using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D stickManRig;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] MenuHandler menuHandler;
    public float speed = 4;
    public float jumpForce = 2;
    public int damageDealt = 0;
    public bool IsBlocking { get { return isBlocking; } }
    private PlayerJoysticks playerJoysticks;
    float potionTime = 20;
    float bigPotionTime = 40;
    float horizontalMove;
    bool isGrounded = true;
    bool isJumping = false;
    bool isOnLeftSide = true;
    bool isBlocking = false;
    bool isPotionActive = false;
    bool isBigPotionActive = false;
    Collider2D enemyInColl;


    private void Awake()
    {
        playerJoysticks = new PlayerJoysticks();
    }


    private void Start()
    {
        GameManager.instance.isPlayerInScene = true;
        playerJoysticks.Player.Jumping.performed += JumpingPerformed;
        playerJoysticks.Player.Attacking.performed += AttackPerformed;
        playerJoysticks.Player.Blocking.performed += BlockPerformed;

    }

    private void JumpingPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isJumping == false && animator.GetBool("alive") == true)
        {
            isGrounded = false;
            isJumping = true;
        }
    }

    private void AttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        animator.SetBool("block", false);
        isBlocking = false;
        animator.SetTrigger("attack");
    }

    private void BlockPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isBlocking == true)
        {
            animator.SetBool("block", false);
            isBlocking = false;
        }
        else
        {
            animator.SetBool("block", true);
            isBlocking = true;
        }
    }

    private void OnEnable()
    {
        playerJoysticks.Player.Enable();
    }

    void Update()
    {


        if (!MenuHandler.IsGamePaused)
        {
            //Movement
            if (animator.GetBool("alive") == true)
                horizontalMove = playerJoysticks.Player.Movement.ReadValue<Vector2>().x;
            else
                horizontalMove = 0.0f;
            animator.SetFloat("speed", horizontalMove);
            if (!isOnLeftSide && horizontalMove > 0)
            {
                isOnLeftSide = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (isOnLeftSide && horizontalMove < 0)
            {
                isOnLeftSide = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            //Jumping
            if (Input.GetKeyDown(KeyCode.Space) && isJumping == false && animator.GetBool("alive") == true)
            {
                isGrounded = false;
                isJumping = true;
            }


            //Blocking
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetBool("block", true);
                isBlocking = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                animator.SetBool("block", false);
                isBlocking = false;
            }


            if (GameManager.instance.isPlayerInScene == false && SceneManager.GetActiveScene().buildIndex == 0)
            {
                GameManager.instance.PlayerIsBack(transform);
                GameManager.instance.isPlayerInScene = true;
            }


            if (isPotionActive)
            {
                potionTime -= Time.deltaTime;
            }
            if (isBigPotionActive)
            {
                bigPotionTime -= Time.deltaTime;
            }

            if (potionTime <= 0)
            {
                isPotionActive = false;
                speed = 4;
                potionTime = 3;
            }
            if (bigPotionTime <= 0)
            {
                isBigPotionActive = false;
                speed = 4;
                bigPotionTime = 6;
            }
        }


    }

    private void FixedUpdate()
    {
        stickManRig.velocity = new Vector2(horizontalMove * speed, stickManRig.velocity.y);
        if (isGrounded == false)
        {
            stickManRig.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = true;
        }
        enemyInColl = Physics2D.OverlapCircle(transform.position, 2.5f, enemyLayers);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 27)
        {
            isGrounded = true;
            isJumping = false;
        }

        if (collision.gameObject.layer == 10)
        {
            speed = speed * 2;
            isPotionActive = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 11)
        {
            speed = speed * 2;
            isBigPotionActive = true;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            isGrounded = true;
            isJumping = false;
        }
    }


    public void HitEnemy()
    {
        if (enemyInColl != null)
        {
            EnemyHP enemyHP = enemyInColl.gameObject.GetComponent<EnemyHP>();
            enemyHP.ReciveDmg(50);
            damageDealt += 50;
        }
    }
}
