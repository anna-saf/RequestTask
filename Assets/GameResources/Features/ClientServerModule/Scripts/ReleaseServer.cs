namespace RequestTask.Feature.ClientServer
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// ����� ��������� �������
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ReleaseServer), menuName = "RequestTask/ClientServer/" + nameof(ReleaseServer))]
    public sealed class ReleaseServer : BaseServer
    {
        [SerializeField]
        private string _serverURL = default;
        [SerializeField]
        private string _getWeatherURLFormat = default;
        /// <summary>
        /// ��������� ��� ������� ��� ��������� ������
        /// </summary>
        public string GetWeatherURLFormat => _getWeatherURLFormat;

        private DownloadHandler _downloadHandler = default;
        /// <summary>
        ///��������� _downloadHandler
        /// </summary>
        public DownloadHandler DownloadHandler => _downloadHandler;

        private string _requestError = default;
        /// <summary>
        ///��������� _requestError
        /// </summary>
        public string RequestError => _requestError;

        private long _errorCode = default;
        /// <summary>
        /// ��������� _errorCode
        /// </summary>
        public long ErrorCode => _errorCode;    

        private CancellationTokenSource _cancellationTokenSource = default;
        private CancellationToken _cancellationToken = default;

        /// <summary>
        /// ����� �������� Get ������� �� ������
        /// </summary>
        /// <param name="getRequestUrl"></param>
        /// <param name="downloadHandler"></param>
        /// <param name="headers"></param>
        public async void SendGetRequest(string getRequestUrl, DownloadHandler downloadHandler, Dictionary<string,string> headers)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            using (UnityWebRequest request = UnityWebRequest.Get(_serverURL + getRequestUrl))
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
                        Debug.Log("�������� ��������");
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
