using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInObjectScript : MonoBehaviour
{
    [SerializeField] private int itemCodeRequired;
    private bool isCooking=false;

    public bool TakeItem(int itemCode)
    {
        if (itemCode == itemCodeRequired)
        {
            StartCoroutine(StartWorking());
            return true;
        }
        else if (isCooking)
        {
            //put warning message
            Debug.Log("An item is already cooking.");
            return false;
        }
        else if(itemCode==0)
        {
            return false;
        }
        //put warning message
        Debug.Log("Item doesn't go here.");
        return false;
    }
    private IEnumerator StartWorking()
    {
        Debug.Log("Cooking.");
        yield return null;
    }
}
