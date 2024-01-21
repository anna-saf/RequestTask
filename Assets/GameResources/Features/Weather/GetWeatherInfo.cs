namespace RequestTask.Feature.Weather
{
    using Newtonsoft.Json;
    using RequestTask.Feature.ClientServer;
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

        protected override void OnServerInitialized()
        {
            if (_serverInstanceContainer.Server.GetType() == typeof(ReleaseServer))
            {
                _releaseServer = (ReleaseServer)_serverInstanceContainer.Server;
                _server = _releaseServer;
                _server.OnServerStartRequest += OnServerStartRequest;
            }
        }

        public void GetWeather(float lat, float lon)
        {
            if (!_requestAlreadyBeen)
            {
                string requestURL = string.Format(_releaseServer.GetWeatherURLFormat, lat, lon, _apiKey);
                _releaseServer.SendGetRequest(requestURL, new DownloadHandlerBuffer(), new Dictionary<string, string>() { { CONTENT_TYPE, CONTENT_TYPE_VALUE } });
                _requestAlreadyBeen = true;
            }
        }

        protected override void OnServerSuccessCompleteRequest()
        {
            base.OnServerSuccessCompleteRequest();
            string responseStr = ((DownloadHandlerBuffer)_releaseServer.DownloadHandler).text;
            ResponseData temp = JsonConvert.DeserializeObject<ResponseData>(responseStr);
            Debug.Log(temp.main.temp);
            _requestAlreadyBeen = false;
        }
    }    
}
