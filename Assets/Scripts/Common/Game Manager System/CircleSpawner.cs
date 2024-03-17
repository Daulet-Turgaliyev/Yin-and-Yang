using System;
using Common.Circle;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Game_Manager_System
{
    public class CircleSpawner: ICircleSpawnService
    {
        private readonly IObjectResolver _objectResolver;

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
            
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 randomSpawnPos = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
                ACircleController aCircleControllerPrefab = circleType == CircleType.White ? _whiteCircleControllerPrefab : _blackCircleControllerPrefab;
                var circle = _objectResolver.Instantiate(aCircleControllerPrefab, randomSpawnPos, Quaternion.identity); 
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