using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Rigidbody rb;

    public float defaultForce;
    public float defaultSpeedH;
    public float defaultSpeedV;

    float force;
    float speedH;
    float speedV;

    private float yaw = -90.0f;
    private float pitch = 0.0f;

    bool paused = true;

    void Start()
    {
        force = defaultForce;
        speedH = defaultSpeedH;
        speedV = defaultSpeedV;
    }

    public void TimeScaleChange(Slider timeScaleSlider)
    {
        force = defaultForce / timeScaleSlider.value;
        speedH = defaultSpeedH / timeScaleSlider.value;
        speedV = defaultSpeedV / timeScaleSlider.value;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
                Vector3 forward = transform.forward;
                forward.y = 0f;

                rb.AddForce(forward * force);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(Vector3.left * force);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Vector3 back = -transform.forward;
                back.y = 0f;

                rb.AddForce(back * force);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeForce(Vector3.right * force);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * force);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(Vector3.down * force);
            }

            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
