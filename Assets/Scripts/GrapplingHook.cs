// GameDev.tv Challenge Club. Got questions or want to share your nifty solution?
// Head over to - http://community.gamedev.tv

using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float distBetweenPlayerAndPoint = 0.01f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LineRenderer line;
    [SerializeField] private float grappleSpeed = 3f;
    [SerializeField] private GameObject playerHand;

    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;

    private void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }

    private void Update()
    {
        MouseDown();
        MouseHeldDown();
        MouseRelease();
    }

    private void MouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            hit = Physics2D.Raycast(playerHand.transform.position, targetPos - playerHand.transform.position, distance, mask);

            if (hit.collider != null && hit.rigidbody != null)
            {
                joint.enabled = true;
                //Challenge 1:
                joint.connectedBody = hit.rigidbody;

                //Challenge 3:
                joint.connectedAnchor = hit.transform.InverseTransformPoint(hit.point);


                joint.distance = Vector2.Distance(playerHand.transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, playerHand.transform.position);
                line.SetPosition(1, hit.point);
            }
        }
    }

    private void MouseHeldDown()
    {
        if (Input.GetMouseButton(0))
        {
            PullPlayer();
            line.SetPosition(0, playerHand.transform.position);
            if (joint.distance < distBetweenPlayerAndPoint)
            {
                ReleasePlayer(false);
            }
        }
    }

    private void MouseRelease()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ReleasePlayer(false);
        }
    }

    private void PullPlayer()
    {
        joint.distance -= Time.deltaTime * grappleSpeed;
    }

    private void ReleasePlayer(bool enabled)
    {
        joint.enabled = enabled;
        line.enabled = enabled;
    }
}