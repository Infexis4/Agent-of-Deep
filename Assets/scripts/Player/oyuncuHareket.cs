using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float HorizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        //unity içindeki özelliklere eriþebilmeyi saðlar
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();       
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");      

        //karakterin harekete geçtiði yöne dönmesini saðlar
        if (HorizontalInput > 0.01f)
            transform.localScale = Vector3.one;

        else if (HorizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);          
        

        //animasyon kurulumu
        anim.SetBool("isWalking", HorizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded());       


        //duvardan zýplama iþlevi
        if (wallJumpCooldown < 0.2f)
        {          
            //Karakter saða-sola hareketini saðlar
            rb.velocity = new Vector2(HorizontalInput * walkingSpeed, rb.velocity.y);            

            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else
                rb.gravityScale = 3;

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
                    SoundManager.instance.PlaySound(jumpSound);
            }
                
        }

        else
            wallJumpCooldown += Time.deltaTime;

    }
        

    
    private void Jump()
    {
        //sýnýrsýz zýplamayý engellemeyi saðlar
        if (isGrounded())
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }

        else if(onWall() && !isGrounded())
        {
            if(HorizontalInput == 0)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 7, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 2, 4);

            wallJumpCooldown = 0;
            
        }
        
        
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //ateþ etme
    public bool canAttack()
    {
        return HorizontalInput == 0 && isGrounded() && !onWall();
    }
}
