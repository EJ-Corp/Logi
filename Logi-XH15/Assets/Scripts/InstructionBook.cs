using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class InstructionBook : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string content;

    [SerializeField] private TMP_Text currentPage;
    [SerializeField] private TMP_Text pagination;

    [SerializeField] private Image previousIcon;
    
    [SerializeField] private Image nextIcon;
    public int pageTotal;
    public float distance = 5;
    public Vector3 offset;
    // Start is called before the first frame update

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameObject book;
    private GameObject bookInstance;
    // Start is called before the first frame update

    void Start()
    {

        SetupContent();
        UpdatePagination();

        if(gameObject.transform.parent.name != "NoteBook Origin")
        {
            player = gameObject.transform.parent.parent.parent.gameObject;
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void SetupContent()
    {
        currentPage.text = content;
    }

    private void OnValidate()
    {
        UpdatePagination();

        if (currentPage.text == content)
            return;

        SetupContent();
    }

    private void UpdatePagination()
    {
        pagination.text = currentPage.pageToDisplay.ToString();
        if(currentPage.pageToDisplay <= 1)
        {
            previousIcon.enabled = false;
        }
        else
        {
            previousIcon.enabled = true;
        }

        if(currentPage.pageToDisplay >= pageTotal)
        {
            nextIcon.enabled = false;
        }
        else
        {
            nextIcon.enabled = true;
        }
    }

    public void PreviousPage()
    {
        if (currentPage.pageToDisplay <= 1)
        {
            currentPage.pageToDisplay = 1;
            return;

        }
        else
        {
            currentPage.pageToDisplay--;
        }

        UpdatePagination();
    }

    public void NextPage()
    {
        if (currentPage.pageToDisplay >= pageTotal)
        {
            currentPage.pageToDisplay = pageTotal;
            return;
        }
        else
        {
            currentPage.pageToDisplay++;
        }

        UpdatePagination();
    }

    
    public void ExitBook(){
        
        //Debug.Log("Exit Book");
        playerController.CanMove = true;
        GameManager.Manager.isReading = false;
        playerController.PlayerReticle.enabled = true;
        Destroy(gameObject.transform.parent.gameObject);

    }
}
