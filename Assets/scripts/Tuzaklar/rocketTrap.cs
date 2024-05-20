using UnityEngine;

public class rocketTrap : MonoBehaviour
{
   [SerializeField] private float saldiriBekleme;
   [SerializeField] private Transform atesNokta;
    [SerializeField] private GameObject[] roketMermi;
    private float beklemeZamanlayici;

    [Header("SFX")]
    [SerializeField] private AudioClip rocketSound;


    private void Saldir()
    {
        beklemeZamanlayici = 0;

        SoundManager.instance.PlaySound(rocketSound);

        roketMermi[bulRoket()].transform.position = atesNokta.position;
        roketMermi[bulRoket()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int bulRoket()
    {
        for(int i = 0; i < roketMermi.Length; i++)
        {
            if (!roketMermi[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        beklemeZamanlayici += Time.deltaTime;
        
        if (beklemeZamanlayici >= saldiriBekleme)
            Saldir();
    }
}
