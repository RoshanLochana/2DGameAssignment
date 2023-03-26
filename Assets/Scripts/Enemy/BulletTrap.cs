using UnityEngine;

public class BulletTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] firebullets;
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip bulletSound;


    private void Attack()
    {
       
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(bulletSound);

        firebullets[FindFirebullets()].transform.position = firePoint.position;
        firebullets[FindFirebullets()].GetComponent<EnemyProjectile>().ActiveProjectile();
    }

    private int FindFirebullets()
    {
        for (int i = 0; i < firebullets.Length; i++)
        {
            if (!firebullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
