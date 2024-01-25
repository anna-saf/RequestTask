namespace RequestTask.Feature.Weather
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    ///  лассы, представл€ющие JSON ответ сервера
    /// </summary>
    public class ResponseData
    {
        public Coords coord;
        public List<Weather> weather;
        [JsonProperty("base")]
        public string stations;
        public Main main;
        public int visibility;
        public Rain rain;
        public Clouds clouds;
        public int dt;
        public Sys sys;
        public int timezone;
        public int id;
        public string name;
        public int cod;
    }
    public class Coords
    {
        public float lon;
        public float lat;
    }
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }
    public class Main
    {
        public float temp;
        public float feels_like;
        public float temp_min;
        public float temp_max;
        public int pressure;
        public int humidity;
        public int sea_level;
        public int grnd_level;
    }
    public class Rain
    {
        [JsonProperty("1h")]
        public float rainDuration;
    }
    public class Clouds
    {
        public float all;
    }
    public class Sys
    {
        public int type;
        public int id;
        public string country;
        public int sunrise;
        public int sunset;
    }
}
