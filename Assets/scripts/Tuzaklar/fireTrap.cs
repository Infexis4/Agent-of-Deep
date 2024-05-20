using System.Collections;
using UnityEngine;

public class fireTrap : MonoBehaviour
{
    [SerializeField] private float hasar;

    [Header("Firetrap Timers")]
    [SerializeField] private float openDelay;
    [SerializeField] private float openTime;

    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("SFX")]
    [SerializeField] private AudioClip fireTrapSound;

    private bool tetikleme; //tuzak tetiklendiginde
    private bool aktif; //hasar vermeyi aktif eder

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && aktif)        
            playerHealth.TakeDamage(hasar);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!tetikleme)
                StartCoroutine(OpenFireTrap());
            if (aktif)
                collision.GetComponent<Health>().TakeDamage(hasar);
            SoundManager.instance.PlaySound(fireTrapSound);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerHealth = null;
    }
        private IEnumerator OpenFireTrap()
        {
            //oyuncuyu tuzaðýn etkinleþmek üzere olduðu konusunda uyarmak için renk deðiþtirir
            tetikleme = true;
            spriteRend.color = Color.blue;

            //süreyi bekler, tuzaðý etkinleþtirir, animasyonu çalýþtýrýr ve tuzaðý eski rengine çevirir 
            yield return new WaitForSeconds(openDelay);
            spriteRend.color = Color.white;
            aktif = true;
            anim.SetBool("aktif", true);

            //kullanýcýnýn belirlediði süre geçtikten sonra tuzaðý kapatýr, animasyonu sonlandýrýr
            yield return new WaitForSeconds(openTime);
            aktif = false;
            tetikleme = false;
            anim.SetBool("aktif", false);
        }



    }
