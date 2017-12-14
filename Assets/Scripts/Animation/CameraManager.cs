using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera camera_PE_1; // Plan d'ensemble
    public Camera camera_PA_1; // Plan Americain avec 1 seul personnage
    public Camera camera_PA_2; // Plan Americain avec les 2 personnages
    public Camera camera_GP_1; // Gros Plan

    // Use this for initialization
    void Start()
    {
        camera_PE_1.enabled = true;
        camera_PA_1.enabled = false;
        camera_PA_2.enabled = false;
        camera_GP_1.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            camera_PE_1.enabled = !camera_PE_1.enabled;
            camera_GP_1.enabled = !camera_GP_1.enabled;
        }
    }
}
