using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{

    public Light light;


    void Update()
    {
        if (light)
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.RightBracket))
            {
                light.intensity += 0.5f;
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.LeftBracket))
            {
                light.intensity -= 0.5f;
            }
        }
    }
}
