using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomToggleOnOff : MonoBehaviour
{
    MeshRenderer renderer;
    public bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        // renderer = GetComponentInChildren<MeshRenderer>();
        // if (renderer != null) {
        //     renderer.enabled = isActive;
        // }
        gameObject.SetActive(isActive);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Toggle() {
        isActive = !isActive;
        // renderer.enabled = isActive;
        gameObject.SetActive(isActive);
    }
    public void MoveX(float val) {
        float newX = transform.localPosition.x + val;
        transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
    }
    public void MoveY(float val) {
        float newY = transform.localPosition.y + val;
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
    public void MoveZ(float val) {
        float newZ = transform.localPosition.z + val;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newZ);
    }
}
