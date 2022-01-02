using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui{
    public class ColorPick : MonoBehaviour
    {
        [SerializeField] private GameObject red;
        [SerializeField] private GameObject green;
        [SerializeField] private GameObject blue;
        [SerializeField] private GameObject hexCode;
        [SerializeField] private GameObject alpha;
        private int redValue = 255;
        private int greenValue = 255;
        private int blueValue = 255;
        private int alphaValue = 100;

        private void SetHex()
        {
            //hex conversion code referenced from https://stackoverflow.com/a/1139975

            string HexString = redValue.ToString("X2") + greenValue.ToString("X2") + blueValue.ToString("X2");
            hexCode.GetComponentInChildren<TMPro.TMP_InputField>().text = HexString;
            SetBackgroundColor();
        }

        private void SetBackgroundColor()
        {
            //color assignment code referenced from ColorPanel.cs and Unity documentation

            Camera.main.backgroundColor = new Vector4(redValue / 255f, greenValue / 255f,
                                                      blueValue / 255f, alphaValue / 100f);
            //Debug.Log(Camera.main.backgroundColor);
            red.GetComponentInChildren<SliderColor>().UpdateColor();
            green.GetComponentInChildren<SliderColor>().UpdateColor();
            blue.GetComponentInChildren<SliderColor>().UpdateColor();
        }

        private void Awake()
        {
            SetAllValues();
        }

        private void SetAllValues()
        {
            red.GetComponentInChildren<Slider>().value = redValue;
            red.GetComponentInChildren<TMPro.TMP_InputField>().text = redValue.ToString();

            green.GetComponentInChildren<Slider>().value = greenValue;
            green.GetComponentInChildren<TMPro.TMP_InputField>().text = greenValue.ToString();

            blue.GetComponentInChildren<Slider>().value = blueValue;
            blue.GetComponentInChildren<TMPro.TMP_InputField>().text = blueValue.ToString();

            alpha.GetComponentInChildren<Slider>().value = alphaValue;
            alpha.GetComponentInChildren<TMPro.TMP_InputField>().text = alphaValue.ToString();

            SetHex();
        }

        public void RedSlider()
        {
            //change slider value to match change in input field
            redValue = (int)red.GetComponentInChildren<Slider>().value;
            red.GetComponentInChildren<TMPro.TMP_InputField>().text = redValue.ToString();
            SetHex();
        }

        public void RedField()
        {
            //change input field value to match slider
            if (red.GetComponentInChildren<TMPro.TMP_InputField>().text == "") red.GetComponentInChildren<TMPro.TMP_InputField>().text = "0";
            redValue = int.Parse(red.GetComponentInChildren<TMPro.TMP_InputField>().text);    //string to int parse referenced from 
                                                                                              //Microsoft C# programming guide
            if (redValue > 255)     //handles invalid inputs, clamps to max or min
            {
                redValue = 255;
                red.GetComponentInChildren<TMPro.TMP_InputField>().text = "255";
            }
            else if (redValue < 0)
            {
                redValue = 0;
                red.GetComponentInChildren<TMPro.TMP_InputField>().text = "0";
            }
            red.GetComponentInChildren<Slider>().value = redValue;
            SetHex();
        }

        public void GreenSlider()
        {
            greenValue = (int)green.GetComponentInChildren<Slider>().value;
            green.GetComponentInChildren<TMPro.TMP_InputField>().text = greenValue.ToString();
            SetHex();
        }

        public void GreenField()
        {
            if (green.GetComponentInChildren<TMPro.TMP_InputField>().text == "") green.GetComponentInChildren<TMPro.TMP_InputField>().text = "0";
            greenValue = int.Parse(green.GetComponentInChildren<TMPro.TMP_InputField>().text);
            if (greenValue > 255)
            {
                greenValue = 255;
                green.GetComponentInChildren<TMPro.TMP_InputField>().text = "255";
            }
            else if (greenValue < 0)
            {
                greenValue = 0;
                green.GetComponentInChildren<TMPro.TMP_InputField>().text = "0";
            }
            green.GetComponentInChildren<Slider>().value = greenValue;
            SetHex();
        }

        public void BlueSlider()
        {
            blueValue = (int)blue.GetComponentInChildren<Slider>().value;
            blue.GetComponentInChildren<TMPro.TMP_InputField>().text = blueValue.ToString();
            SetHex();
        }

        public void BlueField()
        {
            if (blue.GetComponentInChildren<TMPro.TMP_InputField>().text == "") blue.GetComponentInChildren<TMPro.TMP_InputField>().text = "0";
            blueValue = int.Parse(blue.GetComponentInChildren<TMPro.TMP_InputField>().text);
            if (blueValue > 255)
            {
                blueValue = 255;
                blue.GetComponentInChildren<TMPro.TMP_InputField>().text = "255";
            }
            else if (blueValue < 0)
            {
                blueValue = 0;
                blue.GetComponentInChildren<TMPro.TMP_InputField>().text = "0";
            }
            blue.GetComponentInChildren<Slider>().value = blueValue;
            SetHex();
        }

        public void AlphaSlider()
        {
            alphaValue = (int)alpha.GetComponentInChildren<Slider>().value;
            alpha.GetComponentInChildren<TMPro.TMP_InputField>().text = alphaValue.ToString();
            SetBackgroundColor();
        }

        public void AlphaField()
        {
            if (alpha.GetComponentInChildren<TMPro.TMP_InputField>().text == "") alpha.GetComponentInChildren<TMPro.TMP_InputField>().text = "0";
            alphaValue = int.Parse(alpha.GetComponentInChildren<TMPro.TMP_InputField>().text);
            if (alphaValue > 100)
            {
                alphaValue = 100;
                alpha.GetComponentInChildren<TMPro.TMP_InputField>().text = "100";
            }
            if (alphaValue < 0)
            {
                alphaValue = 0;
                alpha.GetComponentInChildren<TMPro.TMP_InputField>().text = "0";
            }
            alpha.GetComponentInChildren<Slider>().value = alphaValue;
            SetBackgroundColor();
        }

        public void HexField()
        {
            string input = hexCode.GetComponentInChildren<TMPro.TMP_InputField>().text;
            if (input.Length < 6)       //input does not have enough characters
            {
                SetHex();
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    //valid hex character check referenced from https://stackoverflow.com/a/223854

                    if (!(input[i] >= '0' && input[i] <= '9' ||
                        input[i] >= 'a' && input[i] <= 'f' ||
                        input[i] >= 'A' && input[i] <= 'F'))    //input is not valid hex character
                    {
                        SetHex();
                        return;
                    }
                }
                string red = input.Substring(0, 2);
                string green = input.Substring(2, 2);
                string blue = input.Substring(4, 2);

                //hex conversion code referenced from https://stackoverflow.com/a/1139975

                redValue = Convert.ToInt32(red, 16);
                greenValue = Convert.ToInt32(green, 16);
                blueValue = Convert.ToInt32(blue, 16);
                SetAllValues();
            }
        }
    }
}