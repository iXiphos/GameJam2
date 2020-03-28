using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;

    private float speed;
    public float maxSpeed;
    public float accelerationSpeed;
    public float decelerationSpeed;
    public float runSpeed;
    public float accelerationRun;

    private float curSpeed;

    public float rotationSpeed;

    public float jumpForce;

    private float xAxis;
    private float zAxis;

    private Vector3 moveDirection;

    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        curSpeed = maxSpeed;

        jump = true;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        zAxis = Input.GetAxisRaw("Vertical");
        xAxis = Input.GetAxisRaw("Horizontal");
        //if(Input.GetAxis("Vertical") != 0 && (speed < maxSpeed))
        //{
        //    speed += (Input.GetAxisRaw("Vertical") * accelerationSpeed) * Time.deltaTime;
        //}
        moveDirection = (xAxis * transform.right + zAxis * transform.forward).normalized;
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 yVelFix = new Vector3(moveDirection.x * curSpeed * Time.deltaTime, rb.velocity.y, moveDirection.z * curSpeed * Time.deltaTime);
        rb.velocity = yVelFix;
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            curSpeed = runSpeed;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            curSpeed = maxSpeed;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            jump = false;
            if(rb.velocity.z != 0)
            {
                rb.velocity += new Vector3(0, jumpForce, 4f);
            }
            else
            {
                rb.velocity += new Vector3(0, jumpForce, 0);
            }
        }
    }

    private void Slide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            jump = true;
        }
    }

}
