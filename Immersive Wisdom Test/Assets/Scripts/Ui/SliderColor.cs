using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour
{
    public enum mSliderColor
    {
        Red,
        Green,
        Blue
    };

    public mSliderColor ThisColor;
    private Texture2D RTexture, GTexture, BTexture;
    private int width = 150;
    private int height = 10;

    private void Awake()
    {
        //RectTransform operations referenced from https://forum.unity.com/threads/modify-the-width-and-height-of-recttransform.270993/
        
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        InitTextures();
        UpdateColor();
    }

    private void InitTextures()
    {
        if (RTexture == null)
        {
            RTexture = new Texture2D(width, 1);
        }
        if (GTexture == null)
        {
            GTexture = new Texture2D(width, 1);
        }
        if (BTexture == null)
        {
            BTexture = new Texture2D(width, 1);
        }
    }

    public void UpdateColor()
    {
        Color bgColor = Camera.main.backgroundColor;
        if (RTexture == null || GTexture == null || BTexture == null)
        {
            InitTextures();
        }

        if (ThisColor == mSliderColor.Red && (RTexture.GetPixel(0,0).g != bgColor.g || RTexture.GetPixel(0, 0).b != bgColor.b))
        {
            for (int i = 0; i < width; i++)
            {
                RTexture.SetPixel(i, 0, new Color(i / (float)width, bgColor.g, bgColor.b));
            }
            RTexture.Apply();
            GetComponent<RawImage>().texture = RTexture;
        }

        if (ThisColor == mSliderColor.Green && (GTexture.GetPixel(0, 0).r != bgColor.r || GTexture.GetPixel(0, 0).b != bgColor.b))
        {
            for (int i = 0; i < width; i++)
            {
                GTexture.SetPixel(i, 0, new Color(bgColor.r, i / (float)width, bgColor.b));
            }
            GTexture.Apply();
            GetComponent<RawImage>().texture = GTexture;
        }

        if (ThisColor == mSliderColor.Blue && (BTexture.GetPixel(0, 0).r != bgColor.r || BTexture.GetPixel(0, 0).g != bgColor.g))
        {
            for (int i = 0; i < width; i++)
            {
                BTexture.SetPixel(i, 0, new Color(bgColor.r, bgColor.g, i / (float)width));
            }
            BTexture.Apply();
            GetComponent<RawImage>().texture = BTexture;
        }

        //Dynamic gradient texture color referenced from https://forum.unity.com/threads/solved-changing-the-texture-of-an-ui-image-using-c.476064/
    }
}
