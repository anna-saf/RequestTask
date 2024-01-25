namespace RequestTask.Feature.ClientServer
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// Класс релизного сервера
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ReleaseServer), menuName = "RequestTask/ClientServer/" + nameof(ReleaseServer))]
    public sealed class ReleaseServer : AbstractServer
    {
        [SerializeField]
        private string _testURL = default;
        [SerializeField]
        private string _prodURL = default;
        [SerializeField]
        private bool _isTest = default;

        [SerializeField]
        private string _getWeatherURL = default;
        /// <summary>
        /// Получение апи запроса для получения погоды
        /// </summary>
        public string GetWeatherURL => _getWeatherURL;
        [SerializeField]
        private string _getWeatherURParametersFormat = default;
        /// <summary>
        /// Получение формата строки с параметрами в url
        /// </summary>
        public string GetWeatherURParametersFormat => _getWeatherURParametersFormat;

        private string _targetURL = default;
 

        private CancellationTokenSource _cancellationTokenSource = default;
        private CancellationToken _cancellationToken = default;

        public override void Init() =>
            _targetURL = _isTest? _testURL : _prodURL;

        /// <summary>
        /// Метод отправки Get запроса на сервер
        /// </summary>
        /// <param name="getRequestUrl"></param>
        /// <param name="downloadHandler"></param>
        /// <param name="headers"></param>
        public override async void SendGetRequest(string urlParameters, string getRequestUrl, DownloadHandler downloadHandler, Dictionary<string,string> headers)
        {
            _targetURL = _testURL;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            using (UnityWebRequest request = UnityWebRequest.Get(_targetURL + getRequestUrl + urlParameters))
            {
                foreach(KeyValuePair<string,string> header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
                request.downloadHandler = downloadHandler;
                request.SendWebRequest();
                StartRequest();

                while (!request.isDone)
                { 
                    if (_cancellationToken.IsCancellationRequested) 
                    {
                        Debug.Log("Операция прервана");
                        _cancellationTokenSource.Dispose();
                        return;
                    }
                    await Task.Yield();
                }

                if(request.responseCode == SUCCESS_REQUEST_STATUS_CODE)
                {
                    _downloadHandler = request.downloadHandler;
                    SuccessCompleteRequest();
                }
                else
                {
                    _requestError = request.error;
                    _errorCode = request.responseCode;
                    ErrorRequest();
                }
            }
        }

        public override void StopRequest()
        {
            base.StopRequest();
            _cancellationTokenSource.Cancel();            
        }
    }
}
