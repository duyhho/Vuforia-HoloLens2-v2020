using UnityEngine;
using Vuforia;
using Image = Vuforia.Image;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
// using System.Linq;
using System.Collections;
public class CameraImageAccess : MonoBehaviour
{
    const PixelFormat PIXEL_FORMAT = PixelFormat.RGB888  ;
    // const PixelFormat PIXEL_FORMAT = PixelFormat.UNKNOWN_FORMAT  ;

    const TextureFormat TEXTURE_FORMAT = TextureFormat.RGB24;
    public Text metricText;

    public RawImage RawImage;
    private IEnumerator coroutine;

    Texture2D mTexture;
    bool mFormatRegistered;

    void Start()
    {
        // Register Vuforia Engine life-cycle callbacks:
        VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
        VuforiaApplication.Instance.OnVuforiaStopped += OnVuforiaStopped;
        if (VuforiaBehaviour.Instance != null) {
            VuforiaBehaviour.Instance.World.OnStateUpdated += OnVuforiaUpdated;
            StartCoroutine("CaptureEveryXSeconds", 3);
        }
        else {
            Debug.Log("Vuforia is null");
        }
            
    }

    void OnDestroy()
    {
        // Unregister Vuforia Engine life-cycle callbacks:
        if (VuforiaBehaviour.Instance != null)
            VuforiaBehaviour.Instance.World.OnStateUpdated -= OnVuforiaUpdated;

        VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
        VuforiaApplication.Instance.OnVuforiaStopped -= OnVuforiaStopped;

        if (VuforiaApplication.Instance.IsRunning)
        {
            // If Vuforia Engine is still running, unregister the camera pixel format to avoid unnecessary overhead
            // Formats can only be registered and unregistered while Vuforia Engine is running
            UnregisterFormat();
        }

        if (mTexture != null)
            Destroy(mTexture);
    }
    private void OnPaused(bool paused)
    {
        if (!paused) // Resumed
        {
            // Set again autofocus mode when app is resumed
            VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(
                FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }

    /// 
    /// Called each time the Vuforia Engine is started
    /// 
    void OnVuforiaStarted()
    {
        mTexture = new Texture2D(0, 0, TEXTURE_FORMAT, false);
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    VuforiaBehaviour.Instance.CameraDevice.SetCameraMode(Vuforia.CameraMode.MODE_DEFAULT);
        // A format cannot be registered if Vuforia Engine is not running
        RegisterFormat();
    }

    /// 
    /// Called each time the Vuforia Engine is stopped
    /// 
    void OnVuforiaStopped()
    {
        // A format cannot be unregistered after OnVuforiaStopped
        UnregisterFormat();
        if (mTexture != null)
            Destroy(mTexture);
    }

    /// 
    /// Called each time the Vuforia Engine state is updated
    /// 
    void OnVuforiaUpdated()
    {
        // if (Input.GetKeyDown("f"))
        // {
        //     StartCoroutine(Fade());
        // }
        if (mFormatRegistered == true) {
            var image = VuforiaBehaviour.Instance.CameraDevice.GetCameraImage(PIXEL_FORMAT);

        // There can be a delay of several frames until the camera image becomes available
        if (Image.IsNullOrEmpty(image)) {
            Debug.Log("empty");
            return;

        }

        Debug.Log("\nImage Format: " + image.PixelFormat +
                  "\nImage Size: " + image.Width + " x " + image.Height +
                  "\nBuffer Size: " + image.BufferWidth + " x " + image.BufferHeight +
                  "\nImage Stride: " + image.Stride + "\n");

        // Override the current texture by copying into it the camera image flipped on the Y axis
        // The texture is resized to match the camera image size
        image.CopyToTexture(mTexture, true);

        RawImage.texture = mTexture;
        RawImage.material.mainTexture = mTexture;
        }
        
    }

    /// 
    /// Register the camera pixel format
    /// 
    void RegisterFormat()
    {
        // Vuforia Engine has started, now register camera image format
        var success = VuforiaBehaviour.Instance.CameraDevice.SetFrameFormat(PIXEL_FORMAT, true);
        if (success)
        {
            Debug.Log("Successfully registered pixel format " + PIXEL_FORMAT);
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register pixel format " + PIXEL_FORMAT +
                           "\n the format may be unsupported by your device;" +
                           "\n consider using a different pixel format.");
            mFormatRegistered = false;
        }
    }

    /// 
    /// Unregister the camera pixel format
    /// 
    void UnregisterFormat()
    {
        Debug.Log("Unregistering camera pixel format " + PIXEL_FORMAT);
        VuforiaBehaviour.Instance.CameraDevice.SetFrameFormat(PIXEL_FORMAT, false);
        mFormatRegistered = false;
    }

    IEnumerator Fade()
    {
        var image = VuforiaBehaviour.Instance.CameraDevice.GetCameraImage(PIXEL_FORMAT);
         // There can be a delay of several frames until the camera image becomes available
        if (Image.IsNullOrEmpty(image)) {
            Debug.Log("empty");
            // return;

        }
        else {
            Debug.Log("not empty");
        }
         yield return null;
    }
    private IEnumerator CaptureEveryXSeconds(int numSeconds) {
        while (true) {
             yield return new WaitForSeconds(numSeconds);
             coroutine = Capture("http://127.0.0.1:5000", metricText);
             StartCoroutine(coroutine);
         }
    }
    
    private IEnumerator Capture(string apiURL, Text displayText)
    {
        byte[] bytes = mTexture.EncodeToJPG();
        WWWForm form = new WWWForm();
        // form.AddField("frameCount", Time.frameCount.ToString());
        string imageFileName = "unity_upload_" + DateTime.UtcNow.Millisecond.ToString() + ".jpg";
        Debug.Log(imageFileName);
        form.AddBinaryData("file1", bytes, imageFileName, "image/jpg");
        // Upload to a cgi script
        using (UnityWebRequest request = UnityWebRequest.Post(apiURL + "/", form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                // StatusBlock.text = $"Error: {count}";
                Debug.Log(request.error);
                // debugText.text = "Upload Error";

            }
            else
            {
                // StatusBlock.text = $"Success: {count}";
                Debug.Log("Finished Uploading Screenshot");
                string jsonResponse = request.downloadHandler.text;

            }
        }
    }
}