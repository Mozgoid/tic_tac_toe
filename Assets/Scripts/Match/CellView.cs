using System;
using DG.Tweening;
using UnityEngine;


/// <summary>
/// View for cell.
/// Reacts to clicks, and notifies about them.
/// Changes appearance according to customization and state
/// </summary>
public class CellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer x;
    [SerializeField] private SpriteRenderer o;
    [SerializeField] private SpriteRenderer back;
    
    [SerializeField] private SpriteRenderer hint;

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

    public void OnHint()
    {
        hint.DOColor(Color.white, 0.5f).SetEase(Ease.OutBounce).SetLoops(2, LoopType.Yoyo);
    }
}
