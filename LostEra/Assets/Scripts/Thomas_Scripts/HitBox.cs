using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float damage = 50f;
    public bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.01f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit == false)
        {
            if (collision.gameObject.tag == "enemy")
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            isHit = true;
        }
    }
}
