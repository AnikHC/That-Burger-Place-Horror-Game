using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public int itemCode;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Throw(Vector2 direction, float itemThrowSpeed)
    {
        rb.velocity = direction.normalized *itemThrowSpeed;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colided");
    }
}
