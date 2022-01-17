using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class CellSelector : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Color32 validColor;
    [SerializeField] private Color32 invalidColor;
    [SerializeField] private GameObject testprefab;
    
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;
    private GameManager _gameManager;
    private Vector3Int _currentCellPos;
    private PlayerInputs _playerInputs;
    private Vector3 _currentCellWorldPos;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mainCamera = Camera.main;

        _playerInputs = new PlayerInputs();
        _playerInputs.Player.Primary.Enable();
        _playerInputs.Player.Primary.performed += _ => PrimaryActionPressed();
        _playerInputs.Player.Secondary.Enable();
        _playerInputs.Player.Secondary.performed += _ => SecondaryActionPressed();
    }

    private void Update()
    {
        UpdatePosition();
        CheckCell();
    }
    
    private void PrimaryActionPressed()
    {
        AddContraction();
    }

    private void AddContraction()
    {
        var cell = _gameManager.World.GetCell(_currentCellPos);
        if (cell.IsEmpty)
        {
            cell.Create(testprefab, _currentCellWorldPos);
        }
    }

    private void SecondaryActionPressed()
    {
        var cell = _gameManager.World.GetCell(_currentCellPos);
        if (!cell.IsEmpty)
        {
            cell.Delete();
        }
    }

    private void UpdatePosition()
    {
        var newPos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        newPos.z = 0;
        _currentCellPos = grid.WorldToCell(newPos);
        newPos = grid.CellToWorld(_currentCellPos);
        newPos += grid.cellSize/2;
        _currentCellWorldPos = newPos; 
        transform.position = newPos;
    }
    
    private void CheckCell()
    {
        _spriteRenderer.color = _gameManager.World.GetCell(_currentCellPos).IsEmpty ? validColor : invalidColor;
    }
}
