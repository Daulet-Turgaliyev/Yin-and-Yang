using Common.Cell_System;
using Common.Containers.GameManagerServices;
using Common.Data;
using Common.Sounds;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Common.Game_Manager_System
{
    public class GridSpawner : MonoBehaviour, ICellSpawnerService
    {
        private Cell _cellPrefab; 
        
        private ISoundManagerService _soundManagerService;
        
        private SpawnDirection _spawnDirection;
        private Vector2 _cellSize;
        private Vector2 _spacing;
        
        [SerializeField] private GameObject _wallPrefab;

        private SoundPackPreset _soundPackPreset;
        
        [Inject]
        public void Construct(ISoundManagerService soundManagerService)
        {
            _soundManagerService = soundManagerService;
        }
        
        public void Initialize(SoundPackPreset soundPackPreset)
        {
            _soundPackPreset = soundPackPreset;
            
            _spawnDirection = soundPackPreset.SpawnDirection;
            _cellSize = soundPackPreset.CellSize;
            _spacing = soundPackPreset.CellSpacing;

            _cellPrefab = soundPackPreset.CellPrefab;
            
            SpawnSquares();
        }

        private void SpawnSquares()
        {
            Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

            int squaresX = Mathf.CeilToInt((topRight.x - bottomLeft.x) / (_cellSize.x + _spacing.x));
            int squaresY = Mathf.CeilToInt((topRight.y - bottomLeft.y) / (_cellSize.y + _spacing.y));

    
            // Расчет и спавн стен вдоль границ камеры
            CreateWall(new Vector2(bottomLeft.x, (bottomLeft.y + topRight.y) / 2), new Vector2(.5f, topRight.y - bottomLeft.y)); // Левая стена
            CreateWall(new Vector2(topRight.x, (bottomLeft.y + topRight.y) / 2), new Vector2(.5f, topRight.y - bottomLeft.y)); // Правая стена
            CreateWall(new Vector2((bottomLeft.x + topRight.x) / 2, bottomLeft.y), new Vector2(topRight.x - bottomLeft.x, .5f)); // Нижняя стена
            CreateWall(new Vector2((bottomLeft.x + topRight.x) / 2, topRight.y), new Vector2(topRight.x - bottomLeft.x, .5f)); // Верхняя стена


            int orderInLayer = 0;
            for (int y = 0; y < squaresY; y++)
            {
                if (_spawnDirection == SpawnDirection.LeftToRight) orderInLayer--;
                if (_spawnDirection == SpawnDirection.RightToLeft) orderInLayer++;
                
                for (int x = 0; x < squaresX; x++)
                {
                    Vector2 spawnPosition = new Vector2(bottomLeft.x + (x * (_cellSize.x + _spacing.x)) + _cellSize.x / 2,
                        bottomLeft.y + (y * (_cellSize.y + _spacing.y)) + _cellSize.y / 2);

                    Cell cell = Instantiate(_cellPrefab, spawnPosition, Quaternion.identity);
                    cell.transform.localScale = Vector3.one * _cellSize; 
                    if (spawnPosition.x < 0)
                    {
                        cell.Initialize(_soundPackPreset.GetRandomWhiteCellSkin(), _soundPackPreset.GetRandomBlackCellSkin(), orderInLayer); // Измените y на squaresY - 1 - y, если хотите сохранить исходное поведение
                        cell.SetCellState(CellState.White);
                    }
                    else
                    {
                        cell.Initialize(_soundPackPreset.GetRandomWhiteCellSkin(), _soundPackPreset.GetRandomBlackCellSkin(), orderInLayer); // Аналогично измените y здесь
                        cell.SetCellState(CellState.Black);
                    }
                    cell.transform.SetParent(transform);
                    cell.cellState.OnChanged += _soundManagerService.PlayRandomSound;
                }
            }
            
            transform.position = new Vector3(transform.position.x, _spacing.y);
        }
        
        
        private void CreateWall(Vector2 position, Vector2 size)
        {
            GameObject wall = Instantiate(_wallPrefab, position, Quaternion.identity);
            wall.transform.localScale = new Vector3(size.x, size.y, 1); // Задайте масштаб стены в соответствии с переданным размером
        }
    }
}
