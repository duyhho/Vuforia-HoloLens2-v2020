using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using System.String;
using System;
public class UIUpdateTrocar : MonoBehaviour
{
    public GameObject trocarGO;
    public GameObject phantomTarget;

    public Text uiText;
    public Text uiTextVelocity = null ;

    Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("updateVelocity", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 trocarLocation = trocarGO.transform.position;
        Vector3 phantomLocation = phantomTarget.transform.position;
        trocarLocation = (phantomLocation - trocarLocation) * 100;

        Quaternion trocarRotation = trocarGO.transform.rotation;
        // float velocity = (trocarLocation-previousPosition).magnitude/Time.deltaTime;



        uiText.text = string.Format("Position: \nX: {0}cm Y: {1}cm Z: {2}cm\n", 
                                    trocarLocation.x.ToString("F2"), 
                                    trocarLocation.y.ToString("F2"), 
                                    trocarLocation.z.ToString("F2")
                                  
                                    );
        uiText.text += string.Format("Rotation: \nX: {0} Y: {1} Z: {2}\n", 
                                    trocarRotation.x.ToString("F3"), 
                                    trocarRotation.y.ToString("F3"), 
                                    trocarRotation.z.ToString("F3"));
        // previousPosition = trocarLocation;
        

    }
    void FixedUpdate() {
        Vector3 trocarLocation = trocarGO.transform.position;
        Vector3 velocity = (trocarLocation- previousPosition)/Time.deltaTime;
        float speed = (trocarLocation- previousPosition).magnitude/Time.deltaTime;

        previousPosition = trocarLocation;


        if (uiTextVelocity != null && speed > 0f) {
            uiTextVelocity.text = string.Format("Velocity: {0}", velocity.ToString("F3"));
        }
    }
    // void updateVelocity(){
    //     Vector3 trocarLocation = trocarGO.transform.position;
    //     Vector3 velocity = (trocarLocation- previousPosition)/Time.deltaTime;
    //     float speed = (trocarLocation- previousPosition).magnitude/Time.deltaTime;

    //     previousPosition = trocarLocation;
    //     Debug.Log("previous: " + previousPosition.ToString("F3") );
    //     Debug.Log("current: " + trocarLocation.ToString("F3") );


    //     if (speed > 0f) {
    //         uiTextVelocity.text = string.Format("Velocity: {0}", velocity.ToString("F3"));
    //     }
    // }
}
