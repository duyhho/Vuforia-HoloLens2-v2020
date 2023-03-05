using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HintButton : MonoBehaviour
{
    bool isActive = false;
    public GameObject toggleObject;
    public TextMeshPro currentText;
    public string activeText;
    public string inactiveText;
    // Start is called before the first frame update
    void Start()
    {
        // currentText = transform.GetComponentInChildren<TextMeshProUGUI>();
        // currentText.SetText("Show Hints");
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == true) {
            // currentText.text = "Hello";
            currentText.SetText(activeText);  
            toggleObject.SetActive(true);
        }
        else {
            // currentText.text = "No Hello";

            currentText.SetText(inactiveText);  
            toggleObject.SetActive(false);

        }
    }
    public void Toggle() {
        
        isActive = !isActive;

       
    }
}
