using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonProblem : MonoBehaviour
{
    [SerializeField] private List<InteractButton> buttons;
    [SerializeField] private InteractButton fixButton;

    //Maybe get rid of this and make it more efficient -> only used to get the flashing warning sign.
    [SerializeField] private ProblemHandler warningSign;
    private bool problemActive = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!problemActive)
            {
                ActivateProblem();
            } else 
            {
                Debug.Log("Button problem is already active");
            }
            
        }
    }

    public void ActivateProblem()
    {
        int randomButton = Random.Range(0, buttons.Count);
        fixButton = buttons[randomButton];
        fixButton.MakeProblem(warningSign);
        Debug.Log("Problem is the " + buttons[randomButton].name);

        warningSign.StartProblem();
    }
}
