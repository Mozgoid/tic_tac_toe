using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Reacts to changes in customization, changes background image
/// </summary>
public class BackgroundCustomization : MonoBehaviour
{
    [Zenject.Inject] private Customization customization;
    [SerializeField] private Image backgroud;

    private void Start()
    {
        customization.OnCustomizationChanged += OnCustomizationChanged;
        OnCustomizationChanged(customization);
    }

    private void OnDestroy()
    {
        customization.OnCustomizationChanged -= OnCustomizationChanged;
    }

    private void OnCustomizationChanged(Customization customization)
    {
        backgroud.sprite = customization.Background;
    }
}
