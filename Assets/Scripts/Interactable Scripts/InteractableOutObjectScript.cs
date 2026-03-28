using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableOutObjectScript : MonoBehaviour
{
   [SerializeField] private int itemCode;
   private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    public int AddItemToHand()
    {
        return itemCode;
    }
}
