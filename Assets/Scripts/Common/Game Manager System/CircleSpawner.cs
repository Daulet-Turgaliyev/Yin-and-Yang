using System;
using Common.Circle;
using Common.Data;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Game_Manager_System
{
    public class CircleSpawner: ICircleSpawnService
    {
        private readonly IObjectResolver _objectResolver;
        private SoundPackPreset _soundPackPreset;
        private Sprite _whiteSprite;
        private Sprite _blackSprite;
        
        public void Initialize(SoundPackPreset soundPackPreset)
        {
            _soundPackPreset = soundPackPreset;
            _whiteSprite = soundPackPreset.WhiteCircleSkin;
            _blackSprite = soundPackPreset.BlackCircleSkin;
        }

        public int WhiteCount => _whiteCount;
        private int _whiteCount;
        public int BlackCount => _blackCount;
        private int _blackCount;

        public CircleCounter CircleCounter { get; }

        private readonly WhiteCircleController _whiteCircleControllerPrefab;
        private readonly BlackCircleController _blackCircleControllerPrefab;
        
        public CircleSpawner(IObjectResolver objectResolver, 
            WhiteCircleController whiteCircleControllerPrefab, 
            BlackCircleController blackCircleControllerPrefab)
        {
            _objectResolver = objectResolver;
            _whiteCircleControllerPrefab = whiteCircleControllerPrefab;
            _blackCircleControllerPrefab = blackCircleControllerPrefab;
            
            CircleCounter = new CircleCounter();
        }
    
        public void SpawnBalls(CircleType circleType, int spawnCount)
        {
            RemoveAllBalls(circleType);

            if (circleType == CircleType.White)
            {
                _whiteCount = spawnCount;
            }
            else
            {
                _blackCount = spawnCount;
            }
            
            for (int i = 0; i < spawnCount; i++)
            {
                var randomSpawnPos = circleType == CircleType.White ?
                    new Vector3(Random.Range(-5, -1), Random.Range(-3, 3)) : 
                    new Vector3(Random.Range(1, 5), Random.Range(-3, 3));
                
                
                ACircleController aCircleControllerPrefab = circleType == CircleType.White ? _whiteCircleControllerPrefab : _blackCircleControllerPrefab;
                var circle = _objectResolver.Instantiate(aCircleControllerPrefab, randomSpawnPos, Quaternion.identity);
                var skinSprite = circleType == CircleType.White ? _whiteSprite : _blackSprite;
                var trailRendererPrefab = circleType == CircleType.White ? _soundPackPreset.WhiteTrailPrefabs : _soundPackPreset.BlackTrailPrefabs;
                circle.Initialize(_soundPackPreset, skinSprite, trailRendererPrefab);
                circle.transform.localScale = Vector3.one * _soundPackPreset.CircleSize;
                CircleCounter.Add(circle);
            }
        }

        public void RemoveAllBalls(CircleType circleType)
        {
            switch (circleType)
            {
                case CircleType.White:
                {
                    foreach (var circle in CircleCounter.WhiteCirclesOnScene)
                        UnityEngine.Object.Destroy(circle.gameObject);
                
                    CircleCounter.Clear(CircleType.White);
                    break;
                }
                case CircleType.Black:
                {
                    foreach (var circle in CircleCounter.BlackCirclesOnScene)
                        UnityEngine.Object.Destroy(circle.gameObject);
                
                    CircleCounter.Clear(CircleType.Black);
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException(nameof(circleType), circleType, null);
            }
        }
    }
}