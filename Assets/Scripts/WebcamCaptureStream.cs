using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
public class WebcamCaptureStream : MonoBehaviour
{
    WebCamTexture webcamTexture;
    public RawImage avatar;
    public RawImage resultImage; //
    public int requestedWidth = 640;
    public int requestedHeight = 480;
    // Start is called before the first frame update
    void Start()
    {
         WebCamDevice[] devices = WebCamTexture.devices;
         string webcamName = "";
        for (var i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
            if (!devices[i].name.Contains("OBS")) {
                webcamName = devices[i].name;
            }
        }
        if (devices.Length > 0)
        {
            Debug.Log("Selected: " + webcamName);

            webcamTexture = new WebCamTexture(webcamName, requestedWidth, requestedHeight);
            avatar.texture = webcamTexture;
            webcamTexture.Play();

        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
