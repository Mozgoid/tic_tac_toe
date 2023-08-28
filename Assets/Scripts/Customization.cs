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

    public static void SwapSpriteAndKeepSize(SpriteRenderer spriteRenderer, Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.Log("new sprite is null");
            return;
        }
        if (spriteRenderer == null)
        {
            Debug.Log("sprite renderer is null");
            return;
        }
        if (spriteRenderer.sprite == null)
        {
            Debug.Log("old sprite is null");
            spriteRenderer.sprite = newSprite;
            return;
        }
        
        var oldSprite = spriteRenderer.sprite;
        // Debug.Log($"old {oldSprite.name} size: {oldSprite.rect.size} {oldSprite.pixelsPerUnit} {oldSprite.rect.width} {oldSprite.rect.height}");
        // Debug.Log($"new {newSprite.name} size: {newSprite.rect.size} {newSprite.pixelsPerUnit} {newSprite.rect.width} {newSprite.rect.height}");

        var oldSize = spriteRenderer.sprite.rect.size;
        var newSize = newSprite.rect.size;

        var scale = new Vector2(oldSize.x / newSize.x, oldSize.y / newSize.y);
        spriteRenderer.sprite = newSprite;

        var oldLocalScale = spriteRenderer.transform.localScale;
        spriteRenderer.transform.localScale = new Vector3(oldLocalScale.x * scale.x, oldLocalScale.y * scale.y, oldLocalScale.z);
    }
}
