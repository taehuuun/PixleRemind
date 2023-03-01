using UnityEngine;
using UnityEngine.UI;


public class Cell : MonoBehaviour
{
    
    public Color color;
    public Image img;

    public void SetCell(Transform parent, int width, int height, int x, int y)
    {
        transform.SetParent(parent);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        transform.localPosition = new Vector2(x * width, -(y * height));
    }

    public void SetCell(Transform parent, int width, int height, int x, int y, Color pixelColor)
    {
        transform.SetParent(parent);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        transform.localPosition = new Vector2(x * width, -(y * height));
        color = pixelColor;
        img.color = color;
    }
}
