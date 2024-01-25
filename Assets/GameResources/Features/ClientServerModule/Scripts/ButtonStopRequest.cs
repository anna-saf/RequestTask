namespace RequestTask.Feature.ClientServer
{
    using UnityDev.Extensions;
    using UnityEngine;

    /// <summary>
    ///  нопка, останавливающа€ запрос
    /// </summary>
    public sealed class ButtonStopRequest : AbstractButtonView
    {
        [SerializeField]
        private ServerInstanceContainer _serverContainer = default;

        protected override void OnButtonClicked() =>
            _serverContainer.Server.StopRequest();
    }
}
