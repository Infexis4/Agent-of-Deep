using UnityEngine;

public class robEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;  
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;
   

    //referanslar
    private Animator anim;
    private Health playerHealth;
    private robPatrol robpatrol;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        robpatrol = GetComponentInParent<robPatrol>();
    }


    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //oyuncu görüþüne girdiðinde saldýrýr
        if(PlayerInSight())
        {
            if (cooldownTimer >= attackCoolDown && playerHealth.currentHealth > 0)
            {
                //saldýrý
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        if (robpatrol != null)
            robpatrol.enabled = !PlayerInSight();
        
    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
            
            //oyuncu menzildeyse hasar verir
            playerHealth.TakeDamage(damage);

        
    }

}
