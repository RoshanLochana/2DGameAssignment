using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float curruntHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float IFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        curruntHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }




    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Invunerability());

        //Player
        if (GetComponent<PlayerMovements>() != null)
            GetComponent<PlayerMovements>().enabled = true;
    }




    public void TakeDamage(float _damage)
    {
        curruntHealth = Mathf.Clamp(curruntHealth - _damage, 0, startingHealth);

        if (curruntHealth > 0)
        {
            //Player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else 
        {
            //Player dead

            if (!dead)
            {
                anim.SetTrigger("die");

                //Player
                if(GetComponent<PlayerMovements>() != null)
                GetComponent<PlayerMovements>().enabled = false;

                //Enemy
                if (GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;

                if(GetComponent<MeleeEnemy>() != null)
                GetComponent<MeleeEnemy>().enabled = false;

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
           

        }

    }

    public void AddHealth(float _value)
    {
        curruntHealth = Mathf.Clamp(curruntHealth + _value, 0, startingHealth); 
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IFrameDuration / (numberOfFlashes) * 2);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(IFrameDuration / (numberOfFlashes) * 2);
        }

        Physics2D.IgnoreLayerCollision(10, 11, true);
    }

}
