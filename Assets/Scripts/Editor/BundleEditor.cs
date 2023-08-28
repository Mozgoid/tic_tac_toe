using UnityEditor;
using UnityEngine;

public class BundleEditor : EditorWindow
{
    string bundleName = "";
    private Sprite X, O, Background;

    
    [MenuItem("Tools/BundleEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BundleEditor));
    }
    
    void OnGUI()
    {
        bundleName = EditorGUILayout.TextField ("Bundle Name", bundleName);
        
        X = (Sprite)EditorGUILayout.ObjectField("X", X, typeof(Sprite), false);
        O = (Sprite)EditorGUILayout.ObjectField("O", O, typeof(Sprite), false);
        Background = (Sprite)EditorGUILayout.ObjectField("Background", Background, typeof(Sprite), false);

        if (GUILayout.Button("Create Bundle"))
        {
            CreateBundle();
        }
    }

    void CreateBundle()
    {
        if (string.IsNullOrEmpty(bundleName))
        {
            Debug.LogError("Bundle name is empty");
            return;
        }

        if (X == null || O == null || Background == null)
        {
            Debug.LogError("Sprites are empty");
            return;
        }

        var bundle = new AssetBundleBuild();
        bundle.assetBundleName = bundleName;

        bundle.assetNames = new string[]
        {
            AssetDatabase.GetAssetPath(X),
            AssetDatabase.GetAssetPath(O),
            AssetDatabase.GetAssetPath(Background)
        };

        bundle.addressableNames = new string[]
        {
            "X",
            "O",
            "Background"
        };

        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/", 
            new AssetBundleBuild[] {bundle}, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

    }
}
