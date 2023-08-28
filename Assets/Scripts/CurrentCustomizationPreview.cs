using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentCustomizationPreview : MonoBehaviour
{
    [SerializeField] private Image X, O, Background;

    [Zenject.Inject]
    private Customization customization;

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
        X.sprite = customization.X;
        O.sprite = customization.O;
        Background.sprite = customization.Background;
    }

}
