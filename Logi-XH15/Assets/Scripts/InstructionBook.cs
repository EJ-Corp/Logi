using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class InstructionBook : Interactable
{
    [TextArea(3, 10)]
    [SerializeField] private string content;

    [SerializeField] private TMP_Text currentPage;
    [SerializeField] private TMP_Text pagination;
    public int pageTotal;
    public float distance = 5;
    public Vector3 offset;
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
            playerController = gameObject.transform.parent.parent.parent.GetComponentInParent<PlayerController>();
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

        bookInstance = Instantiate(book, Camera.main.transform);
        bookInstance.transform.localPosition = offset;
        bookInstance.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));

    }

    public void ExitBook(){
        
        Debug.Log("Exit Book");
        Destroy(gameObject.transform.parent.gameObject);
        playerController.CanMove = true;
    }
    public override void OnFocus()
    {

    }
    public override void OnLoseFocus()
    {

    }
}
