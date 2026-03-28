using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private InputActionAsset InputActions;
    private InputAction movement;
    private InputAction interact;
    private bool isInteract=false;
    private InputAction yeet;
    private Vector2 movementVector;
    private float timer;
    private bool itemInHand=false;
    private Animator anim;

    [Header("-----Movement Tweaks-----")]
    [SerializeField] private float movementSpeed = 5f;
    [Header("-----Interactable Tweaks")]
    [SerializeField] private Transform itemPosition;
    [SerializeField] private GameObject itemBlueprint;
    [System.Serializable] public struct ItemStruct
    {
        public int itemCode;
        public Sprite itemSprite;
    }
    [SerializeField] private ItemStruct[] items;
    [SerializeField] private float itemThrowSpeed;
    [SerializeField] private float searchTime=0.2f;
    private int itemInHandCode = 0;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();    
    }
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        interact = InputActions.FindAction("Interact");
        yeet = InputActions.FindAction("Throw");
        movement = InputActions.FindAction("Movement");
        anim = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        movementVector = movement.ReadValue<Vector2>();
        
        if (movementVector.magnitude > 0) anim.SetBool("isMoving",true);
        else anim.SetBool("isMoving",false);

        PlayerFlip(movementVector.x);
        if (interact.WasPressedThisFrame())
        {
            StartCoroutine(Interact());
        }
        if (yeet.WasPressedThisFrame())
        {
            ThrowItem();
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (isInteract&&collision.CompareTag("Storage"))
        {
            AddItem(collision.gameObject.GetComponent<InteractableOutObjectScript>().AddItemToHand());
            isInteract = false;
        }
        if (isInteract && collision.CompareTag("Cook"))
        {
            RemoveItem(collision.gameObject);
            isInteract = false;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementVector.x,movementVector.y).normalized*movementSpeed;
    }[SerializeField]
    private IEnumerator Interact()
    {
        isInteract=true;
        //searching animation start
        yield return new WaitForSeconds(searchTime);
        isInteract=false;
        //seraching animation ends
    }
    private void AddItem(int itemCode)
    {
        if(!itemInHand){
            itemInHand=true;

            GameObject inHand = Instantiate(itemBlueprint,gameObject.transform.Find("Hand"));
            inHand.GetComponent<SpriteRenderer>().sprite = items[itemCode-1].itemSprite;
            inHand.GetComponent<ItemScript>().itemCode = items[itemCode-1].itemCode;
            inHand.name = "Item";
            inHand.transform.SetParent(gameObject.transform);

            //change charecter sprite

            itemInHandCode = itemCode;
            Debug.Log("Item number "+itemInHandCode+" in hand.");  

            return;
        }
        Debug.Log("Item Already in Hand");
        //add warning
    }
    private void ThrowItem()
    {
        if (gameObject.transform.Find("Item")){

            GameObject Item = gameObject.transform.Find("Item").gameObject;
            Item.transform.parent = null;
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)-Item.transform.position;
            Item.GetComponent<ItemScript>().Throw(direction,itemThrowSpeed);

            EmptyHand();

            Debug.Log("Thrown");
        }
        else
        {
            return;
        }
    }
    private void RemoveItem(GameObject appliance)
    {
        bool isRemove = appliance.GetComponent<InteractableInObjectScript>().TakeItem(itemInHandCode);
        if (isRemove)
        {
            EmptyHand();
            Destroy(gameObject.transform.Find("Item").gameObject);
        }
    }
    private void EmptyHand()
    {
        itemInHand = false;
        itemInHandCode = 0;
        //charecter sprite change
    }
    private void PlayerFlip(float x)
    {
        if (x<0f)
        {
            gameObject.transform.rotation= Quaternion.Euler(0,180,0);
        }
        else if(x>0f)
        {
            gameObject.transform.rotation=Quaternion.Euler(0,0,0);
        }
    }
}
