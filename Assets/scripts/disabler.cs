using UnityEngine;

public class disabler : MonoBehaviour
{
    [SerializeField] private Transform Room;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if ( transform.position.x < collision.transform.position.x )
            {
                Room.GetComponent<Room>().ActivateRoom(true);
            }
            else
            {
                Room.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }

    

}
