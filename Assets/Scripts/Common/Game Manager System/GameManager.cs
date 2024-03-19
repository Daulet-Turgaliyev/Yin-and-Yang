using System.Threading.Tasks;
using Common.Circle;
using Common.Containers.GameManagerServices;
using UnityEngine;
using VContainer.Unity;

namespace Common.Game_Manager_System
{
    public sealed class GameManager : IGameManagerService
    {
        private readonly ICircleSpawnService _circleSpawner;

        public GameManager(ICircleSpawnService circleSpawner)
        {
            _circleSpawner = circleSpawner;
            CurrentSpeed = 25;
        }

        public float CurrentSpeed
        {
            get => _globalSpeed;
            set => _globalSpeed = Mathf.Clamp(value, 1f, 200);
        }

        private float _globalSpeed;

        public async void StartInitialize()
        {
            await Task.Delay(1000);
            
            _circleSpawner.SpawnBalls(CircleType.Black, 1);
            _circleSpawner.SpawnBalls(CircleType.White, 1);
        }

        public void ChangeCircleSpeed(float newSpeed)
        {
            CurrentSpeed = newSpeed;
            
            foreach (var circle in _circleSpawner.CircleCounter.WhiteCirclesOnScene)
                circle.ChangeSpeed(CurrentSpeed);
            
            foreach (var circle in _circleSpawner.CircleCounter.BlackCirclesOnScene)
                circle.ChangeSpeed(CurrentSpeed);
        }

        public void ChangeUpSpeed(float newSpeed)
        {
            CurrentSpeed += newSpeed;
            ChangeCircleSpeed(CurrentSpeed);
        }

        public void ChangeDownSpeed(float newSpeed)
        {
            CurrentSpeed -= newSpeed;
            ChangeCircleSpeed(CurrentSpeed);
        }
    }
}
