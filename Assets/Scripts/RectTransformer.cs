using UnityEngine;

public static class RectTransformer
{
    public static void SetImageSize(RectTransform rectTransform, int textureWidth, int textureHeight, Vector2 resolution)
    {
        var rectSize = new Vector2(textureWidth, textureHeight);

        if (rectSize.y > resolution.y)
        {
            rectSize.y = resolution.y;
            rectSize.x = textureWidth * rectSize.y / textureHeight;
        }

        if (rectSize.x > resolution.x)
        {
            rectSize.x = resolution.x;
            rectSize.y = textureHeight * rectSize.x / textureWidth;
        }

        rectTransform.sizeDelta = rectSize;
    }
}