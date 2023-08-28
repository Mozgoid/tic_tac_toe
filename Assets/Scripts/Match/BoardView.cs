using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private CellView cellPrefab;

    private Board board;
    private Match match;
    private Customization customization;

    private CellView[,] cells;

    [Zenject.Inject]
    public void Inject(Board board, Match match, Customization customization)
    {
        this.board = board;
        this.match = match;
        this.customization = customization;
    }

    private void Start()
    {
        board.OnSymbolChange += OnSymbolChange;
        customization.OnCustomizationChanged += OnCustomizationChanged;
        SyncBoard();
        match.StartMatch();
    }

    private void OnDestroy()
    {
        board.OnSymbolChange -= OnSymbolChange;
        customization.OnCustomizationChanged -= OnCustomizationChanged;
    }

    private void OnCustomizationChanged(Customization customization)
    {
        foreach (var cell in cells)
        {
            cell.OnCustomizationChanged(customization);
        }
    }

    public void SyncBoard()
    {
        if (cells != null)
        {
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
        }

        cells = new CellView[board.Size, board.Size];

        var cellSize = cellPrefab.Size;
        var cellBottomLeftStart = new Vector3(-cellSize.x * board.Size / 2, -cellSize.y * board.Size / 2, 0); // board corner position
        cellBottomLeftStart += new Vector3(cellSize.x, cellSize.y) / 2.0f; // corner cell center position

        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                var cell = Instantiate(cellPrefab, transform);
                cell.transform.localPosition = cellBottomLeftStart + new Vector3(cellSize.x * i, cellSize.y * j, 0);
                cell.Position = new Vector2Int(i, j);
                cell.OnClick += match.OnCellClick;
                cells[i, j] = cell;
            }
        }

        OnCustomizationChanged(customization);
    }

    private void OnSymbolChange(Board.Symbol symbol, Vector2Int pos)
    {
        cells[pos.x, pos.y].SetSymbol(symbol);
    }
}
