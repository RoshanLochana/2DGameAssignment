using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private float wallJumpCooldown;
    private BoxCollider2D boxCollider;
    private float horizonatlInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;



    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        horizonatlInput = Input.GetAxis("Horizontal");
      



        //Flip charcter
        if (horizonatlInput > 0.01f)
            transform.localScale = new Vector3(5,5,5);

        else if (horizonatlInput < -0.01f)
            transform.localScale = new Vector3(-5,5,5);

       
          


        //Set animetion para

        anim.SetBool("run", horizonatlInput != 0);
        anim.SetBool("grounded", isGrounded());


        //Wall jump
        if (wallJumpCooldown < 0.2f)
        {
           
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 2.5f;
           
            if (Input.GetKey(KeyCode.Space))
                Jump();

            if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
                SoundManager.instance.PlaySound(jumpSound);

        }
        else
            wallJumpCooldown +=Time.deltaTime;
            
    }

    private void Jump()
    {
        if (isGrounded())
        {
           
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");

        }
        else if (onWall() && !isGrounded())
        {
            if (horizonatlInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 30, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 30, 6);

            wallJumpCooldown = 0;

        }

    }

  

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizonatlInput == 0 && isGrounded() && !onWall();
    }
}
