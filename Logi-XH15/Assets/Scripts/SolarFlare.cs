using UnityEngine;
using UnityEngine.Audio;

public class SolarFlare : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 10f;
    public float timeElapsed = 0f;
    public float opacity = 1;
    public AudioMixer masterMixer;
    public float lowpass = 0f;

    void Start()
    {
        transform.Rotate(0, 180, 0);
    }
    void Update()
    {
        transform.Translate(Vector3.back*speed*Time.deltaTime);
        timeElapsed += Time.deltaTime;
        masterMixer.SetFloat("FlareLowpass", lowpass);
        float t = Mathf.Clamp01(timeElapsed / lifetime);
        lowpass = MapToFrequency(t);

        if(timeElapsed >= lifetime)
        {
            //Destroy(this.gameObject);
        }
    }

    public float MapToFrequency(float value)
    {
        // Clamp value to ensure it's between 0 and 1
        value = Mathf.Clamp01(value);

        // Define the frequency range
        float minFrequency = 10f; // 10 Hz
        float maxFrequency = 22000f; // 22 kHz

        // Calculate the logarithmic interpolation
        float minLog = Mathf.Log10(minFrequency);
        float maxLog = Mathf.Log10(maxFrequency);

        // Map the linear value to the logarithmic scale
        float logFrequency = Mathf.Lerp(minLog, maxLog, value);

        // Convert back to linear scale
        return Mathf.Pow(10, logFrequency);
    }

    public void BreakComputer()
    {
        GameManager.Manager.ToggleComputer(true);
    }
}