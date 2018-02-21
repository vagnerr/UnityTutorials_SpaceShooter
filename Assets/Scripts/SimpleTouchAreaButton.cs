using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool touched;
    private int pointerID;
    private bool canFire;

    void Awake()
    {
        touched = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            pointerID = eventData.pointerId;
            canFire = true;

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            // Reset Everything
            touched = false;
            canFire = false;

        }
    }


    public bool CanFire()
    {
        
        return canFire;
    }

}
