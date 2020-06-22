using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text val;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        val.text = slider.value.ToString();
    }
}
