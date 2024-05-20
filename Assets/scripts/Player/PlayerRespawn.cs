using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
   [SerializeField] private AudioClip checkpointSound; //checkpoint al�nd���nda bildirim sesi verir
    private Transform currentCheckpoint; //son al�nan checkpointi tutar
    private Health playerHealth;
    private UIManager uiManager;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {

        //checkpoint al�n�p al�nmad���n� kontrol eder
        if(currentCheckpoint == null)
        {
            //oyun sonu ekran�n� g�sterir
            uiManager.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position; //oyuncu checkpointe girdi�ini anlar

        playerHealth.Respawn();    //oyuncunun can�n� fuller ve animasyonu s�f�rlar


    }

    //checkpoint aktif etmeyi sa�lar
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //aktif etti�imiz checkpointi haf�zada tutar
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //checkpoint collider kapat�r
            collision.GetComponent<Animator>().SetTrigger("appear");//checkpoint animasyonu tetikler
        }
    }

}
