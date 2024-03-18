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
        }

        public float CurrentSpeed { get; set; }

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
                circle.ChangeSpeed(newSpeed);
            
            foreach (var circle in _circleSpawner.CircleCounter.BlackCirclesOnScene)
                circle.ChangeSpeed(newSpeed);
        }

        public void ChangeUpSpeed(float newSpeed)
        {
            CurrentSpeed += newSpeed;
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, 1f, 450f);

            ChangeCircleSpeed(CurrentSpeed);
        }

        public void ChangeDownSpeed(float newSpeed)
        {
            CurrentSpeed -= newSpeed;
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, 1f, 450f);
            
            ChangeCircleSpeed(CurrentSpeed);
        }
    }
}
