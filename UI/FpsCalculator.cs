using System;
using TMPro;
using UnityEngine;

public class FpsCalculator : MonoBehaviour
{
    [SerializeField]
    private TMP_Text fpsText;

    int frameCount = 0;
    double deltaTime = 0.0;
    double fps = 0.0;
    double updateRate = 4.0;

    private void Update()
    {
        frameCount ++;
        deltaTime += Time.deltaTime;

        if (deltaTime > 1.0/updateRate)
        {
            fps = frameCount / deltaTime;

            fpsText.text = ((int) fps).ToString();

            frameCount = 0;
            deltaTime -= 1.0 / updateRate;
        }
    }
}
