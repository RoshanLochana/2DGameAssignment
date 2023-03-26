using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBullets;
    [SerializeField] private AudioClip fireBulletSound;

    private Animator anim;
    private PlayerMovements playerMovements;
    private float cooldownTimer = Mathf.Infinity;
    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovements = GetComponent<PlayerMovements>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovements.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }


    private void Attack()
    {
        SoundManager.instance.PlaySound(fireBulletSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //Pool fireball
        fireBullets[FindFireBullets()].transform.position = firePoint.position;
        fireBullets[FindFireBullets()].GetComponent<FireObject>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBullets()
    {
        for (int i = 0; i < fireBullets.Length; i++)
        {
            if (!fireBullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
