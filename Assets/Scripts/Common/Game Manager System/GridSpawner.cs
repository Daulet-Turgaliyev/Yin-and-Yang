using Common.Cell_System;
using Common.Containers.GameManagerServices;
using Common.Sounds;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Common.Game_Manager_System
{
    public class GridSpawner : MonoBehaviour
    {
        [SerializeField]
        private Cell _cellPrefab; 
        
        private ISoundManagerService _soundManagerService;

        private readonly float _cellSize = 1f;

        [SerializeField] private CellCounter _cellCounter;

        [SerializeField] private GameObject _wallPrefab;
        
        
        [Inject]
        public void Construct(ISoundManagerService soundManagerService, IGameManagerService gameManagerService)
        {
            _soundManagerService = soundManagerService;
            
            SpawnSquares();
        }

        private void SpawnSquares()
        {
            Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

            int squaresX = Mathf.CeilToInt((topRight.x - bottomLeft.x) / _cellSize);
            int squaresY = Mathf.CeilToInt((topRight.y - bottomLeft.y) / _cellSize);

            float thickness = _cellSize;
            
            CreateWall(new Vector2(bottomLeft.x - thickness, (bottomLeft.y + topRight.y) / 2), new Vector2(thickness, topRight.y - bottomLeft.y + thickness * 2)); // Левая стена
            CreateWall(new Vector2(topRight.x + thickness, (bottomLeft.y + topRight.y) / 2), new Vector2(thickness, topRight.y - bottomLeft.y + thickness * 2)); // Правая стена
            CreateWall(new Vector2((bottomLeft.x + topRight.x) / 2, bottomLeft.y - thickness), new Vector2(topRight.x - bottomLeft.x + thickness * 2, thickness)); // Нижняя стена
            CreateWall(new Vector2((bottomLeft.x + topRight.x) / 2, topRight.y + thickness), new Vector2(topRight.x - bottomLeft.x + thickness * 2, thickness)); // Верхняя стена

        

            for (int y = 0; y < squaresY; y++)
            {
                for (int x = 0; x < squaresX; x++)
                {
                    Vector2 spawnPosition = new Vector2(bottomLeft.x + (x * _cellSize) + _cellSize / 2,
                        bottomLeft.y + (y * _cellSize) + _cellSize / 2);
                
                    Cell cell = Instantiate(_cellPrefab, spawnPosition, Quaternion.identity);
                    if (spawnPosition.x < 0)
                    {
                        _cellCounter.AddWhite();
                        cell.SetCellState(CellState.White, _cellCounter);
                    }
                    else
                    {
                        _cellCounter.AddBlack();
                        cell.SetCellState(CellState.Black, _cellCounter);
                    }
                    cell.transform.SetParent(transform);
                    cell.CellState.OnChanged += _soundManagerService.PlayRandomSound;
                }
            }
        }
        
        private void CreateWall(Vector2 position, Vector2 size)
        {
            GameObject wall = Instantiate(_wallPrefab, position, Quaternion.identity);
            wall.transform.localScale = new Vector3(size.x / _cellSize, size.y / _cellSize, 1);
        }
    }
}
