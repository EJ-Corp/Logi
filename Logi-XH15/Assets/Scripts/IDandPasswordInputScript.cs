using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IDandPasswordInputScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private string textInput;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetTextInput(string playerInput)
    {
        textInput = playerInput;
    }

    public string GetInputResult()
    {
        return textInput;
    }
}
