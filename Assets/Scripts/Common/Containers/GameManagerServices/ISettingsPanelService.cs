namespace Common.Containers.GameManagerServices
{
    public interface ISettingsPanelService
    {
        void StartInitialize();
        bool IsNeedVibration { get; set; }
    }
}