using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.UI;

public class MonitorScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private Mouse mouse;
    [SerializeField] private Canvas mouseBounds;
    [SerializeField] private Vector3 mousePos;
    private Vector3[] worldCorners = new Vector3[4];
    [SerializeField] private Vector3[] screenCorners = new Vector3[4];

    [SerializeField] string correctID = "Edgar";
    [SerializeField] string correctPSWD = "1234";
    [SerializeField] string idInputValue;
    [SerializeField] string pswdInputValue;

    [SerializeField] GameObject desktopScreen;
    [SerializeField] GameObject loginScreen;

    [SerializeField] GameObject[] speechPanels = new GameObject[4];
    private float nextFact = 0.0f;
    private float randomCountdown = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mouseBounds = gameManager.monitorBoundary;
        mouse = Mouse.current;
        desktopScreen.SetActive(false);
        randomCountdown = Random.Range(5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mouseBounds.GetComponent<RectTransform>().GetWorldCorners(worldCorners);

        idInputValue = gameManager.idInput.GetInputResult();
        pswdInputValue = gameManager.pswdInput.GetInputResult();
        CheckIDAndPassword();


        //We are getting the same 4 corners repeadetly every fram --- IS THIS NEEDED??? NOT JUST ONCE?? BAD CODE.
        for (int i = 0 ; i < 4; i++)
        {
            screenCorners[i] = gameManager.pcCamera.WorldToScreenPoint(worldCorners[i]); //botleft, topleft, topright, botright
        }

        if (gameManager.inPCView)
        {
            RestrictMouseToMonitorBounds();
        }

        nextFact += Time.deltaTime;

        if (nextFact >= randomCountdown)
        {
            RandomFact();
            nextFact = 0.0f;
            randomCountdown = Random.Range(5f, 15f);
        }
    }

    void CheckIDAndPassword()
    {
        if (idInputValue == correctID && pswdInputValue == correctPSWD)
        {
            loginScreen.SetActive(false);
            desktopScreen.SetActive(true);
        }
    }

    void RestrictMouseToMonitorBounds()
    {   //botleft0 (less than x and y)
        //topleft1 (less than x / greater than y)
        //topright2 (greater than x and y)
        //botright3 (greater than x less than y)

        //check y boundary
        if (mousePos.y <= screenCorners[0].y + 25)
        {
            Mouse.current.WarpCursorPosition(new Vector2(mousePos.x , screenCorners[0].y + 30));
        } else
        if(mousePos.y >= screenCorners[1].y)
        {
            Mouse.current.WarpCursorPosition(new Vector2(mousePos.x, screenCorners[1].y - 5));
        }

        //check x boundary
        if (mousePos.y >= ((screenCorners[0].y + screenCorners[1].y) / 2))
        {
            if(mousePos.x <= screenCorners[1].x)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenCorners[1].x + 5, mousePos.y));
            } else       
            if (mousePos.x >= screenCorners[2].x - 10)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenCorners[2].x - 15, mousePos.y));
            }  

        } else
        if (mousePos.y <= ((screenCorners[0].y + screenCorners[1].y) / 2))
        {
            if (mousePos.x <= screenCorners[0].x)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenCorners[0].x + 5, mousePos.y));
            } else        
            if(mousePos.x >= screenCorners[3].x - 10)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenCorners[3].x - 15, mousePos.y));
            }
        }
    }

    public void RandomFact()
    {
        float randomFactNum = Random.Range(0,3);
        float randomTurnOff = Random.Range(0,9);

        for (int i = 0; i < speechPanels.Length; i++)
        {
            if (i == randomFactNum)
            {
                speechPanels[i].SetActive(true);
            } //Wont turn off if its 4 or higher
            if (i == randomTurnOff)
            {
                speechPanels[i].SetActive(false);
            }
        }
    }

}
