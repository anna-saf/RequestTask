namespace RequestTask.Feature.Weather
{
    using UnityEngine;

    public class GetWeatherOnEnable : MonoBehaviour
    {
        [SerializeField, Space()]
        protected GetWeatherInfo _getWeatherInfo = default;

        [SerializeField, Min(0)]
        protected float _lat = default;
        [SerializeField, Min(0)]
        protected float _lon = default;

        protected virtual void Start() =>
            _getWeatherInfo.GetWeather(_lat, _lon);
    }
}
