using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebUtils : MonoBehaviour
{
    private static readonly Dictionary<Type, Func<string, UnityWebRequest>> FabricRequests =
        new Dictionary<Type, Func<string, UnityWebRequest>>
        {
            {
                typeof(string),
                (url) => new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, new DownloadHandlerBuffer(), null)
            },
            {
                typeof(Texture), UnityWebRequestTexture.GetTexture
            },
            {
                typeof(AssetBundle), UnityWebRequestAssetBundle.GetAssetBundle
            },
        };

    private static readonly Dictionary<Type, Func<UnityWebRequest, object>> FabricResponses =
        new Dictionary<Type, Func<UnityWebRequest, object>>
        {
            {
                typeof(string), (unityWebRequest) => (object) unityWebRequest.GetText()
            },
            {
                typeof(Texture),
                (unityWebRequest) => (object) unityWebRequest.GetTexture()
            },
            {
                typeof(AssetBundle),
                (unityWebRequest) => unityWebRequest.GetAssetBundle()
            },
        };


    private static UnityWebRequest CreateRequest<T>(string url)
    {
        if (FabricRequests.ContainsKey(typeof(T)))
        {
            return FabricRequests[typeof(T)].Invoke(url);
        }

        return UnityWebRequest.Get(url);
    }

    private static T CreateResponse<T>(UnityWebRequest unityWebRequest)
    {
        if (FabricResponses.ContainsKey(typeof(T)))
        {
            return (T) FabricResponses[typeof(T)].Invoke(unityWebRequest);
        }

        Debug.LogError("This file type cannot be downloaded");
        return default;
    }

    public void Load<T>(string url, Action<T> onResponse, Action<string> onError)
    {
        Debug.Log("Start Loading");
        StartCoroutine(LoadCoroutine(url, onResponse, onError));
    }

    private static IEnumerator LoadCoroutine<T>(string url, Action<T> onResponse, Action<string> onError)
    {
        var webRequest = CreateRequest<T>(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            onError?.Invoke(webRequest.error);
        }
        else
        {
            var result = CreateResponse<T>(webRequest);
            onResponse?.Invoke(result);

            Debug.Log("Loading finished");
        }
    }
}