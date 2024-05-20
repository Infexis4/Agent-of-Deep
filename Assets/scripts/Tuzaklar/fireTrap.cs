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
            //oyuncuyu tuza��n etkinle�mek �zere oldu�u konusunda uyarmak i�in renk de�i�tirir
            tetikleme = true;
            spriteRend.color = Color.blue;

            //s�reyi bekler, tuza�� etkinle�tirir, animasyonu �al��t�r�r ve tuza�� eski rengine �evirir 
            yield return new WaitForSeconds(openDelay);
            spriteRend.color = Color.white;
            aktif = true;
            anim.SetBool("aktif", true);

            //kullan�c�n�n belirledi�i s�re ge�tikten sonra tuza�� kapat�r, animasyonu sonland�r�r
            yield return new WaitForSeconds(openTime);
            aktif = false;
            tetikleme = false;
            anim.SetBool("aktif", false);
        }



    }
