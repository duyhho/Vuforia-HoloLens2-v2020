using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;  
using Microsoft.MixedReality.Toolkit.Input;  
using Microsoft.MixedReality.Toolkit.Utilities;  
public class HandTrackingHololens2 : MonoBehaviour
{
    public GameObject trocar;
    public GameObject trocarMarker;

    GameObject palmObject;
    MixedRealityPose pose;
    Vector3 originalTrocarLocation;
    Quaternion originalTrocarRotation;
    Vector3 originalMarkerLocation;
    Quaternion originalMarkerRotation;
    public bool HandTrackingEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        palmObject = Instantiate(trocar, this.transform);
        Transform trocarChild = palmObject.transform.GetChild(0);
        originalTrocarLocation = trocarChild.localPosition;
        originalTrocarRotation = trocarChild.localRotation;

        Transform markerChild = trocarMarker.transform.GetChild(0);
        originalMarkerLocation = markerChild.localPosition;
        originalMarkerRotation = markerChild.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (HandTrackingEnabled) {
            trocarMarker.SetActive(false);
            palmObject.GetComponentInChildren<Renderer>().enabled = false;
            Vector3 palmLocation = new Vector3(0f, 0f, 0f);
            Quaternion palmRotation = new Quaternion(0f, 0f, 0f, 0f);

            Quaternion middleMiddleRotation = new Quaternion(0f, 0f, 0f, 0f);
            Vector3 middleMiddlePosition = new Vector3(0f, 0f, 0f);;

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleMiddleJoint, Handedness.Right, out pose))
            {
                palmObject.GetComponentInChildren<Renderer>().enabled = true;

                middleMiddlePosition = pose.Position;
                middleMiddleRotation = pose.Rotation;
                palmObject.transform.position = middleMiddlePosition;
                palmObject.transform.rotation = middleMiddleRotation;
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleKnuckle, Handedness.Right, out pose))
            {
                palmObject.GetComponentInChildren<Renderer>().enabled = true;

                middleMiddlePosition = pose.Position;
                middleMiddleRotation = pose.Rotation;
                palmObject.transform.position = middleMiddlePosition;
                palmObject.transform.rotation = middleMiddleRotation;
            }
            

        }
        else {
            trocarMarker.SetActive(true);
            palmObject.GetComponentInChildren<Renderer>().enabled = false;
        }
       

        
    }
    public void OnSourceDetected(SourceStateEventData eventData)
    {
        var hand = eventData.InputSource as IMixedRealityHand;
        Vector3 handDetectedPosition;
        if (hand.TryGetJoint(TrackedHandJoint.IndexTip, out MixedRealityPose jointPose))
        {
            Debug.Log("Index Detected");
        }
    }
    public void RecallTrocar() {
        Debug.Log("recalling trocar");
        if (HandTrackingEnabled) {
            Transform trocarChild = palmObject.transform.GetChild(0);
            trocarChild.localPosition = originalTrocarLocation;
            trocarChild.localRotation = originalTrocarRotation;
        }
        else {
            Transform markerChild = trocarMarker.transform.GetChild(0);
            markerChild.localPosition = originalMarkerLocation;
            markerChild.localRotation = originalMarkerRotation;
        }

    }
    public void ToggleHandTracking() {
        HandTrackingEnabled = !HandTrackingEnabled;
    }
    public void MoveX(float val) {
        Transform markerChild = trocarMarker.transform.GetChild(0);
        float newX = markerChild.localPosition.x + val;
        markerChild.localPosition = new Vector3(newX, markerChild.localPosition.y, markerChild.localPosition.z);
    }
    public void MoveY(float val) {
        Transform markerChild = trocarMarker.transform.GetChild(0);
        float newY = markerChild.localPosition.y + val;
        markerChild.localPosition = new Vector3(markerChild.localPosition.x, newY, markerChild.localPosition.z);
    }
    public void MoveZ(float val) {
        Transform markerChild = trocarMarker.transform.GetChild(0);
        float newZ = markerChild.localPosition.z + val;
        markerChild.localPosition = new Vector3(markerChild.localPosition.x, markerChild.localPosition.y, newZ);
    }
}
