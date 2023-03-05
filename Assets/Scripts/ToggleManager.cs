using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    public PhantomToggleOnOff basePhantom;
    public PhantomToggleOnOff combinedPhantom;
    Vector3 originalPhantomLocation;
    // Start is called before the first frame update
    void Start()
    {
        originalPhantomLocation = basePhantom.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            basePhantom.Toggle();
            combinedPhantom.Toggle();
        }
    }
    public void MoveX(float val) {
        Transform phantomTransform = basePhantom.gameObject.transform;
        Transform combinedPhantomTransform = combinedPhantom.gameObject.transform;

        float newX = phantomTransform.localPosition.x + val;
        phantomTransform.localPosition = new Vector3(newX, phantomTransform.localPosition.y, phantomTransform.localPosition.z);
        combinedPhantomTransform.localPosition = new Vector3(newX, combinedPhantomTransform.localPosition.y, combinedPhantomTransform.localPosition.z);

    }
    public void MoveY(float val) {
        Transform phantomTransform = basePhantom.gameObject.transform;
        Transform combinedPhantomTransform = combinedPhantom.gameObject.transform;

        float newY = phantomTransform.localPosition.y + val;
        phantomTransform.localPosition = new Vector3(phantomTransform.localPosition.x, newY, phantomTransform.localPosition.z);
        combinedPhantomTransform.localPosition = new Vector3(combinedPhantomTransform.localPosition.x, newY, combinedPhantomTransform.localPosition.z);
    }
    public void MoveZ(float val) {
        Transform phantomTransform = basePhantom.gameObject.transform;
        Transform combinedPhantomTransform = combinedPhantom.gameObject.transform;

        float newZ = phantomTransform.localPosition.z + val;
        phantomTransform.localPosition = new Vector3(phantomTransform.localPosition.x, phantomTransform.localPosition.y, newZ);
        combinedPhantomTransform.localPosition = new Vector3(combinedPhantomTransform.localPosition.x, combinedPhantomTransform.localPosition.y, newZ);
    }
}
