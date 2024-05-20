using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
   [SerializeField] private AudioClip checkpointSound; //checkpoint alýndýðýnda bildirim sesi verir
    private Transform currentCheckpoint; //son alýnan checkpointi tutar
    private Health playerHealth;
    private UIManager uiManager;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {

        //checkpoint alýnýp alýnmadýðýný kontrol eder
        if(currentCheckpoint == null)
        {
            //oyun sonu ekranýný gösterir
            uiManager.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position; //oyuncu checkpointe girdiðini anlar

        playerHealth.Respawn();    //oyuncunun canýný fuller ve animasyonu sýfýrlar


    }

    //checkpoint aktif etmeyi saðlar
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //aktif ettiðimiz checkpointi hafýzada tutar
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //checkpoint collider kapatýr
            collision.GetComponent<Animator>().SetTrigger("appear");//checkpoint animasyonu tetikler
        }
    }

}
