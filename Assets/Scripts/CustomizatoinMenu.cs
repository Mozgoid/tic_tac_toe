using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizatoinMenu : MonoBehaviour
{
    [Zenject.Inject] private Customization customization;

    [SerializeField] private TMPro.TMP_InputField inputField;

    public void ChangeCustomization(string bundleName)
    {
        customization.SwapAssets(bundleName);
    }

    public void ChangeToCustom()
    {
        customization.SwapAssets(inputField.text);
    }
}
