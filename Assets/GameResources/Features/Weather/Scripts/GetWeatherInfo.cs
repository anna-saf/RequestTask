namespace RequestTask.Feature.Weather
{
    using Newtonsoft.Json;
    using RequestTask.Feature.ClientServer;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// Получение информации о погоде по ширине и долготе
    /// </summary>
    public sealed class GetWeatherInfo : AbstractServerProvider
    {
        public const string CONTENT_TYPE = "Content-Type";
        public const string CONTENT_TYPE_VALUE = "application/json";
        [SerializeField]
        private string _apiKey = default;

        private bool _requestAlreadyBeen = false;

        private ReleaseServer _releaseServer = default;

        protected override void OnServerInitSuccess()
        {
            if (_serverInstanceContainer.Server.GetType() == typeof(ReleaseServer))
            {
                _releaseServer = (ReleaseServer)_serverInstanceContainer.Server;
                _server = _releaseServer;
                _server.OnServerStartRequest += OnServerStartRequest;
            }
        }

        /// <summary>
        /// Запросить информацию о погоде
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        public void GetWeather(float lat, float lon)
        {
            if (!_requestAlreadyBeen)
            {
                string requestURL = string.Format(_releaseServer.GetWeatherURParametersFormat, lat, lon, _apiKey);
                _releaseServer.SendGetRequest(requestURL, _releaseServer.GetWeatherURL, new DownloadHandlerBuffer(), new Dictionary<string, string>() { { CONTENT_TYPE, CONTENT_TYPE_VALUE } });
                _requestAlreadyBeen = true;
            }
        }

        protected override void OnServerSuccessCompleteRequest()
        {
            base.OnServerSuccessCompleteRequest();
            _requestAlreadyBeen = false;
            string responseStr = ((DownloadHandlerBuffer)_releaseServer.DownloadHandler).text;
            ResponseData temp = JsonConvert.DeserializeObject<ResponseData>(responseStr);
            Debug.Log(temp.main.temp);
        }

        protected override void OnServerErrorRequest()
        {
            base.OnServerErrorRequest();
            _requestAlreadyBeen = false;
            Debug.Log("Произошла ошибка при выполнении запроса: " + _serverInstanceContainer.Server.RequestError + Environment.NewLine + "Код ошибки: " + _serverInstanceContainer.Server.ErrorCode);
        }

        protected override void OnServerStopRequest()
        {
            base.OnServerStopRequest();
            _requestAlreadyBeen = false;
            Debug.Log("Запрос был прерван.");
        }
    }    
}
