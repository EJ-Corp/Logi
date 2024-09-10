using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBookInteract : Interactable
{
    public float distance = 5;
    public Vector3 offset;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameObject book;
    private GameObject bookInstance;
    void Start()
    {
        //player = gameObject.transform.parent.parent.gameObject;
        playerController = player.GetComponent<PlayerController>();
    }
    public override void OnInteract()
    {
        //Debug.Log("Book Touched");
        GameManager.Manager.isReading = true;
        playerController.CanMove = false;
        playerController.PlayerReticle.enabled = false;

        bookInstance = Instantiate(book, Camera.main.transform);
        bookInstance.transform.localPosition = offset;
        bookInstance.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));

    }

    public override void OnFocus()
    {

    }
    public override void OnLoseFocus()
    {

    }

    public override void OnStopInteract()
    {

    }
}
