using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;//luu og parent
        transform.SetParent(transform.root);// ben ngoai canvas
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;// semi trans trong luc drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;//fl mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Enables raycasts  
        canvasGroup.alpha = 1f; // No longer transparent  

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); // Slot where item dropped
        if(dropSlot == null)
{
            GameObject item = eventData.pointerEnter;
            if (item != null)
            {
                dropSlot = item.GetComponentInParent<Slot>();
            }
        }
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            // Is a slot under drop point  
            if (dropSlot.currentItem != null)
            {
                // Slot has an item - swap items  
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;  
            }
            else
            {
                originalSlot.currentItem = null;
            }

            // Move item into drop slot  
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            // No slot under drop point  
            transform.SetParent(originalParent);
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;//Center
    }

  

  
}
