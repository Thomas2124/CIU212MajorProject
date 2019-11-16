using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float damage = 50f;
    public bool isHit = false;
    // Start is called before the first frame update
    void Awake()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.01f);
    }
}
