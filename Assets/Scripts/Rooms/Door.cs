using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousroom;
    [SerializeField] private Transform nextroom;
    [SerializeField] private CameraControler cam;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextroom);
            else
                cam.MoveToNewRoom(previousroom);
        }
    }
}
