using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;

    public float speed;
    public float rotationSpeed;

    private float xAxis;
    private float zAxis;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        zAxis = Input.GetAxisRaw("Vertical");
        xAxis = Input.GetAxisRaw("Horizontal");
        moveDirection = (xAxis * transform.right + zAxis * transform.forward).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed * Time.deltaTime;
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2f;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            speed /= 2f;
    }

    private void Jump()
    {

    }

}
