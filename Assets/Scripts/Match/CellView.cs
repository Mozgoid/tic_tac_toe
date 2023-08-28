using System;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer x;
    [SerializeField] private SpriteRenderer o;
    [SerializeField] private SpriteRenderer back;

    public Vector2 Size => back.sprite.bounds.extents * 2;

    public Action<Vector2Int> OnClick;

    public Vector2Int Position { get; set; }

    public void OnCustomizationChanged(Customization customization)
    {
        Customization.SwapSpriteAndKeepSize(x, customization.X);
        Customization.SwapSpriteAndKeepSize(o, customization.O);
    }

    public void SetSymbol(Board.Symbol symbol)
    {
        x.gameObject.SetActive(symbol == Board.Symbol.X);
        o.gameObject.SetActive(symbol == Board.Symbol.O);
    }

    private void OnMouseDown()
    {
        OnClick?.Invoke(Position);
    }
}
