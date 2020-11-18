using UnityEngine;

public class TestCreature : MonoBehaviour
{
    public GameObject planet;

    public float speed = 4f;
    public float jumpHeight = 5f;

    private float gravity = 100f;
    private bool onGround = false;

    private float distanceToGround;
    private Vector3 groundNormal;

    private Rigidbody rb;

    private float x = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    float DistToPlayer(Vector3 position1, Vector3 position2)
    {
        return Mathf.Acos(Vector3.Dot(position1, position2));
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) {
            Vector3 playerPos = player.transform.position;
            Vector3 playerDir = (playerPos - transform.position).normalized;

            //movement
            if (DistToPlayer(playerPos.normalized, transform.position.normalized) > 0.25f)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }

            //rotation
            Vector3 forward = Vector3.ProjectOnPlane(playerDir, transform.up);
            Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);
            transform.rotation = Quaternion.LookRotation(forward);

            Debug.Log("Distance to player = " + DistToPlayer(playerPos.normalized, transform.position.normalized));
        }

        //ground control
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            if (distanceToGround <= 0.2f)
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }
        }

        //gravity and rotation
        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;
        if (!onGround)
        {
            rb.AddForce(gravDirection * -gravity);
        }

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
        transform.rotation = toRotation;
    }

    private void FixedUpdate()
    {/*
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            return;
        }
        Vector3 moveDir = new Vector3(1, 0, 1);
        rb.MovePosition(rb.position + transform.TransformDirection(player.transform.position.normalized) * speed * Time.deltaTime);*/
    }

    private void Jump()
    {
        if (onGround)
        {
            rb.AddForce(transform.up * 40000 * jumpHeight * Time.deltaTime);
        }
    }

}
