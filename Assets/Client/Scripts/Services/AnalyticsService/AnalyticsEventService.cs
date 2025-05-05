using Firebase;
using Firebase.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Scripts.Services
{
    public class AnalyticsEventService : IAnalyticsEventService
    {
        public void LogDeath()
        {
            FirebaseAnalytics.LogEvent("Player death", new Parameter("death", 1));
        }

        public void LogLevelCompleted(string levelName)
        {
            FirebaseAnalytics.LogEvent($"{levelName} completed", new Parameter("level", 1));
        }
    }
}
