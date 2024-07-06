using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionBook : Interactable
{
    [TextArea(3, 10)]
    [SerializeField] private string content;

    [SerializeField] private TMP_Text currentPage;
    [SerializeField] private TMP_Text pagination;
    public int pageTotal;
    public float distance = 5;
    public Vector3 offset;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update

    void Start()
    {
        SetupContent();
        UpdatePagination();
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
    }

    public void PreviousPage()
    {
        if (currentPage.pageToDisplay < 1)
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
        if (currentPage.pageToDisplay > pageTotal)
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
    public override void OnInteract()
    {
        Debug.Log("Book Touched");
        playerController.CanMove = false;
        transform.position = Camera.main.transform.position + offset;
        transform.rotation = Camera.main.transform.rotation;

        //transform.rotation.x = transform.rotation.x*-1;
        //transform.rotation

    }
    public override void OnFocus()
    {

    }
    public override void OnLoseFocus()
    {

    }
}
