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

    private float curSpeed;
    private float tempSpeed;

    public float jumpForce;

    private float xAxis;
    private float zAxis;

    private Vector3 moveDirection;

    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        curSpeed = 0;

        tempSpeed = maxSpeed;

        jump = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        zAxis = Input.GetAxisRaw("Vertical");
        xAxis = Input.GetAxisRaw("Horizontal");

        Debug.Log(curSpeed);
        Debug.Log(zAxis);

        if (xAxis != 0 && (curSpeed <= maxSpeed))
        {
            curSpeed += accelerationSpeed;
        }
        else if (zAxis != 0 && (curSpeed <= maxSpeed))
        {
            curSpeed += accelerationSpeed;
        }
        Sprint();

        if (xAxis == 0 && zAxis == 0 && (curSpeed > 0 ))
        {
            curSpeed -= decelerationSpeed;
        }

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
            maxSpeed = runSpeed;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            maxSpeed = tempSpeed;
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
