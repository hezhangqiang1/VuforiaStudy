using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FrameQuality : MonoBehaviour {

    public Image[] LowMidHigh;

    void SetMeter(Color low,Color mid,Color high)
    {
        if (LowMidHigh.Length == 3)
        {
            if (LowMidHigh[0])
            {
                LowMidHigh[0].color = low;
            }
            if (LowMidHigh[1])
            {
                LowMidHigh[1].color = mid;
            }
            if (LowMidHigh[2])
            {
                LowMidHigh[2].color = high;
            }
        }
    }
    /// <summary>
    /// 设置质量
    /// </summary>
    /// <param name="frameQuality"></param>
    public void SetQuality(Vuforia.ImageTargetBuilder.FrameQuality frameQuality)
    {
        switch (frameQuality)
        {
            case (Vuforia.ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE):
                SetMeter(Color.gray, Color.gray, Color.gray);
                break;
            case (Vuforia.ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW):
                SetMeter(Color.red, Color.gray, Color.gray);
                break;
            case (Vuforia.ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM):
                SetMeter(Color.red, Color.yellow, Color.gray);
                break;
            case (Vuforia.ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH):
                SetMeter(Color.red, Color.yellow, Color.green);
                break;

        }

    }
}
