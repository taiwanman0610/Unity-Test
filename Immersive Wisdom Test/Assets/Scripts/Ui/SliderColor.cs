using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*public class SliderColor : MaskableGraphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //base.OnPopulateMesh(vh);
        UpdateColors(vh);
    }

    public void UpdateColors(VertexHelper vh)
    {
        vh.Clear();
        vh.AddVert(new Vector3(-80, -10), Color.black, new Vector2(0, 0));
        vh.AddVert(new Vector3(-80, 10), Color.black, new Vector2(0, 1));
        vh.AddVert(new Vector3(80, 10), Camera.main.backgroundColor, new Vector2(1, 1));
        vh.AddVert(new Vector3(80, -10), Camera.main.backgroundColor, new Vector2(1, 0));

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(0, 2, 3);
    }

    private void Update()
    {
        UpdateMaterial();
    }
}*/

public class SliderColor : MonoBehaviour
{
    public enum mSliderColor
    {
        Red,
        Green,
        Blue
    };

    public mSliderColor ThisColor;
    private Texture2D mTexture;
    private int width = 150;
    private int height = 10;

    private void Awake()
    {
        //RectTransform operations referenced from https://forum.unity.com/threads/modify-the-width-and-height-of-recttransform.270993/
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        mTexture = new Texture2D(width, 1);
    }

    public void UpdateColor()
    {
        Color bgColor = Camera.main.backgroundColor;
        for (int i = 0; i < width; i++)
        {
            if (ThisColor == mSliderColor.Red) mTexture.SetPixel(i, 0, new Color(i/(float)width, bgColor.g, bgColor.b));
            else if (ThisColor == mSliderColor.Green) mTexture.SetPixel(i, 0, new Color(bgColor.r, i / (float)width, bgColor.b));
            else mTexture.SetPixel(i, 0, new Color(bgColor.r, bgColor.g, i / (float)width));
        }
        mTexture.Apply();
        GetComponent<RawImage>().texture = mTexture;

        //Dynamic gradient texture color referenced from https://forum.unity.com/threads/solved-changing-the-texture-of-an-ui-image-using-c.476064/
    }
}
