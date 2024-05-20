using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]

    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("koruma")]

    [SerializeField] private float hasarAlma;
    [SerializeField] private int kalkan;
    private SpriteRenderer spriteHasar;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteHasar = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        

        if(currentHealth > 0)
        {
            //oyuncu hasar alma
            anim.SetTrigger("hurt");
            //hasar alma anim
            StartCoroutine(Hasarsizlik());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            //oyuncu ölme
            if(!dead)
            {
              
                //bütün özellikleri kapatýr
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("isGrounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }           
        }
    }

   public void canEkle(float _deger)
    {
        currentHealth = Mathf.Clamp(currentHealth + _deger, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        canEkle(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Hasarsizlik());

        //respawndan sonra bütün özellikleri açar
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    private IEnumerator Hasarsizlik()
    {
        invulnerable = true;

        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < kalkan ; i++)
			{
            spriteHasar.color = new Color(1, 0, 0, 0.5f);

            yield return new WaitForSeconds(hasarAlma / (kalkan * 2));

            spriteHasar.color = Color.white;

            yield return new WaitForSeconds(hasarAlma / (kalkan * 2));
        }

        //hasarsýzlýk süresi

        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
