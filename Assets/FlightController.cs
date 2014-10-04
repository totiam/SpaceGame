using UnityEngine;
using System.Collections;

public class FlightController : MonoBehaviour
{
    public float turningAcceleration = 50f;
    public Vector2 deadZone = new Vector2(80f, 80f);
    public float accecelrationForce = 50f;
    public float decelerationforce = 30f;
    public float bankingForce = 45f;

    public Transform displayObject;
    public Transform followObject;

    Rigidbody rb;


    private

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        if (rb == null)
            rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.maxAngularVelocity = 1;
    }


    void Update()
    {
        Vector2 animationRotation = UpdateMouseAxis();
        updateAxis();

        MoveFollowObject(animationRotation.normalized);

        Debug.Log(rb.velocity);
    }

    private void MoveFollowObject(Vector2 animationRotation)
    {
        followObject.localPosition = new Vector3(4* animationRotation.x, 4 * animationRotation.y, followObject.transform.localPosition.z);
        Quaternion wantedRotation = Quaternion.LookRotation(followObject.position - displayObject.position, followObject.up);
        displayObject.rotation = Quaternion.Slerp(displayObject.rotation, wantedRotation, Time.deltaTime * 20);
    }

    private void updateAxis()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                rb.AddRelativeForce(Vector3.forward * Time.deltaTime * accecelrationForce * Input.GetAxis("Vertical"));
            }
            else
            {
                rb.AddRelativeForce(Vector3.forward * Time.deltaTime * decelerationforce * Input.GetAxis("Vertical"));
            }
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.AddRelativeTorque(Vector3.back * Time.deltaTime * bankingForce * Input.GetAxis("Horizontal"));
        }
    }

    private Vector2 UpdateMouseAxis()
    {
        Vector2 animationRotation = Vector2.zero;
        if (Mathf.Abs(Input.mousePosition.y - Screen.height / 2) >= 40)
        {
            float normalDelta = (Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2);

            if (normalDelta > 1)
            {
                normalDelta = 1;
            }
            else if (normalDelta < -1)
            {
                normalDelta = -1;
            }
            animationRotation.y = normalDelta;
            rb.AddRelativeTorque(Vector3.left * normalDelta * Time.deltaTime * turningAcceleration, ForceMode.Acceleration);
        }

        if (Mathf.Abs(Input.mousePosition.x - Screen.width / 2) >= 40)
        {
            float normalDelta = (Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2);
            if (normalDelta > 1)
            {
                normalDelta = 1;
            }
            else if (normalDelta < -1)
            {
                normalDelta = -1;
            }
            animationRotation.x = normalDelta;
            rb.AddRelativeTorque(Vector3.up * normalDelta * Time.deltaTime * turningAcceleration, ForceMode.Acceleration);
        }
        return animationRotation;
    }
}
