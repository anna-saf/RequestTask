namespace RequestTask.Feature.ClientServer
{
    using UnityEngine;

    /// <summary>
    /// Абстрактный провайдер сервера
    /// </summary>
    public abstract class AbstractServerProvider : MonoBehaviour
    {
        [SerializeField, Space()]
        protected ServerInstanceContainer _serverInstanceContainer = default;

        protected AbstractServer _server = default;

        protected virtual void Awake()
        {
            if (_serverInstanceContainer.Server != null)
            {
                OnServerInitialized();
            }
            else
            {
                _serverInstanceContainer.OnServerInitialized += OnServerInitialized;
            }
        }

        protected abstract void OnServerInitialized();

        protected virtual void OnServerStartRequest()
        {
            _server.OnServerStopRequest += OnServerStopRequest;
            _server.OnServerErrorRequest += OnServerErrorRequest;
            _server.OnServerSuccessCompleteRequest += OnServerSuccessCompleteRequest;
        }

        protected virtual void OnServerSuccessCompleteRequest() =>
            RequestEnd();

        protected virtual void OnServerErrorRequest() =>
            RequestEnd();

        protected virtual void OnServerStopRequest() =>
            RequestEnd();

        protected virtual void RequestEnd()
        {
            _server.OnServerStopRequest -= OnServerStopRequest;
            _server.OnServerErrorRequest -= OnServerErrorRequest;
            _server.OnServerSuccessCompleteRequest -= OnServerSuccessCompleteRequest;
        }

        protected virtual void OnDestroy()
        {
            if(_serverInstanceContainer != null)
            {
                _serverInstanceContainer.OnServerInitialized -= OnServerInitialized;
            }
            if (_server != null)
            {
                _server.OnServerStartRequest -= OnServerStartRequest;
                RequestEnd();
            }
        }
    }
}
