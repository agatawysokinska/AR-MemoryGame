using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaneChoice : MonoBehaviour
{
    [SerializeField] private GameObject gamePlane;

    private GameObject spawnedGamePlane;
    private Vector2 touchPosition;
    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _planeManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARPlane arPlaneHit;
    public int inputType = 1;
    private int gamePlaneStatus = 1;
    private Pose dragStartPoint;
    private Pose gamePlaneCenterPoint;
    private bool pinchHold = false;
    private Vector2 pinchDistPrev;
    private bool clearInput = false;


    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _planeManager = GetComponent<ARPlaneManager>();
    }

    void setAllPlanesActive(bool status)
    {
        _planeManager.enabled = status;
            ARPlane[] arPlanes = FindObjectsOfType<ARPlane>();
            foreach (ARPlane arPlane in arPlanes)
            {
                arPlane.gameObject.SetActive(status);
            }
    }

    void Update()
    {
        if(Input.touchCount == 0 && clearInput)
        {
            setAllPlanesActive(false);
            pinchHold = false;
            clearInput = false;
        }
        if(Input.touchCount == 1 && !clearInput) {
            setAllPlanesActive(true);
            clearInput = true;
        }
        if(Input.touchCount == 1)
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

            if (_arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                gamePlaneCenterPoint = hitPose;

                if (spawnedGamePlane == null)
                    spawnedGamePlane = Instantiate(gamePlane, hitPose.position, hitPose.rotation);
                else
                    spawnedGamePlane.transform.position = hitPose.position;
                    spawnedGamePlane.transform.rotation = hitPose.rotation;
            }
            clearInput = true;
        }
        if(Input.touchCount == 2)
        {
            Vector2 touchPos1 = Input.GetTouch(0).position;
            Vector2 touchPos2 = Input.GetTouch(1).position;

            Vector2 pinchDist = new Vector2(
                Mathf.Abs(touchPos1.x - touchPos2.x),
                Mathf.Abs(touchPos1.y - touchPos2.y)
                );

            if(pinchHold)
            {
                float pinchDeltaX = pinchDist.x / pinchDistPrev.x;
                float pinchDeltaY = pinchDist.y / pinchDistPrev.y;
                if(pinchDeltaX != 1 || pinchDeltaY != 1)
                {
                    Vector3 currentScale = spawnedGamePlane.transform.localScale;
                    Vector3 newScale = new Vector3(currentScale.x * pinchDeltaX, currentScale.y, currentScale.z * pinchDeltaY);
                    spawnedGamePlane.transform.localScale = newScale;
                    pinchDistPrev = pinchDist;
                }
                
            }
            else {
                pinchDistPrev = pinchDist;
                pinchHold = true;
            }
            clearInput = true;
        }
    }
}

