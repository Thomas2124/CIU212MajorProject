using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;

    // Start is called before the first frame update
    void Start()
    {
        offsetRight = new Vector3(target.position.x + 3f, target.position.y, offset.z);
        offsetLeft = new Vector3(target.position.x - 3f, target.position.y, offset.z);
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            offset = offsetLeft;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            offset = offsetRight;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
