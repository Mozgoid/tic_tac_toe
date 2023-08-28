using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    [SerializeField] Image X, O, Background;

    void Start() 
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "moon bundle"));
        if (myLoadedAssetBundle == null) 
        {
            Debug.LogError("Failed to load AssetBundle!");
            return;
        }

        X.sprite = myLoadedAssetBundle.LoadAsset<Sprite>("X");;
        O.sprite = myLoadedAssetBundle.LoadAsset<Sprite>("O");;
        Background.sprite = myLoadedAssetBundle.LoadAsset<Sprite>("Background");;
    }

}
