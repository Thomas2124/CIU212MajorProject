using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D targetRb;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;
    public int highestPoint = 0;
    public int longestPoint = 0;
    public Vector3 offsetUp;
    public Vector3 offsetDown;
    public Vector3 offsetMoveLeft;
    public Vector3 offsetMoveRight;
    Vector3 desiredPosition;
    public float yMinus = 4f;
    public float xMinus = 6f;

    // Start is called before the first frame update
    void Start()
    {
        // Sets offset values.
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetRb = target.GetComponent<Rigidbody2D>();
        offsetRight = new Vector3(target.position.x + 2f, target.position.y, -10f);
        offsetLeft = new Vector3(target.position.x - 2f, target.position.y, -10f);
        offsetUp = new Vector3(target.position.x, target.position.y - 2.5f, -10f);
        offsetDown = new Vector3(target.position.x, target.position.y + 2.5f, -10f);
        offsetMoveRight = new Vector3(target.position.x - 4.5f, target.position.y, -10f);
        offsetMoveLeft = new Vector3(target.position.x + 4.5f, target.position.y, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        // Gets highest and longest point on the generated level.
        highestPoint = LevelGenerator.Instance.highestNum;
        longestPoint = LevelGenerator.Instance.longestNum;

        // Sets offset and limits the position of the camera based on level size.
        offset = new Vector3(target.position.x, target.position.y, -10f);
        offset.x = Mathf.Clamp(target.position.x, xMinus, longestPoint - xMinus);
        offset.y = Mathf.Clamp(target.position.y, yMinus, highestPoint - yMinus);

        desiredPosition = offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}

