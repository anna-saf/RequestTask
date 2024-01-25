namespace RequestTask.Feature.Weather
{
    using UnityDev.Extensions;
    using UnityEngine;

    /// <summary>
    ///  нопка отправки запроса дл€ получени€ погоды
    /// </summary>
    public sealed class ButtonStartRequest : AbstractButtonView
    {
        [SerializeField]
        private GetWeatherInfo _getWeatherInfo = default;

        [SerializeField, Min(0)]
        private float _lat = default;
        [SerializeField, Min(0)]
        private float _lon = default;

        protected override void OnButtonClicked() =>
            _getWeatherInfo.GetWeather(_lat, _lon);
    }
}
