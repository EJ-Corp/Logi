using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordGenerator : MonoBehaviour
{
    [SerializeField] private int length = 7;
    [SerializeField] private TMP_Text passwordText;
    [SerializeField] private int max_length = 8;
    [SerializeField] private int min_length = 4;
    [SerializeField] private bool randomLength = false;

    // Start is called before the first frame update
    void Start()
    {
       // passwordText.text = GeneratePassword();
    }

    private string GeneratePassword()
    {
        if (randomLength)
        {
            length = UnityEngine.Random.Range(min_length, max_length);
        }

        string allChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        string password = "";

        for( int i = 0; i < length; i++)
        {
            password += allChars[UnityEngine.Random.Range(0, allChars.Length)];
        }

        return password;
    }
}
