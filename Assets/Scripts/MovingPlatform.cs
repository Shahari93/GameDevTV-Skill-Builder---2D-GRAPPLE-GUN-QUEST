using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float limitX, limitNegX;
    private bool isMovingRight = true;


    private void Update()
    {
        PlatformMovement();
    }

    private void PlatformMovement()
    {
        if (isMovingRight)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x >= limitX)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x <= limitNegX)
            {
                isMovingRight = true;
            }
        }
    }
}