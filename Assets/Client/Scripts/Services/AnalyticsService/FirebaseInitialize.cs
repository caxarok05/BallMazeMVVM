using Firebase;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Services
{
    public class FirebaseInitialize : IInitializable
    {
        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
        }

        private void OnDependencyStatusReceived(Task<DependencyStatus> task)
        {
            try
            {
                if (!task.IsCompletedSuccessfully)
                    throw new Exception("Could not resolve all Firebase dependencies", task.Exception);

                var status = task.Result;
                if (status != DependencyStatus.Available)
                    throw new Exception($"Could not resolve all Firebase dependencies: {status}");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

        }
    }
}