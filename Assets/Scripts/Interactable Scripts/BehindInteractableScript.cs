using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindInteractableScript : MonoBehaviour
{
    [SerializeField]private BoxCollider2D col;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sortingOrder = 3;
            spriteRenderer.color = new Color(1f,1f,1f,0.8f);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.color = new Color(1f,1f,1f,1f);
        }
    }
}
