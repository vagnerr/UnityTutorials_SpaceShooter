using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    public float smoothing;

    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;


    void Awake()
    {
        direction = Vector2.zero;
        touched = false;
        
           
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!touched){
            touched = true;
            pointerID = eventData.pointerId;
            // Set out start point
            origin = eventData.position;
            
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ( eventData.pointerId == pointerID) { 
            // Compare diff between start and current
            Vector2 currentPosition = eventData.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            // Reset Everything
            direction = Vector2.zero;
            touched = false;
        }
    }


    public Vector2 GetDirction()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction,smoothing);
        return smoothDirection;
    }

}
