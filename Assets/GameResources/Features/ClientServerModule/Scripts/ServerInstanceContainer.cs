namespace RequestTask.Feature.ClientServer
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Контейнер для биндинга класса сервера и выдачи его при необходимости
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ServerInstanceContainer), menuName = "RequestTask/ClientServer/" + nameof(ServerInstanceContainer))]
    public sealed class ServerInstanceContainer : ScriptableObject
    {
        public event Action OnServerInitialized = delegate { };
        public event Action OnServerChanged = delegate { };

        private AbstractServer _server = default;
        /// <summary>
        /// Получение сервера
        /// </summary>
        public AbstractServer Server => _server;

        private void Awake() =>
            hideFlags = HideFlags.DontUnloadUnusedAsset;

        /// <summary>
        /// Метод биндинга сервера
        /// </summary>
        /// <param name="server"></param>
        public void BindServer(AbstractServer server)
        {
            if (_server == null)
            {
                _server = server;
                _server.Init();
                OnServerInitialized();
            }
            else if (_server != server)
            {
                _server.UnsubscribeAllActions();
                _server = server;
                _server.Init();
                OnServerChanged();
            }
        }
    }
}
