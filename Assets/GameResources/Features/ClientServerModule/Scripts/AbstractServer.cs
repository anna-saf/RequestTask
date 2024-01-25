namespace RequestTask.Feature.ClientServer
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// Абстрактный класс сервера
    /// </summary>
    public abstract class AbstractServer: ScriptableObject, IDisposable
    {
        protected const int SUCCESS_REQUEST_STATUS_CODE = 200;

        public event Action OnServerStartRequest = delegate { };
        public event Action OnServerSuccessCompleteRequest = delegate { };
        public event Action OnServerErrorRequest = delegate { };
        public event Action OnServerStopRequest = delegate { };

        protected DownloadHandler _downloadHandler = default;
        /// <summary>
        ///Получение _downloadHandler
        /// </summary>
        public DownloadHandler DownloadHandler => _downloadHandler;

        protected string _requestError = default;
        /// <summary>
        ///Получение _requestError
        /// </summary>
        public string RequestError => _requestError;

        protected long _errorCode = default;
        /// <summary>
        /// Получение _errorCode
        /// </summary>
        public long ErrorCode => _errorCode;

        /// <summary>
        /// Метод инициализации сервера
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Get запрос на сервер
        /// </summary>
        /// <param name="urlParameters"></param>
        /// <param name="getRequestUrl"></param>
        /// <param name="downloadHandler"></param>
        /// <param name="headers"></param>
        public virtual void SendGetRequest(string urlParameters, string getRequestUrl, DownloadHandler downloadHandler, Dictionary<string, string> headers) =>
            StartRequest();

        /// <summary>
        /// Post запрос на сервер
        /// </summary>
        /// <param name="urlParameters"></param>
        /// <param name="parameters"></param>
        /// <param name="getRequestUrl"></param>
        /// <param name="downloadHandler"></param>
        /// <param name="headers"></param>
        public virtual void SendPostRequest(string urlParameters, string parameters, string getRequestUrl, DownloadHandler downloadHandler, Dictionary<string, string> headers) =>
            StartRequest();

        protected virtual void StartRequest()=>
            OnServerStartRequest();

        protected virtual void SuccessCompleteRequest() =>
            OnServerSuccessCompleteRequest();

        protected virtual void ErrorRequest() =>
            OnServerErrorRequest();

        public virtual void StopRequest() =>
            OnServerStopRequest();

        public virtual void UnsubscribeAllActions()
        {
            OnServerStartRequest = delegate { };
            OnServerSuccessCompleteRequest = delegate { };
            OnServerErrorRequest = delegate { };
            OnServerStopRequest= delegate { };
        }

        protected virtual void OnDispose() =>
            OnServerStopRequest();

        void IDisposable.Dispose() => OnDispose();
    }
}
