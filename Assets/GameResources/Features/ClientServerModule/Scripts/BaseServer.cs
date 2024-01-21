namespace RequestTask.Feature.ClientServer
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Базовый класс сервера
    /// </summary>
    public class BaseServer: ScriptableObject
    {
        protected const int SUCCESS_REQUEST_STATUS_CODE = 200;

        public event Action OnServerStartRequest = delegate { };
        public event Action OnServerSuccessCompleteRequest = delegate { };
        public event Action OnServerErrorRequest = delegate { };
        public event Action OnServerStopRequest = delegate { };

        protected virtual void StartRequest()=>
            OnServerStartRequest();

        protected virtual void SuccessCompleteRequest() =>
            OnServerSuccessCompleteRequest();

        protected virtual void ErrorRequest() =>
            OnServerErrorRequest();

        public virtual void StopRequest() =>
            OnServerStopRequest();

    }
}
