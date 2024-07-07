using System.Collections;
using System.Collections.Generic;
using System.Security;
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
    [SerializeField] private bool isIncreasingPressure;
    [SerializeField] private bool isBroken;
    [SerializeField] private float pressure = 0f;
    [SerializeField] private float safePressure = 40f;
    [SerializeField] private float dangerousPressure = 60f;
    [SerializeField] private float maxPressure = 100f;
    [SerializeField] private int pressureReleaseSpeed = 1;
    [SerializeField] private float arrowheadRotationFactor = 2.8f;
    [SerializeField] private int arrowheadRotationOffset = -230;

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
            isBroken = true;
        }

        //Relieve pressure if handle is being pulled
        if(handleDistance > innerDistance && pressure > 0)
        {
            pressure -= Time.deltaTime * pressureReleaseSpeed;
            if(!isBroken)
            {
                isBroken = true;
            }
        }

        //Fix puzzle if pressure is safe
        if(pressure <= safePressure && isBroken == true)
        {
            warningSign.FixProblemOnHandler(13);
            isBroken = false;
            isIncreasingPressure = false;
        }

        //Release handle when outside range
        if(handleDistance > outerDistance)
        {
            handle.GetComponent<Rigidbody>().isKinematic = false;
            handle.transform.parent = this.transform;
        }
    }

    public void ActivateProblem()
    {
        isIncreasingPressure = true;
        Debug.Log("Problem is Pressure");
    }
}
