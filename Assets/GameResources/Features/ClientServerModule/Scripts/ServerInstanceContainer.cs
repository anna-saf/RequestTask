namespace RequestTask.Feature.ClientServer
{
    using System;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Unity.VisualScripting;
    using UnityEngine;

    /// <summary>
    /// ��������� ��� �������� ������ ������� � ������ ��� ��� �������������
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ServerInstanceContainer), menuName = "RequestTask/ClientServer/" + nameof(ServerInstanceContainer))]
    public sealed class ServerInstanceContainer : ScriptableObject
    {
        public event Action OnServerInitialized = delegate { };
        public event Action OnServerChanged = delegate { };

        private AbstractServer _server = default;
        /// <summary>
        /// ��������� �������
        /// </summary>
        public AbstractServer Server => _server;

        private void Awake() =>
            hideFlags = HideFlags.DontUnloadUnusedAsset;

        public void BindServer(AbstractServer server)
        {
            if (_server == null)
            {
                _server = server;
                OnServerInitialized();
            }
            else if (_server != server)
            {
                _server = server;
                OnServerChanged();
            }
        }
    }
}
