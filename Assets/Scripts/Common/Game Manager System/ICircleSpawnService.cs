using Common.Circle;

namespace Common.Game_Manager_System
{
    public interface ICircleSpawnService
    {
        CircleCounter CircleCounter { get; }
        void SpawnBalls(CircleType circleType, int spawnCount);
        void RemoveAllBalls(CircleType circleType);
    }
}