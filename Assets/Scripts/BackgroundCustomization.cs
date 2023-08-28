﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCustomization : MonoBehaviour
{
    [Zenject.Inject] private Customization customization;
    [SerializeField] private Image backgroud;

    private void Start()
    {
        customization.OnCustomizationChanged += OnCustomizationChanged;
        OnCustomizationChanged(customization);
    }

    private void OnCustomizationChanged(Customization customization)
    {
        backgroud.sprite = customization.Background;
    }
}