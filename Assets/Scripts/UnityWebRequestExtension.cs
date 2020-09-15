using System;
using UnityEngine;
using UnityEngine.Networking;

public static class UnityWebRequestExtension
{
    private const string ParseWebRequestError = "Get object from web request error: {0}";

    public static string GetText(this UnityWebRequest unityWebRequest)
    {
        try
        {
            return unityWebRequest.downloadHandler.text;
        }
        catch (Exception exception)
        {
            Debug.LogError(string.Format(ParseWebRequestError, exception.Message));

            return string.Empty;
        }
    }

    public static Texture GetTexture(this UnityWebRequest unityWebRequest)
    {
        try
        {
            return ((DownloadHandlerTexture) unityWebRequest.downloadHandler).texture;
        }
        catch (Exception exception)
        {
            Debug.LogError(string.Format(ParseWebRequestError, exception.Message));

            return null;
        }
    }


    public static AssetBundle GetAssetBundle(this UnityWebRequest unityWebRequest)
    {
        try
        {
            return ((DownloadHandlerAssetBundle) unityWebRequest.downloadHandler).assetBundle;
        }
        catch (Exception exception)
        {
            Debug.LogError(string.Format(ParseWebRequestError, exception.Message));

            return null;
        }
    }
}