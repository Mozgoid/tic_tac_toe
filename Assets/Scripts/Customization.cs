using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    public Sprite X {get; private set;}
    public Sprite O {get; private set;}
    public Sprite Background {get; private set;}

    private AssetBundle currentBundleInUse;

    public Action<Customization> OnCustomizationChanged;

    const string PrefsSaveKey = "preferredBundle";

    private void Start()
    {
        var preferredBundle = PlayerPrefs.GetString(PrefsSaveKey, "default");
        SwapAssets(preferredBundle);
    }

    
    public void SwapAssets(string newBundleName) 
    {
        StartCoroutine(SwapAssetsAsync(newBundleName));
    }

    private IEnumerator SwapAssetsAsync(string newBundleName) 
    {
        if (newBundleName == currentBundleInUse?.name)
        {
            Debug.Log($"Bundle {newBundleName} is already in use");
            yield break;
        }

        var newAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, newBundleName));
        if (newAssetBundle == null) 
        {
            Debug.LogError($"Failed to load AssetBundle {newBundleName}!");
            yield break;
        }
        var oldBundle = currentBundleInUse;
        currentBundleInUse = newAssetBundle;

        var xRequest = newAssetBundle.LoadAssetAsync<Sprite>("X");
        var oRequest = newAssetBundle.LoadAssetAsync<Sprite>("O");
        var backgroundRequest = newAssetBundle.LoadAssetAsync<Sprite>("Background");

        yield return xRequest;
        yield return oRequest;
        yield return backgroundRequest;

        X = xRequest.asset as Sprite;
        O = oRequest.asset as Sprite;
        Background = backgroundRequest.asset as Sprite;

        PlayerPrefs.SetString(PrefsSaveKey, newBundleName);
        PlayerPrefs.Save();

        OnCustomizationChanged?.Invoke(this);
        if (oldBundle != null) 
        {
            oldBundle.Unload(true);
        }

        Debug.Log($"Swapped to bundle {newBundleName}");
    }
}
