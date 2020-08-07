using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 20;

    public float speedH = 5.0f;
    public float speedV = 5.0f;

    private float yaw = -90.0f;
    private float pitch = 0.0f;

    bool paused = true;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(Vector3.forward * forwardForce);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(Vector3.left * forwardForce);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(Vector3.back * forwardForce);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeForce(Vector3.right * forwardForce);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddRelativeForce(Vector3.up * forwardForce);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddRelativeForce(Vector3.down * forwardForce);
            }

            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
