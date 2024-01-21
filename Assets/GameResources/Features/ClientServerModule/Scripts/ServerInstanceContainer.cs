namespace RequestTask.Feature.ClientServer
{
    using System;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Unity.VisualScripting;
    using UnityEngine;

    /// <summary>
    /// Контейнер для биндинга класса сервера и выдачи его при необходимости
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ServerInstanceContainer), menuName = "RequestTask/ClientServer/" + nameof(ServerInstanceContainer))]
    public sealed class ServerInstanceContainer : ScriptableObject
    {
        public event Action OnServerInitialized = delegate { };

        private BaseServer _server = default;
        /// <summary>
        /// Получение сервера
        /// </summary>
        public BaseServer Server => _server;

        private void Awake() =>
            hideFlags = HideFlags.DontUnloadUnusedAsset;

        public void BindServer(BaseServer server)
        {
            if (!(_server != null && _server == server))
            {
                _server = server;
                OnServerInitialized();
            }
        }
    }
}
