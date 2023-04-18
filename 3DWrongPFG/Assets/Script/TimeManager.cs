using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private float maxTime = 300;
    private float currentTime = 0;
    private float Timer;
    private TMP_Text textMeshPro;
    private bool notFinish = true;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = this.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer <= maxTime && notFinish)
        {
            Timer = Timer + Time.deltaTime;
            currentTime = maxTime - Timer;
            textMeshPro.text = string.Format("{0:N0}", currentTime);
        }
    }

    public int FinishGame()
    {
        notFinish = false;
        return (int)currentTime;
    }
}
