using LTH.ColorMatch.Data;
using UnityEngine;
using UnityEngine.UI;


public class Cell : MonoBehaviour
{
    public Color color;
    public Image img;
    public ColorMatchColor originColor;
    public ColorMatchColor grayColor;
    
    public void SetCell(Transform parent, int width, int height, int x, int y)
    {
        transform.SetParent(parent);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        transform.localPosition = new Vector2(x * width, -(y * height));
    }

    public void SetCell(Transform parent, int width, int height, int x, int y, CustomColor customColor)
    {
        // SetCell(parent,width,height,x,y);
        transform.SetParent(parent);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        transform.localPosition = new Vector2(x * width, -(y * height));

        ColorMatchColor colorMatchColor = (customColor.complete) ? customColor.originColorMatchColor : customColor.grayColorMatchColor;

        Color convertColor = new Color(colorMatchColor.r, colorMatchColor.g, colorMatchColor.b, colorMatchColor.a);
        color = convertColor;
        img.color = convertColor;
        originColor = customColor.originColorMatchColor;
        grayColor = customColor.grayColorMatchColor;
    }

    public void Fill()
    {
        Color setColor = new Color(originColor.r, originColor.g, originColor.b, originColor.a);
        color = setColor;
        img.color = color;
    }

    // public void SetPixelColor(ColorMatchColor setColor)
    // {
    //     Color convertColor = new Color(setColor.r, setColor.g, setColor.b, setColor.a);
    //     color = convertColor;
    //     img.color = color;
    // }
}
