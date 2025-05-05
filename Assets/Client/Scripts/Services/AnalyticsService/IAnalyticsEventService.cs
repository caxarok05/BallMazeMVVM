namespace Client.Scripts.Services
{
    public interface IAnalyticsEventService
    {
        void LogDeath();
        void LogLevelCompleted(string levelName);
    }
}