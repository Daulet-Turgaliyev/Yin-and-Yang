using Common.Circle;
using Common.Data;

namespace Common.Game_Manager_System
{
    public interface ICircleSpawnService
    {
        void Initialize(SoundPackPreset soundPackPreset);
        
        int WhiteCount { get; }
        int BlackCount { get; }
        
        CircleCounter CircleCounter { get; }
        void SpawnBalls(CircleType circleType, int spawnCount);
        void RemoveAllBalls(CircleType circleType);
    }
}