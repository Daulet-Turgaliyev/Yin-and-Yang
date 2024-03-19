using Common.Circle;

namespace Common.Containers.GameManagerServices
{
    public interface IGameManagerService
    {
        float CurrentSpeed { get; }
        void StartInitialize();
        void ChangeCircleSpeed(float newSpeed);
        void ChangeUpSpeed(float newSpeed);
        void ChangeDownSpeed(float newSpeed);
    }
}