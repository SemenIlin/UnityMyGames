using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float FirstLinePos;
    public float LineDistance;
    public float SideSpeed;
    public bool CanPlay;

    public GameObject GamePlay;
    public PowerUpController PUController;
    public GameManagerCanvas GM;
    public Animator animator;

    private CapsuleCollider playerCollider;
    private Rigidbody rigidbody;
    
    private Vector3 startPlayerPosition;
    private Vector3 rbVelocity;

    private float lineNumber = 1;
    private float linesCount = 2;
    private float JumpSpeed = 15;
    private bool isJump;
            
    private void Start()
    {
        isJump = false;
        startPlayerPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        SwipeController.SwipeEvent += CheckInput;
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector3(0, Physics.gravity.y * 4, 0), ForceMode.Acceleration);

        if(isJump && IsGrounded())
        {
            isJump = false;
            AudioManager.Instance.StopPlayRun();
            AudioManager.Instance.PlayJumpEffect();
            rigidbody.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (CanPlay) 
        { 
            if (IsGrounded())
             {
                animator.ResetTrigger("Falling");
                if (isJump)
                 {
                     animator.SetTrigger("Jump");
                 }

                 AudioManager.Instance.PlayRun();
             }
             else if (rigidbody.velocity.y < -8)
             {
                 animator.SetTrigger("Falling");
                animator.ResetTrigger("Jump");
            } 
        }

        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, FirstLinePos + (lineNumber * LineDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.09f);
    }

    private void CheckInput(SwipeController.SwipeType type)
    {
        if (IsGrounded() && CanPlay)
        {
            if (type == SwipeController.SwipeType.UP)
            {
                isJump = true;
            }
        }
        
        int sign = 0;
        if (type == SwipeController.SwipeType.LEFT)
        {
            sign = -1;
        }
        else if (type == SwipeController.SwipeType.RIGHT)        
        {
            sign = 1;
        }
        lineNumber += sign;
        lineNumber = Mathf.Clamp(lineNumber, 0, linesCount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((!collision.gameObject.CompareTag("Trap") 
            && !collision.gameObject.CompareTag("DeathPlatform")
            && !collision.gameObject.CompareTag("Puddle"))
            || !CanPlay)
        {
            return;
        }
        if(IsImmortal && !collision.gameObject.CompareTag("DeathPlatform"))
        {
            collision.collider.isTrigger = true;
            return;
        }


        if(collision.gameObject.CompareTag("Trap"))
        {
            AudioManager.Instance.PlayTrapEffect();
        }
        if (collision.gameObject.CompareTag("Puddle"))
        {
            AudioManager.Instance.PlayPuddleEffect();
        }
        if (collision.gameObject.CompareTag("DeathPlatform"))
        {
            AudioManager.Instance.PlayFallingEffect();
        }

        AudioManager.Instance.StopPlayRun();

        StartCoroutine(Death());
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Coins":
                GM.AddCoins(GM.CoinsMultiplier);
                AudioManager.Instance.PlayCoinEffect();
                break;
            case "Multiplier":
                PUController.PowerUpUse(PowerUpController.PowerUp.Type.MULTIPLIER);
                AudioManager.Instance.PlayBonusEffect();
                break;
            case "Immortal":
                PUController.PowerUpUse(PowerUpController.PowerUp.Type.IMMORALITY);
                AudioManager.Instance.PlayBonusEffect();
                break;
            case "HexogenSpawn":
                PUController.PowerUpUse(PowerUpController.PowerUp.Type.COINS_SPWN);
                AudioManager.Instance.PlayBonusEffect();
                break;

            default:return;
        }

        Destroy(other.gameObject);
    }

    IEnumerator Death()
    {
        CanPlay = false;
        GamePlay.SetActive(false);
        PUController.ResetAllPowerUps();

        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2.5f);
        animator.ResetTrigger("Death");
        GM.Show();
    }

    public void ResetPosition()
    {
        transform.position = startPlayerPosition;
        lineNumber = 1;
    }

    public void Pause()
    {
        rbVelocity = rigidbody.velocity;
        rigidbody.isKinematic = true;
        animator.speed = 0;
    }

    public void UnPause()
    {
        rigidbody.isKinematic = false;
        rigidbody.velocity = rbVelocity;
        animator.speed = 1;
    }

    public void Respawn()
    {
        AudioManager.Instance.PlayRun();
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Falling");
        GamePlay.SetActive(true);
        StopAllCoroutines();
        IsImmortal = false;
        isJump = false;
    }

    public bool IsImmortal { get; set; }

}
