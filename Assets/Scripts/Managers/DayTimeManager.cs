using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeManager : MonoBehaviour
{
    [Header("In-Game Time Settings")]
    public int startTime = 12 * 60;
    public float timeSpeedMultiplier = 1;    
    private int currentTime;    

    [Header("Objects in scene")]
    public GameObject flashlightObject;
    public TMPro.TMP_Text dayValueField;
    public TMPro.TMP_Text timeValueField;
    public Light lightObject;

    private void Start()
    {
        currentTime = startTime;
        UpdateTextFields();
        StartCoroutine(DayTimer());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            timeSpeedMultiplier -= 1f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            timeSpeedMultiplier += 1f;
        }
    }
    public IEnumerator DayTimer()
    {
        
        yield return new WaitForSeconds(1/timeSpeedMultiplier);
        if (WaveSystem.waveIsRunning)
        {
            currentTime++;
            UpdateTextFields();
            if(currentTime % (24 * 60) >= (18 * 60))
            {
                ChangeLightIntensity(-0.006f);                
            }
            else if (currentTime % (24 * 60) <= (10 * 60) && currentTime % (24 * 60) > (4 * 60))
            {
                ChangeLightIntensity(0.006f);                
            }
        }
        if(currentTime % (24 * 60) == 22 * 60)
        {
            TurnOnFlashlight();
        }
        else if(currentTime % (24 * 60) == 6 * 60)
        {
            TurnOffFlashlight();
        }
        StartCoroutine(DayTimer());
    }

    public void UpdateTextFields()
    {
        dayValueField.text = getDays().ToString();
        timeValueField.text = string.Format("{0:D2}:{1:D2}", getHours(), getMinutes());
    }

    public void ChangeLightIntensity(float value)
    {
        lightObject.intensity += value;
    }
    
    public void TurnOnFlashlight()
    {
        flashlightObject.SetActive(true);
    }
    public void TurnOffFlashlight()
    {
        flashlightObject.SetActive(false);
    }
    public int getTime()
    {
        return currentTime;
    }
    public int getDays()
    {
        return Mathf.FloorToInt(currentTime / (24*60)) + 1;
    }
    public int getHours()
    {
        return Mathf.FloorToInt((currentTime % (24 * 60)) / 60);
    }
    public int getMinutes()
    {
        return Mathf.FloorToInt(currentTime % 60);
    }
}
