using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    public static Action<bool> IsRunning;
    public static Action<bool> IsJumping;
    public static Action<bool> IsAttacking;


    private bool mobile;
    //private PlayerAnimationController _playerAnimationController;

    private float horizontal;
    private float speed = 7f;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;

    private bool isJumping;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;
    private bool moblieJumping = false;

    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] Joystick joystick;
    public float flip;

    [SerializeField] BowCombat _bowCombat;
    [SerializeField] SwordCombat _swordCombat;

    public float KBForce = 5;
    public float KBCounter;
   // public float KBTotalTime;
    public bool KnockFromRight;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        _playerHealth=GetComponent<PlayerHealth>();
    }
    private void OnEnable()
    {

        GameManager.IsMobileON += IsMobileOn;
    }
    private void OnDisable()
    {
        GameManager.IsMobileON -= IsMobileOn;
    }

    private void Update()
    {
        flip = transform.localScale.x;
        if (joystick.Horizontal == 0 && _playerHealth.Health != 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        else if(joystick.Horizontal != 0 && _playerHealth.Health != 0)
        {
            horizontal = joystick.Horizontal;
        }
        if (joystick.Horizontal != 0 && _playerHealth.Health != 0 || horizontal != 0 && _playerHealth.Health != 0)
        {
            IsRunning?.Invoke(true);
        }
        else if(joystick.Horizontal == 0 && _playerHealth.Health != 0 && horizontal == 0)
        {
            IsRunning?.Invoke(false);
        }
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && _playerHealth.Health != 0)
        {
            jumpBufferCounter = jumpBufferTime;

        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0f && _playerHealth.Health != 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
       
        if (!IsGrounded())
        {
            IsJumping?.Invoke(true);
        }
        else
        {
            IsJumping?.Invoke(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && PlayerPrefs.GetInt("SwordPickedUp") == 1 && _playerHealth.Health != 0 && _bowCombat.CanChangeWeapon && _swordCombat.CanChangeWeapon)
        {
            PlayerPrefs.SetInt("Weapon", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerPrefs.GetInt("BowPickedUp") == 1 && _playerHealth.Health != 0 && _bowCombat.CanChangeWeapon && _swordCombat.CanChangeWeapon)
        {
            PlayerPrefs.SetInt("Weapon", 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && PlayerPrefs.GetInt("SpearPickedUp") == 1 && _playerHealth.Health != 0 && _bowCombat.CanChangeWeapon && _swordCombat.CanChangeWeapon)
        {
            PlayerPrefs.SetInt("Weapon", 3);
        }
        if (!mobile)
        {
            if (PlayerPrefs.GetInt("Weapon") > 0 && Input.GetKeyDown(KeyCode.Mouse0) && _playerHealth.Health != 0)
            {
                IsAttacking?.Invoke(true);
            }
        }
        if (IsGrounded())
        {
            moblieJumping = false;
        }
        if (_playerHealth.Health > 0)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
        }
        else
        {
            if (KnockFromRight)
            {
                rigidbody2D.velocity = new Vector2(-KBForce, KBForce);
            }
            if (!KnockFromRight)
            {
                rigidbody2D.velocity = new Vector2(KBForce, KBForce);
            }
            KBCounter -= Time.deltaTime;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
    public void JumpForMobile()
    {
        
    }
    public void OnJumpDown()
    {
        moblieJumping = true;
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            // _playerAnimationController.isGrounded = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (moblieJumping == true)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping && _playerHealth.Health != 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (moblieJumping == true && rigidbody2D.velocity.y > 0f && _playerHealth.Health != 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 1f);

            coyoteTimeCounter = 0f;
        }
    }
    public void OnLeftDown()
    {
        horizontal = -1;
        Debug.Log(horizontal);
    }
    public void OnRightDown()
    {
        horizontal = 1;

        Debug.Log(horizontal);
    }
  public void IsMobileOn(bool ismobile)
    {
        if (ismobile == true)
        {
            mobile = true;
        }
        else
        {
            mobile = false;
        }
    }
}
