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
        [SerializeField]
        private Cell _cellPrefab; 
        
        private ISoundManagerService _soundManagerService;
        
        private float _cellSize;
        
        [SerializeField] private CellCounter _cellCounter;

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
            _cellSize = soundPackPreset.CellSize;
            SpawnSquares();
        }

        private void SpawnSquares()
        {
            Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

            int squaresX = Mathf.CeilToInt((topRight.x - bottomLeft.x) / _cellSize);
            int squaresY = Mathf.CeilToInt((topRight.y - bottomLeft.y) / _cellSize);

            // Толщина стен устанавливается в значение _cellSize для удобства, но вы можете выбрать другое значение при необходимости
            float thickness = _cellSize / 6;
    
            // Расчет и спавн стен вдоль границ камеры
            CreateWall(new Vector2(bottomLeft.x, (bottomLeft.y + topRight.y) / 2), new Vector2(thickness, topRight.y - bottomLeft.y)); // Левая стена
            CreateWall(new Vector2(topRight.x, (bottomLeft.y + topRight.y) / 2), new Vector2(thickness, topRight.y - bottomLeft.y)); // Правая стена
            CreateWall(new Vector2((bottomLeft.x + topRight.x) / 2, bottomLeft.y), new Vector2(topRight.x - bottomLeft.x, thickness)); // Нижняя стена
            CreateWall(new Vector2((bottomLeft.x + topRight.x) / 2, topRight.y), new Vector2(topRight.x - bottomLeft.x, thickness)); // Верхняя стена

            for (int y = squaresY - 1; y >= 0; y--) // Измените условие цикла, чтобы y уменьшался
            {
                for (int x = 0; x < squaresX; x++)
                {
                    Vector2 spawnPosition = new Vector2(bottomLeft.x + (x * _cellSize) + _cellSize / 2,
                        bottomLeft.y + (y * _cellSize) + _cellSize / 2);

                    Cell cell = Instantiate(_cellPrefab, spawnPosition, Quaternion.identity);
                    cell.transform.localScale = Vector3.one * _cellSize; 
                    if (spawnPosition.x < 0)
                    {
                        _cellCounter.AddWhite();
                        cell.Initialize(_soundPackPreset.GetRandomWhiteCellSkin(), _soundPackPreset.GetRandomBlackCellSkin(), squaresY - 1 - y); // Измените y на squaresY - 1 - y, если хотите сохранить исходное поведение
                        cell.SetCellState(CellState.White, _cellCounter);
                    }
                    else
                    {
                        _cellCounter.AddBlack();
                        cell.Initialize(_soundPackPreset.GetRandomWhiteCellSkin(), _soundPackPreset.GetRandomBlackCellSkin(), squaresY - 1 - y); // Аналогично измените y здесь
                        cell.SetCellState(CellState.Black, _cellCounter);
                    }
                    cell.transform.SetParent(transform);
                    cell.cellState.OnChanged += _soundManagerService.PlayRandomSound;
                }
            }

        }
        
        private void CreateWall(Vector2 position, Vector2 size)
        {
            GameObject wall = Instantiate(_wallPrefab, position, Quaternion.identity);
            wall.transform.localScale = new Vector3(size.x, size.y, 1); // Задайте масштаб стены в соответствии с переданным размером
        }
    }
}
