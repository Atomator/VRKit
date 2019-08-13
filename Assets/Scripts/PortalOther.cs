/*
 * This Class handles flipping the global _StencilTest property when the device
 * moves throught the portal, from any direction
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalOther : MonoBehaviour
{

    public Transform device;
    public Camera camera;
    public Material[] materials;
    public GameObject VRText;
    public GameObject UpText;
    public GameObject Button;
    public GameObject indicator;
    public GameObject Tap;

    //bool for checking if the device is not in the same direction as it was
    bool wasInFront;
    //bool for knowing that on the next change of state, what to set the stencil test
    public bool inOtherWorld = false;

    //This bool is on while device colliding, done so we ensure the shaders are being updated before render frames
    //Avoids flickering
    bool isColliding;

    private bool setMat = false;
 
    private MeshRenderer meshRenderer;

    void Start()
    {
        if (device == null)
            device = GameObject.FindWithTag("Player").transform;
        if (camera == null)
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        //start outside other world
        SetMaterials(false);
        meshRenderer = GetComponent<MeshRenderer>();
    }
 
    void OnDestroy()
    {
        // ensure geometry renders in the editor
        SetMaterials(true);
    }
 
    void Update()
    {
        WhileCameraColliding();
        if (inOtherWorld) {
            VRText.SetActive(true);
            UpText.SetActive(true);
            Button.SetActive(true);
            Tap.SetActive(true);
        }
        else {
            VRText.SetActive(false);
            UpText.SetActive(false);
            Button.SetActive(false);
            Tap.SetActive(false);
        }
    }
 
    void SetMaterials(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;
		foreach (var mat in materials) {
			mat.SetInt("_StencilTest", (int)stencilTest);
		}
    }
 
    bool GetIsInFront()
    {
        Vector3 worldPos = device.position + device.forward * camera.nearClipPlane;
 
        Vector3 pos = transform.InverseTransformPoint(worldPos);
        return pos.z >= 0 ? true : false;
    }
 
    // This technique registeres if the device has hit the portal, flipping the bool
    void OnTriggerEnter(Collider other)
    {
        if (other.transform != device)
            return;
        //Important to do this for if the user re-enters the portal from the same side
        wasInFront = GetIsInFront();
        isColliding = true;
        // print("entering");
 
        if (inOtherWorld == false)
        {
            SetMaterials(true);
            // meshRenderer.enabled = false;
            setMat = true;
        }
        else
        {
            SetMaterials(false);
            // meshRenderer.enabled = true;
            setMat = false;
        } 
        // print(inOtherWorld);
        if (inOtherWorld == true) {
            SetMaterials(true);
        }     
        meshRenderer.enabled = false;
    }
 
    void OnTriggerExit(Collider other)
    {
        if (other.transform != device)
            return;
        isColliding = false;
        // print("exiting");
        if (inOtherWorld == false)
        {
            SetMaterials(false);
        }

        meshRenderer.enabled = true;
    }
 
    // If there has been a change in the relative position of the device to the portal, flip the
    // Stencil Test
    void WhileCameraColliding()
    {
        if (!isColliding)
            return;
        bool isInFront = GetIsInFront();
        if ((isInFront && !wasInFront) || (wasInFront && !isInFront))
        {
            inOtherWorld = !inOtherWorld;
        }
        wasInFront = isInFront;
    }
}
