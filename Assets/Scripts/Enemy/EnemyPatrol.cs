using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;

    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idel Behaviour")]
    [SerializeField] private float idleDuration;
    private float idelTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moveing", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
            {
                //Change Direction
                DirectionChange();
            }
        }
        else 
        {
            if(enemy.position.x <= rightEdge.position.x)
            MoveInDirection(1);
            else
            {
                //Change Direction
                DirectionChange();
            }
        }

    }

    private void DirectionChange()
    {
        anim.SetBool("moveing", false);
        idelTimer += Time.deltaTime;

        if(idelTimer > idleDuration)
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idelTimer = 0;
        anim.SetBool("moveing", true);

        enemy.localScale = new Vector3(Mathf.Abs( initScale.x) * _direction,
            initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z); 
    }
}
