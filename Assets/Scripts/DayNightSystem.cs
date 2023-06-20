using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DayNightSystem : MonoBehaviour
{
    public float rotationFactor;

    public float currentTime;
    private float dayLenghtMinutes;
    public TextMeshProUGUI timeText;
    public Material stars;
    
    public float rotationSpeed;
    float midday;
    float translateTime;
    

    string AMPM = "AM";

    //



    void Start()
    {
        
        rotationFactor = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        dayLenghtMinutes = 1 * rotationFactor;
        rotationSpeed = 360 / dayLenghtMinutes / 60;
        midday = dayLenghtMinutes * 60 / 2;


        currentTime += 1 * Time.deltaTime;
        translateTime = (currentTime / (midday * 2));

        float t = translateTime * 24f;
        float hours = Mathf.Floor(t);
        

        string displayHours = hours.ToString();
        if (hours == 0)
        {
            displayHours = "12";
        }

        if (hours > 12)
        {
            displayHours = (hours - 12).ToString();
        }

        if (currentTime >= midday / 2 && currentTime <= midday * 1.5)
        {
            if (stars.GetFloat("_Cutoff") < 1)
            {
                float alpha = stars.GetFloat("_Cutoff") * 100f;
                alpha += 3 * rotationSpeed * Time.deltaTime;
                alpha = alpha * .01f;
                stars.SetFloat("_Cutoff", alpha);
            }
        }

        else
        {
            if (stars.GetFloat("_Cutoff") > .6f)
            {
                float alpha = stars.GetFloat("_Cutoff") * 100f;
                alpha -= 3 * rotationSpeed * Time.deltaTime;
                alpha = alpha * .01f;
                stars.SetFloat("_Cutoff", alpha);
            }
        }



        if (currentTime >= midday)
        {
            if (AMPM != "PM")
            {
                AMPM = "PM";
            }
        }

        if (currentTime >= midday * 2)
        {
            if (AMPM != "AM")
            {
                AMPM = "AM";
            }
            currentTime = 0;
        }

        t *= 60;
        float minutes = Mathf.Floor(t % 60);

        string displayMinutes = minutes.ToString();
        if (minutes < 10)
        {
            displayMinutes = "0" + minutes.ToString();
        }

        string displayTime = displayHours + ":" + displayMinutes + " " + AMPM;
        timeText.text = displayTime;

        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
    }
}
