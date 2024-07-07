using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PressurePuzzle : MonoBehaviour
{
    [SerializeField] private float innerDistance;
    [SerializeField] private float outerDistance;
    [SerializeField] private float handleDistance;
    [SerializeField] private GameObject handle;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject arrowhead;
    [SerializeField] private ProblemHandler warningSign;
    [SerializeField] private AudioSource releaseSFX;
    [SerializeField] private bool isIncreasingPressure;
    [SerializeField] private bool isBroken;
    [SerializeField] private bool isCurrentlyReleasing;
    [SerializeField] private float pressure = 0f;
    [SerializeField] private float safePressure = 40f;
    [SerializeField] private float dangerousPressure = 60f;
    [SerializeField] private float maxPressure = 100f;
    [SerializeField] private int pressureReleaseSpeed = 1;
    [SerializeField] private float arrowheadRotationFactor = 2.8f;
    [SerializeField] private int arrowheadRotationOffset = -230;
    [SerializeField] private float fadeSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        pressure = 0f;
        isIncreasingPressure = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate distance from handle to pressure gauge
        handleDistance = Vector3.Distance(handle.transform.position, transform.position);

        //Calculate arrowhead rotation from pressure
        arrowhead.transform.localEulerAngles = new Vector3(arrowheadRotationOffset + (pressure * arrowheadRotationFactor), -90, 90);

        //Increase pressure
        if(isIncreasingPressure && pressure < maxPressure)
        {
            pressure += Time.deltaTime;
        }

        

        //Break puzzle
        //TODO: Ping player + update data transfer speed
        if(pressure >= dangerousPressure)
        {
            Debug.Log("Pressure is greater than danger zone");
            isBroken = true;
        }

        if(!isCurrentlyReleasing && releaseSFX.volume > 0)
        {
            Debug.Log("Decreasing Volume");
            releaseSFX.volume -= Time.deltaTime * fadeSpeed;
        }

        //Relieve pressure if handle is being pulled
        if(handleDistance > innerDistance && pressure > 0)
        {
            isCurrentlyReleasing = true;
            if(releaseSFX.volume < 1)
            {
                releaseSFX.volume += Time.deltaTime * fadeSpeed; 
            }
            pressure -= Time.deltaTime * pressureReleaseSpeed;


            if(isIncreasingPressure) //Problem is active on the handler
            {
                //Stop it from going up and tell the problem handler it can be fired again
                Debug.Log("We shoudl stop increasing");
                isIncreasingPressure = false;
                warningSign.FixProblemOnHandler(13);
            }
            
            //break once it reaches red

            // if(!isBroken)
            // {
            //     isBroken = true;
            // }
        }

        //Fix puzzle if pressure is safe
        if(pressure <= safePressure && isBroken == true)
        {
            isBroken = false;
            isIncreasingPressure = false;
            Debug.Log("We gonna fix it");
            warningSign.FixProblemOnHandler(13);
            
        }

        //Release handle when outside range
        if(handleDistance > outerDistance)
        {
            handle.GetComponent<Rigidbody>().isKinematic = false;
            handle.transform.parent = this.transform;
        }

        if(pressure <= 0 || handleDistance < innerDistance)
        {
            Debug.Log("Setting state to false");
            isCurrentlyReleasing = false;
        }
    }

    public void ActivateProblem()
    {
        isIncreasingPressure = true;
        Debug.Log("Problem is Pressure");
    }
}
