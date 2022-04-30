using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    private GameObject messageObj;
    private TMPro.TMP_Text messageArea;
    private GameObject countdownObj;
    private TMPro.TMP_Text countdownArea;
    private void Start()
    {
        messageObj = transform.GetChild(0).gameObject;
        countdownObj = transform.GetChild(1).gameObject;
        messageArea = messageObj.GetComponent<TMPro.TMP_Text>();
        countdownArea = countdownObj.GetComponent<TMPro.TMP_Text>();
    }
    public void DisplayMessage(string message, float time = 10)
    {
        messageArea.GetComponent<TMPro.TMP_Text>().text = message;
        messageObj.SetActive(true);
        if(time > 0)Invoke(nameof(HideMessage), time);
    } 
    public void HideMessage()
    {
        messageObj.SetActive(false);
    }

    public void WaveCountdown(int time, int wave)
    {
        countdownArea.GetComponent<TMPro.TMP_Text>().text = "Wave " + wave + "\n"+ time;
        countdownObj.SetActive(true);
    }
    public void HideCountdown()
    {
        countdownObj.SetActive(false);
    }
}
