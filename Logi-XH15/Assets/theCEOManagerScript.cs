using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theCEOManagerScript : MonoBehaviour
{
    private static theCEOManagerScript ceo;
    public static theCEOManagerScript CEO
    {
        get {return ceo;}
    }

    void Awake()
    {
        if(ceo == null)
        {
            ceo = this;
            DontDestroyOnLoad(this.gameObject);
        } else 
        if (ceo != this)
        {
            Destroy(this.gameObject);
        }
    }
}
