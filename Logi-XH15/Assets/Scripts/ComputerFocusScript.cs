using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerFocusScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioListener mainListener;

    [SerializeField] private Camera pcCamera; 
    [SerializeField] private AudioListener pcListener; 

    [SerializeField] private Animator cameraAnimator;

    [SerializeField] private PlayerController playerController;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mainCamera = gameManager.mainCamera;
        mainListener = gameManager.mainListener;

        pcCamera = gameManager.pcCamera;
        pcCamera.enabled = false;
        pcListener = gameManager.pcListener;
        pcListener.enabled = false;

        cameraAnimator = mainCamera.GetComponent<Animator>();
        cameraAnimator.enabled = false;

        playerController = gameManager.playerController;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && pcCamera.enabled == true)
        {
            cameraAnimator.enabled = true;
            cameraAnimator.SetTrigger("pcTOmain");
        }
    }

    private void OnMouseUpAsButton() 
    {
        cameraAnimator.enabled = true;
        cameraAnimator.SetTrigger("mainTOpc");
        playerController.CanMove = false;
    }

    public void SwapCameras() 
    {
        mainCamera.enabled = !mainCamera.enabled;
        mainListener.enabled = !mainListener.enabled;

        pcCamera.enabled = !pcCamera.enabled;
        pcListener.enabled = !pcListener.enabled;
    }
}
