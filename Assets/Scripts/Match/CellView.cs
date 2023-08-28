using System;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer x;
    [SerializeField] private SpriteRenderer o;
    [SerializeField] private SpriteRenderer back;

    [SerializeField] private Animator animator;

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
        animator.SetInteger("Symbol", (int)symbol);
    }

    private void OnMouseDown()
    {
        OnClick?.Invoke(Position);
    }

    public void OnWin()
    {
        animator.SetTrigger("Win");
    }

    public void OnHint()
    {
        animator.SetTrigger("Hint");
    }
}
