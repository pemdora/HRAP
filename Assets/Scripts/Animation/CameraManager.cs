using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRAP;

public class CameraManager : MonoBehaviour
{
    public Camera camera_PE_1; // Plan d'ensemble
    public Camera camera_PA_1; // Plan Americain avec 1 seul personnage
    public Camera camera_PA_2; // Plan Americain avec les 2 personnages
    public Camera camera_PR_1; // Plan rapproché 1
    public Camera camera_PR_2; // Plan rapproché 2
    public Camera camera_PR_3; // Plan rapproché 3
    public Camera camera_PR_4; // Plan rapproché 4
    public Camera camera_PR_5; // Plan rapproché 5
    public Camera camera_GP_1; // Gros Plan


    public static CameraManager cameraManagerinstance;
    // TEST
    private bool activateInterface = true;

    //SINGLETON
    void Awake()
    {
        if (cameraManagerinstance != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        else
        {
            cameraManagerinstance = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        camera_PE_1.enabled = true;
        camera_PA_1.enabled = false;
        camera_PA_2.enabled = false;
        camera_PE_1.enabled = false;
        camera_PR_1.enabled = false;
        camera_PR_2.enabled = false;
        camera_PR_3.enabled = false;
        camera_PR_4.enabled = false;
        camera_PR_5.enabled = false;
        IHMInterview.MaskAllNguiComponents(true);
    }

    // Update is called once per frame
    void Update()
    {
        // TEST
        if (Input.GetKeyDown("space"))
        {
            activateInterface = !activateInterface;
            IHMInterview.MaskAllNguiComponents(activateInterface);
        }
    }

    public void Display(M_Camera camera,bool displayIHM) // Display current camera
    {
        switch (camera)
        {
            case M_Camera.GP_1:
                camera_GP_1.enabled = true;
                camera_PA_1.enabled = false;
                camera_PA_2.enabled = false;
                camera_PE_1.enabled = false;
                camera_PR_1.enabled = false;
                camera_PR_2.enabled = false;
                camera_PR_3.enabled = false;
                camera_PR_4.enabled = false;
                camera_PR_5.enabled = false;
                break;
            case M_Camera.PA_1:
                camera_GP_1.enabled = false;
                camera_PA_1.enabled = true;
                camera_PA_2.enabled = false;
                camera_PE_1.enabled = false;
                camera_PR_1.enabled = false;
                camera_PR_2.enabled = false;
                camera_PR_3.enabled = false;
                camera_PR_4.enabled = false;
                camera_PR_5.enabled = false;
                break;
            case M_Camera.PA_2:
                camera_GP_1.enabled = false;
                camera_PA_1.enabled = false;
                camera_PA_2.enabled = true;
                camera_PE_1.enabled = false;
                camera_PR_1.enabled = false;
                camera_PR_2.enabled = false;
                camera_PR_3.enabled = false;
                camera_PR_4.enabled = false;
                camera_PR_5.enabled = false;
                break;
            case M_Camera.PE_1:
                camera_GP_1.enabled = false;
                camera_PA_1.enabled = false;
                camera_PA_2.enabled = false;
                camera_PE_1.enabled = true;
                camera_PR_1.enabled = false;
                camera_PR_2.enabled = false;
                camera_PR_3.enabled = false;
                camera_PR_4.enabled = false;
                camera_PR_5.enabled = false;
                break;
            case M_Camera.PR_1:
                camera_GP_1.enabled = false;
                camera_PA_1.enabled = false;
                camera_PA_2.enabled = false;
                camera_PE_1.enabled = false;
                camera_PR_1.enabled = true;
                camera_PR_2.enabled = false;
                camera_PR_3.enabled = false;
                camera_PR_4.enabled = false;
                camera_PR_5.enabled = false;
                break;
            case M_Camera.PR_2:
                camera_GP_1.enabled = false;
                camera_PA_1.enabled = false;
                camera_PA_2.enabled = false;
                camera_PE_1.enabled = false;
                camera_PR_1.enabled = false;
                camera_PR_2.enabled = true;
                camera_PR_3.enabled = false;
                camera_PR_4.enabled = false;
                camera_PR_5.enabled = false;
                break;
            case M_Camera.PR_3:
                camera_GP_1.enabled = false;
                camera_PA_1.enabled = false;
                camera_PA_2.enabled = false;
                camera_PE_1.enabled = false;
                camera_PR_1.enabled = false;
                camera_PR_2.enabled = false;
                camera_PR_3.enabled = true;
                camera_PR_4.enabled = false;
                camera_PR_5.enabled = false;
                break;
            case M_Camera.PR_4:
                camera_GP_1.enabled = false;
                camera_PA_1.enabled = false;
                camera_PA_2.enabled = false;
                camera_PE_1.enabled = false;
                camera_PR_1.enabled = false;
                camera_PR_2.enabled = false;
                camera_PR_3.enabled = false;
                camera_PR_4.enabled = true;
                camera_PR_5.enabled = false;
                break;
            case M_Camera.PR_5:
                camera_GP_1.enabled = false;
                camera_PA_1.enabled = false;
                camera_PA_2.enabled = false;
                camera_PE_1.enabled = false;
                camera_PR_1.enabled = false;
                camera_PR_2.enabled = false;
                camera_PR_3.enabled = false;
                camera_PR_4.enabled = false;
                camera_PR_5.enabled = true;
                break;
        }
        if (displayIHM)
        {
            IHMInterview.MaskAllNguiComponents(true);
        }
        else
        {
            IHMInterview.MaskAllNguiComponents(false);
        }

    }

}
