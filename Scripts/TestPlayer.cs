using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public Joystick upDownJoystick;
    public Joystick leftRightJoystick;
    public GameObject planet;
    public GameObject playerPlaceholder;
    public Animator animator;

    public float speed = 4f;
    public float jumpHeight = 5f;

    private float gravity = 100f;
    private bool onGround = false;

    private float distanceToGround;
    private Vector3 groundNormal;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    public void Jump()
    {
        if (onGround)
        {
            rb.AddForce(transform.up * 40000 * jumpHeight * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        //float x = upDownJoystick.Horizontal * Time.deltaTime * speed;
        float z = upDownJoystick.Vertical * Time.deltaTime * speed;
        animator.SetFloat("Move", Mathf.Abs(upDownJoystick.Vertical));

        transform.Translate(0, 0, z);

        //local rotation
        if (leftRightJoystick.Horizontal > 0 || Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 150 * Time.deltaTime, 0);
        } else if (leftRightJoystick.Horizontal < 0 || Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -150 * Time.deltaTime, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
            } else
            {
                onGround = false;
            }
        }

        //gravity and rotation
        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;
        if (!onGround)
        {
            rb.AddForce(gravDirection * -gravity);
            animator.Play("Jump");
        } 

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
        transform.rotation = toRotation;
    }

    //change planet
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform != planet.transform)
        {
            planet = collision.transform.gameObject;

            Vector3 gravDirection = (transform.position - planet.transform.position).normalized;
            Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
            transform.rotation = toRotation;

            rb.velocity = Vector3.zero;
            rb.AddForce(gravDirection * gravity);

            playerPlaceholder.GetComponent<TestPlaceholder>().NewPlanet(planet);
        }
    }
}
