using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IDandPasswordInputScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private string textInput;
    [SerializeField] private TMP_InputField textDisplay;

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

    public void ResetInputResult()
    {
        textInput = null;
        textDisplay.text = null;
    }
}
