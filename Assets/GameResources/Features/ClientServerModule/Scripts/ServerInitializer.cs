namespace RequestTask.Feature.ClientServer
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Класс, в котором определяется реализация сервера
    /// </summary>
    public sealed class ServerInitializer : MonoBehaviour
    {
        public event Action OnBindServer = delegate { };
        [SerializeField]
        private ServerInstanceContainer _serverInstanceContainer = default;
        [SerializeField]
        private AbstractServer _server = default;

        private void Start()
        {
            if (_server != null)
            {
                if (_serverInstanceContainer.Server != null && _serverInstanceContainer.Server == _server)
                {
                    _server.Init();
                }
                else
                {
                    _serverInstanceContainer.BindServer(_server);
                    OnBindServer();
                }
            }
        }
    }
}

