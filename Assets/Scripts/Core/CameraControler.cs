using UnityEngine;

public class CameraControler : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float curruntPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform Player;
    [SerializeField] private float ahedDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(curruntPosX, transform.position.y, transform.position.z), ref velocity, speed );

        //Follow player
        transform.position = new Vector3(Player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (ahedDistance * Player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        curruntPosX = _newRoom.position.x;
    }
}
