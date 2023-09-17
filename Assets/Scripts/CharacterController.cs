using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private ParticleSystem runParticle;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform lauchPoint;

    [Header("Charge bomb")]
    [SerializeField] private float chargeTime;
    [SerializeField] private Slider chargeBar;
    PlayerHealth playerHealth;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool isBeingHit = false;
    AudioSource audioSource;
    DoorInteract door;
    private bool lockScript;
    public bool isWin;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        audioSource= GetComponent<AudioSource>();
        door = FindAnyObjectByType<DoorInteract>();
        LauchPoint();
    }

    private void LauchPoint()
    {
        lauchPoint.transform.localPosition = new Vector2(0.2f, lauchPoint.transform.localPosition.y);
        lauchPoint.transform.localRotation = Quaternion.Euler(0, 180f, 0);
    }

    private void FixedUpdate()
    {
        
        if (lockScript)
        {
            return;
        }
        Movement();
    }

    void Update()
    {
        if (playerHealth.isDead)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            rigidbody2D.freezeRotation = true;
            animator.SetBool("isDead", playerHealth.isDead);
            return;
        }
        if (lockScript)
        {
            return;
        }
        if (isBeingHit)
        {
            animator.SetBool("isHit", true);
            Invoke("SetAnimator", 0.1f);
        }
        Jumping();
        Shooting();
        GetInDoor();
    }

    private void GetInDoor()
    {
        if(door.isIn && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))){
            if (SceneManager.GetActiveScene().buildIndex + 1 > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", SceneManager.GetActiveScene().buildIndex + 1);
            }
            Time.timeScale = 0;
            isWin = true;
            runParticle.Stop();
            animator.SetBool("isRun", false);
            animator.SetBool("IsEnter", true);
            lockScript = true;
            
        }
    }
    private void SetAnimator()
    {
        isBeingHit = false;
        animator.SetBool("isHit", isBeingHit);
    }

    private void Movement()
    {
        animator.SetBool("isRun", false);
        float moveX = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(moveX * speed, rigidbody2D.velocity.y);
        if (moveX != 0)
        {
            
            animator.SetBool("isRun", true);
            if (runParticle.isStopped)
            {
                runParticle.Play();
            }
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            
        }
        if (!IsGround() || moveX == 0)
        {
            audioSource.Stop();
            runParticle.Stop();
        }
        if (moveX < 0)
        {
            ParticleTransform(moveX);
            LauchPointTransform(moveX);
            spriteRenderer.flipX = true;

        }
        if (moveX > 0)
        {
            ParticleTransform(moveX);
            LauchPointTransform(moveX);
            spriteRenderer.flipX = false;
        }
    }

    private void LauchPointTransform(float x)
    {
        if (!Mathf.Approximately(0, x))
        {
            lauchPoint.transform.localPosition = x > 0 ? new Vector2(0.2f, lauchPoint.transform.localPosition.y) : new Vector2(-0.2f, lauchPoint.transform.localPosition.y);
            lauchPoint.transform.localRotation = x > 0 ? Quaternion.Euler(0, 180f, 0) : Quaternion.identity;
        }
    }

    private void ParticleTransform(float x)
    {
        if (!Mathf.Approximately(0, x))
        {
            runParticle.transform.localRotation = x > 0 ? Quaternion.identity : Quaternion.Euler(0, 0, 180f);
        }
    }


    // Update is called once per frame


    private void Shooting()
    {
        if (Input.GetKey(KeyCode.M))
        {
            chargeTime += Time.deltaTime;
            chargeBar.value = chargeTime;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            if(chargeTime > 2)
            {
                Instantiate(projectile, lauchPoint.position, lauchPoint.rotation);
            }
            chargeTime = 0f;
            chargeBar.value = chargeTime;
        }
    }
    private void Jumping()
    {
        animator.SetBool("isJump", !IsGround());
        
        if (Input.GetButtonDown("Jump") && IsGround())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidbody2D.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                rigidbody2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        animator.SetFloat("jumpVel", Mathf.Floor(rigidbody2D.velocity.y));
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }
}
