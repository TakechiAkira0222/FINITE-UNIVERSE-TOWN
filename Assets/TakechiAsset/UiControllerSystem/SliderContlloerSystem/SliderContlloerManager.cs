using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.SliderContlloer
{
    public class SliderContlloerManager : MonoBehaviour
    {
        protected void setValue(Slider slider, float value)
        {
            slider.value = value;
        }

        protected void updateValue(Slider slider, float value, float max)
        {
            slider.value = value / max;
        }
    }
}
