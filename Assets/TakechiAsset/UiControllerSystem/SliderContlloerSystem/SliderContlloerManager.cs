using System.Collections;
using System.Collections.Generic;
using TakechiEngine.PUN;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.SliderContlloer
{
    public class SliderContlloerManager : TakechiPunCallbacks
    {
        protected void setValue( Slider slider, float value)
        {
            slider.value = value;
        }

        protected void updateValue( Slider slider, float value, float max)
        {
            slider.value = value / max;
        }

        protected void setValue( Slider slider, int value, int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = value;
        }

        protected void updateValue( Slider slider, int value)
        {
            slider.value = value;
        }
    }
}
