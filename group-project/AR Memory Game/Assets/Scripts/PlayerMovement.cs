using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.ARFoundation;

public class PlayerMovement : NetworkBehaviour
{
    private Touch touch;
    private float speedModifier;
    Vector2 firstTouch;
    Vector2 secondTouch;
    float distanceCurrent;
    float distancePrevious;
    bool objectMoving = false;
    bool firstPinch = true;
    bool secondPinch;

    void Awake()
    {
        speedModifier = 0.001f; // increase this value to make object move faster
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return; // only object owner(player) moves the objects
        // Handle screen touches.
        if (Input.touchCount > 0 && !objectMoving){

            touch = Input.GetTouch(0);
            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                // changes position of object in each frame, assigns new vector 3 values
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier,transform.position.y+ touch.deltaPosition.y * speedModifier,transform.position.z );
                objectMoving = true;
            }

        }
        // pinch to scale
        if(Input.touchCount>1 && objectMoving){
            // position of both fingers
            firstTouch = Input.GetTouch(0).position;
            secondTouch = Input.GetTouch(1).position;

            distanceCurrent = secondTouch.magnitude - firstTouch.magnitude;
            if(firstPinch){
                distancePrevious = distanceCurrent;
                firstPinch = false;
            }
            if(distanceCurrent != distancePrevious){
                Vector3 scaleValue = transform.localScale * (distanceCurrent/distancePrevious);
                transform.localScale = scaleValue;
                distancePrevious = distanceCurrent;
            }
        }
        else{
            firstPinch = true;
            objectMoving = false;
        }
        
    }
}
