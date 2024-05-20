using UnityEngine;

public class canTopla : MonoBehaviour
{
    [SerializeField] private float canDeger;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().canEkle(canDeger);
            gameObject.SetActive(false);
        }
    }
}
