namespace RequestTask.Feature.ClientServer
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// �����, � ������� ������������ ���������� �������
    /// </summary>
    public sealed class ServerInitializer : MonoBehaviour
    {
        [SerializeField]
        private ServerInstanceContainer _serverInstanceContainer = default;
        [SerializeField]
        private BaseServer _server = default;

        private void Awake() =>
            _serverInstanceContainer.BindServer(_server);
    }
}

