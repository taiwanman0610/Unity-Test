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

    private void Awake()
    {
        InitTextures();
        UpdateColor();
    }

    private void InitTextures()
    {
        //creates 2x1 texture to set as slider background. When assigned to raw image, if the texture doesn't cover the image area,
        //Unity will create a gradient effect that interpolates between the two assigned colors
        if (RTexture == null)
        {
            RTexture = new Texture2D(2, 1);
        }
        if (GTexture == null)
        {
            GTexture = new Texture2D(2, 1);
        }
        if (BTexture == null)
        {
            BTexture = new Texture2D(2, 1);
        }
    }

    public void UpdateColor()
    {
        Color bgColor = Camera.main.backgroundColor;
        if (RTexture == null || GTexture == null || BTexture == null)
        {
            InitTextures();
        }

        //Dynamic gradient texture color referenced from https://forum.unity.com/threads/solved-changing-the-texture-of-an-ui-image-using-c.476064/

        if (ThisColor == mSliderColor.Red && (RTexture.GetPixel(0,0).g != bgColor.g || RTexture.GetPixel(0, 0).b != bgColor.b))
        {
            //0 red value at leftmost point of slider, 1 red value at rightmost, keeping the other two color values the same
            RTexture.SetPixel(0, 0, new Color(0, bgColor.g, bgColor.b));
            RTexture.SetPixel(1, 0, new Color(1, bgColor.g, bgColor.b));
            RTexture.Apply();
            GetComponent<RawImage>().texture = RTexture;
        }

        if (ThisColor == mSliderColor.Green && (GTexture.GetPixel(0, 0).r != bgColor.r || GTexture.GetPixel(0, 0).b != bgColor.b))
        {
            GTexture.SetPixel(0, 0, new Color(bgColor.r, 0, bgColor.b));
            GTexture.SetPixel(1, 0, new Color(bgColor.r, 1, bgColor.b));
            GTexture.Apply();
            GetComponent<RawImage>().texture = GTexture;
        }

        if (ThisColor == mSliderColor.Blue && (BTexture.GetPixel(0, 0).r != bgColor.r || BTexture.GetPixel(0, 0).g != bgColor.g))
        {
            BTexture.SetPixel(0, 0, new Color(bgColor.r, bgColor.g, 0));
            BTexture.SetPixel(1, 0, new Color(bgColor.r, bgColor.g, 1));
            BTexture.Apply();
            GetComponent<RawImage>().texture = BTexture;
        }

    }
}
