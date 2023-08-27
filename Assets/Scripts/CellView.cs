using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer x;
    [SerializeField] private SpriteRenderer o;

    public Action<Vector2Int> OnClick;

    public Vector2Int Position { get; set; }

    public void SetSymbol(Board.Symbol symbol)
    {
        x.gameObject.SetActive(symbol == Board.Symbol.X);
        o.gameObject.SetActive(symbol == Board.Symbol.O);
    }

    private void OnMouseDown()
    {
        Debug.Log($"Sprite Clicked {Position}");
        OnClick?.Invoke(Position);
    }

}
