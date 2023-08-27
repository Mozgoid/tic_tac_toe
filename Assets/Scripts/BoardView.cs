using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private CellView cellPrefab;

    private Board board;
    private Match match;

    private CellView[,] cells;

    [Zenject.Inject]
    public void Inject(Board board, Match match)
    {
        this.board = board;
        this.match = match;
    }

    private void Start()
    {
        board.OnSymbolChange += OnSymbolChange;
        SyncBoard();
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
    }

    private void OnSymbolChange(Board.Symbol symbol, Vector2Int pos)
    {
        cells[pos.x, pos.y].SetSymbol(symbol);
    }
}
