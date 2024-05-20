using UnityEngine;

public class oyuncuSaldiri : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform bulPoint;
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private AudioClip bulletSound;

    private Animator anim;
    private oyuncuHareket oyuncuHareket;
    private float coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        oyuncuHareket = GetComponent<oyuncuHareket>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCoolDown && oyuncuHareket.canAttack())
            Attack();

        coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(bulletSound);
        anim.SetTrigger("isAttack");
        coolDownTimer = 0;



        bullet[FindBullet()].transform.position = bulPoint.position;

        bullet[FindBullet()].GetComponent<mermi>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindBullet()
    {
        for(int i = 0; i < bullet.Length; i++)
        {
            if (!bullet[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
