using UnityEngine;
using UnityEngine.UI;

public class TextureLoader : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private InputField inputField;
    [SerializeField] private Button button;
    [SerializeField] private CanvasScaler canvasScaler;

    private WebUtils _webUtils;

    private void Start()
    {
        button.onClick.AddListener(OnPressBtn);
    }

    private void OnPressBtn()
    {
        if (_webUtils == null)
            _webUtils = gameObject.AddComponent<WebUtils>();

        _webUtils.Load<Texture>(
            inputField.text,
            OnResponse,
            OnError
        );
    }

    private void OnError(string error)
    {
        Debug.LogError(error);
    }

    private void OnResponse(Texture texture)
    {
        var sprite = Sprite.Create((Texture2D) texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));

        image.sprite = sprite;
        RectTransformer.SetImageSize(image.rectTransform, texture.width, texture.height, canvasScaler.referenceResolution);
    }
}