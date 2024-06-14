using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skeleton : MonoBehaviour, IDmg
{
    [SerializeField] Animator animator;
    [SerializeField] float skelSpeed;
    [SerializeField] Rigidbody2D skelRig;
    [SerializeField] Collider2D skelColl;
    [SerializeField] LayerMask playerLayer;
    Collider2D playerInColl;
    float skellRange = 10.0f;
    float coolDownTime = 0.0f;
    int horizontalMove = 1;
    bool isPlayerInRange;
    bool isOnLeftSide = true;
    bool isAlive = true;
    public static event Action OnSkeletonDead;

    PlayerHP playerHP = null;

    void Start()
    {

    }

    private void Update()
    {
        animator.SetFloat("speed", horizontalMove);
        if (!isOnLeftSide && horizontalMove == 1)
        {
            isOnLeftSide = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (isOnLeftSide && horizontalMove == -1)
        {
            isOnLeftSide = false;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void FixedUpdate()
    {
        skelRig.velocity = new Vector2(horizontalMove * skelSpeed, skelRig.velocity.y);
        playerInColl = Physics2D.OverlapCircle(transform.position, 2.5f, playerLayer);
        if (playerInColl != null && isAlive == true)
        {
            if (playerHP == null)
            {
                playerHP = playerInColl.gameObject.GetComponent<PlayerHP>();
            }
            if (coolDownTime <= 0.0f && playerHP.IsAlive)
            {
                Debug.Log("Szkielet Atakuje");
                coolDownTime = 1.5f;
                animator.SetTrigger("attack");
            }
        }
        coolDownTime -= Time.deltaTime;
        RaycastHit2D backHit = Physics2D.Raycast(transform.position, Vector2.left, skellRange, playerLayer);
        RaycastHit2D forwardHit = Physics2D.Raycast(transform.position, Vector2.right, skellRange, playerLayer);
        if (backHit.collider != null)
        {
            if (horizontalMove == 1)
            {
                horizontalMove = -1;
            }
        }
        else if (forwardHit.collider != null)
        {
            if (horizontalMove == -1)
            {
                horizontalMove = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ChangeDirection")
        {
            SkeletonChangeDirection();
        }
    }

    private void SkeletonChangeDirection()
    {
        if (horizontalMove == 1)
        {
            horizontalMove = -1;
        }
        else if (horizontalMove == -1)
        {
            horizontalMove = 1;
        }
    }

    public void IsDead()
    {
        horizontalMove = 0;
        animator.SetBool("alive", false);
        isAlive = false;
        skelRig.simulated = false;
        skelColl.enabled = false;
        OnSkeletonDead?.Invoke();
    }

    public void AttackPlayer()
    {
        if (playerInColl != null)
        {
            PlayerHP playerHP = playerInColl.gameObject.GetComponent<PlayerHP>();
            playerHP.ReciveDmg(30);
        }
    }
}
