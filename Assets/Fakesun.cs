using UnityEngine;

public class Fakesun : MonoBehaviour
{
    public Light sunLight; 

    void Update()
    {
        transform.forward = -sunLight.transform.forward;
    }
}