using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonitorScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private Mouse mouse;
    [SerializeField] private Canvas mouseBounds;
    [SerializeField] private Vector3 mousePos;
    private Vector3[] worldCorners = new Vector3[4];
    [SerializeField] private Vector3[] screenCorners = new Vector3[4];

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mouseBounds = gameManager.monitorBoundary;
        mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mouseBounds.GetComponent<RectTransform>().GetWorldCorners(worldCorners);

        for (int i = 0 ; i < 4; i++)
        {
            screenCorners[i] = gameManager.pcCamera.WorldToScreenPoint(worldCorners[i]); //botleft, topleft, topright, botright
        }

        if (gameManager.inPCView)
        {
            RestrictMouseToMonitorBounds();
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

}
