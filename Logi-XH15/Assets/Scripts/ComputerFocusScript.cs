using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerFocusScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] private Camera roomCamera;
    [SerializeField] private Camera pcFocusCamera; 
    [SerializeField] private AudioListener roomListener;
    [SerializeField] private AudioListener pcFocusListener; 
    [SerializeField] private Animator cameraAnimator;

    [SerializeField] private PlayerController playerController;




    private float inputHorizontal;
    private float inputVertical;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        roomCamera = gameManager.roomCamera;
        roomListener = gameManager.roomListener;

        pcFocusCamera = gameManager.pcFocusCamera;
        pcFocusCamera.enabled = false;
        pcFocusListener = gameManager.pcFocusListener;
        pcFocusListener.enabled = false;

        cameraAnimator = roomCamera.GetComponent<Animator>();
        cameraAnimator.enabled = false;

        playerController = gameManager.playerController;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && pcFocusCamera.enabled == true)
        {
            cameraAnimator.SetTrigger("unfocusOnPC");
        }
    }

    private void OnMouseUpAsButton() 
    {
        cameraAnimator.enabled = true;
        cameraAnimator.SetTrigger("focusOnPC");
        playerController.CanMove = false;
    }

    public void SwapCameras() 
    {
        roomCamera.enabled = !roomCamera.enabled;
        roomListener.enabled = !roomListener.enabled;
        pcFocusCamera.enabled = !pcFocusCamera.enabled;
        pcFocusListener.enabled = !pcFocusListener.enabled;
    }
}
