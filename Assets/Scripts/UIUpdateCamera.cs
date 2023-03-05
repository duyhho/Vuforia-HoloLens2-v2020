using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using System.String;
using System;
public class UIUpdateCamera : MonoBehaviour
{
    public GameObject cameraGO;
    public GameObject phantomTarget;
    public Text uiText;
    public Text uiTextVelocity = null;

    Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(previousPosition);
        // Debug.Log(cameraLocation);
        // Vector3 temp = new Vector3(0f, 0f, 0f);
        Vector3 cameraLocation = cameraGO.transform.position;
        Vector3 phantomLocation = phantomTarget.transform.position;

        cameraLocation = (phantomLocation - cameraLocation) * 100;

        Quaternion cameraRotation = cameraGO.transform.rotation;
        // Vector3 velocity = (cameraLocation-previousPosition)/Time.deltaTime;

        uiText.text = string.Format("Position: \nX: {0}cm Y: {1}cm Z: {2}cm\n", 
                                    cameraLocation.x.ToString("F2"), 
                                    cameraLocation.y.ToString("F2"), 
                                    cameraLocation.z.ToString("F2")     
                                    );
        uiText.text += string.Format("Rotation: \nX: {0} Y: {1} Z: {2}\n", 
                                    cameraRotation.x.ToString("F3"), 
                                    cameraRotation.y.ToString("F3"),
                                    cameraRotation.z.ToString("F3")

                                    );
        // Debug.Log(velocity);
        

    }
    void FixedUpdate() {
        Vector3 cameraLocation = cameraGO.transform.position;
        Vector3 velocity = (cameraLocation- previousPosition)/Time.deltaTime;
        float speed = (cameraLocation- previousPosition).magnitude/Time.deltaTime;

        previousPosition = cameraLocation;
        if (speed > 0f && uiTextVelocity != null ) {
            uiTextVelocity.text = string.Format("Velocity: {0}", velocity.ToString("F3"));
        }
    }
}
