using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour
{
    private GameObject messageObj;
    private TMPro.TMP_Text messageArea;
    private GameObject countdownObj;
    private TMPro.TMP_Text countdownArea;
    private void Awake()
    {
        messageObj = transform.GetChild(0).gameObject;
        countdownObj = transform.GetChild(1).gameObject;
        messageArea = messageObj.GetComponent<TMPro.TMP_Text>();
        countdownArea = countdownObj.GetComponent<TMPro.TMP_Text>();
    }
    public void DisplayMessage(string message, float time = 10)
    {
        messageObj.SetActive(true);
        messageArea.text = message;
        if(time > 0)Invoke(nameof(HideMessage), time);
    } 
    public void HideMessage()
    {
        messageObj.SetActive(false);
    }

    public void WaveCountdown(int time, int wave)
    {
        countdownObj.SetActive(true);
        countdownArea.text = "Wave " + wave + "\n"+ time;
    }
    public void HideCountdown()
    {
        countdownObj.SetActive(false);
    }
}
