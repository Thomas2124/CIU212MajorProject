using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float zMinus = -12f;
    public int deathCounter = 0;
    public bool changed = false;
    public bool trollChanged = false;
    public GameObject trollText;

    // Start is called before the first frame update
    void Start()
    {
        trollText = PauseMenu.Instance.trollMessage;
        trollText.SetActive(false);
        // Sets offset values.
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetRb = target.GetComponent<Rigidbody2D>();
        //offsetRight = new Vector3(target.position.x + 2f, target.position.y, zMinus);
        //offsetLeft = new Vector3(target.position.x - 2f, target.position.y, zMinus);
        //offsetUp = new Vector3(target.position.x, target.position.y - 2.5f, zMinus);
        //offsetDown = new Vector3(target.position.x, target.position.y + 2.5f, zMinus);
        //offsetMoveRight = new Vector3(target.position.x - 4.5f, target.position.y, zMinus);
        //offsetMoveLeft = new Vector3(target.position.x + 4.5f, target.position.y, zMinus);

        // Gets highest and longest point on the generated level.
        highestPoint = LevelGenerator.Instance.highestNum;
        longestPoint = LevelGenerator.Instance.longestNum;
    }

    // Update is called once per frame
    void Update()
    {
        deathCounter = PauseMenu.Instance.deathCount;

        if (deathCounter >= 10 && changed == false)
        {
            yMinus = 6.5f;
            xMinus = 12f;
            zMinus = -12f;
            changed = true;
        }

        if (deathCounter >= 40 && trollChanged == false)
        {
            trollText.SetActive(true);
            trollChanged = true;
        }

        // Sets offset and limits the position of the camera based on level size.
        offset = new Vector3(target.position.x, target.position.y, zMinus);
        offset.x = Mathf.Clamp(target.position.x, xMinus, longestPoint - xMinus);
        offset.y = Mathf.Clamp(target.position.y, yMinus, highestPoint - yMinus);

        desiredPosition = offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}

