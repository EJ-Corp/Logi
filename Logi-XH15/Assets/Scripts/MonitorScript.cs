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
        if (mousePos.y < screenCorners[0].y || mousePos.y < screenCorners[3].y)
        {
           Mouse.current.WarpCursorPosition(new Vector2(mousePos.x, screenCorners[0].y));
        } else 
        if (mousePos.y > screenCorners[1].y || mousePos.y > screenCorners[2].y)
        {
            mousePos.y--;
        }

        //check x boundary
        if (mousePos.x < screenCorners[0].x || mousePos.x < screenCorners[1].x)
        {
            mousePos.x++;
        } else 
        if (mousePos.x > screenCorners[2].x || mousePos.x > screenCorners[3].x)
        {
            mousePos.x--;
        }
    }
}
