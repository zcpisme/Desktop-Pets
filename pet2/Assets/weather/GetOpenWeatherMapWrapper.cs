using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

namespace XANTools
{
    /// <summary>
    ///  OpenWeatherMap 获取当天天气的接口封装
    /// </summary>
    public class GetOpenWeatherMapWrapper : MonoSingleton<GetOpenWeatherMapWrapper>
    {

        //获取天气 url = "https://api.openweathermap.org/data/2.5/weather?lat=22.36&lon=114.11&appid=YourKey";
        string urlHeader = "https://api.openweathermap.org/data/2.5/weather?lat=";
        string urlMid = "&lon=";
        string urlTailer = "&appid=282614f351f3cc9ecdedcc944aad1cec&units=metric";

        // 天气数据
        private WeatherStruct weatherStructData;
        public WeatherStruct WeatherStructData { get => weatherStructData; set => weatherStructData = value; }

        // 天气获取成功的事件
        Action<WeatherStruct> GetWeatherSucessAction;
        Action<string> GetWeatherFailAction;


        /// <summary>
        /// 开始获取天气数据
        /// </summary>
        /// <param name="GetWeatherSucessHandler">获取成功的事件</param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        public void StartGetWeather(float lat, float lon, Action<WeatherStruct> GetWeatherSucessHandler, Action<string> GetWeatherFailHandler)
        {
            GetWeatherSucessAction = GetWeatherSucessHandler;
            GetWeatherFailAction = GetWeatherFailHandler;
            StartCoroutine(GetRequest(GetWeatherUrl(lat, lon)));
        }

        /// <summary>
        /// 获取网络数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IEnumerator GetRequest(string url)
        {

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                Debug.Log(GetType() + "/UnityWebRequest.Get()/");

                yield return webRequest.SendWebRequest();

                // 可以放在 Update 中显示 
                //Debug.Log("webRequest.uploadProgress " + webRequest.uploadProgress);

                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);

                    // 执行回调
                    if (GetWeatherFailAction != null)
                    {
                        GetWeatherFailAction(webRequest.downloadHandler.text);
                    }
                }
                else
                {
                    string weatherJsonStr = webRequest.downloadHandler.text;
                    //Debug.Log(GetType() + "/GetRequest()/Old weatherJsonStr : " + weatherJsonStr);
                    // 如果有进行替换，便于 json 解析
                    weatherJsonStr = weatherJsonStr.Replace("\"base\":", "\"_base\":");
                    weatherJsonStr = weatherJsonStr.Replace("\"1h\":", "\"_1h\":");
                    weatherJsonStr = weatherJsonStr.Replace("\"3h\":", "\"_3h\":");
                    //Debug.Log(GetType() + "/GetRequest()/Replace weatherJsonStr : " + weatherJsonStr);

                    // 解析数据
                    WeatherStructData = JsonMapper.ToObject<WeatherStruct>(weatherJsonStr);

                    //Debug.Log(GetType() + "/GetRequest()/ WeatherStructData.weather[0].main :" + WeatherStructData.weather[0].main);
                    //Debug.Log(GetType() + "/GetRequest()/ WeatherStructData.main.temp_max :" + WeatherStructData.main.temp_max);
                    //Debug.Log(GetType() + "/GetRequest()/ WeatherStructData.main.temp_min :" + WeatherStructData.main.temp_min);

                    // 执行回调
                    if (GetWeatherSucessAction != null)
                    {
                        GetWeatherSucessAction(WeatherStructData);
                    }
                }
            }
        }

        /// <summary>
        /// 获取 天气数据，可能为空
        /// </summary>
        /// <returns></returns>
        public WeatherStruct GetWeatherStructData()
        {

            return weatherStructData;
        }

        /// <summary>
        /// 获取拼接的天气 url
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        string GetWeatherUrl(float lon, float lat)
        {

            return urlHeader + lon + urlMid + lat + urlTailer;
        }


    }


    #region 天气 json 数据结构 网址：https://openweathermap.org/current#current_JSON 
    /// <summary>
    /// OpenWeathermMap 获取数据的json数据结构
    /// </summary>
    public class WeatherStruct
    {
        public WeatherCoord coord;
        public List<Weather> weather;
        // base 是代码的特殊属性，不能作为变量名,替换位_base
        public string _base;
        public WeatherMain main;
        public double visibility;
        public WeatherWind wind;
        public WeatherClouds clouds;
        public WeatherRain rain;
        public WeatherSnow snow;
        public int dt;
        public WeatherSys sys;
        public int timezone;
        public int id;
        public string name;
        public int cod;
    }



    /// <summary>
    /// 经纬度
    /// </summary>
    public class WeatherCoord
    {
        public double lon;   // 经度
        public double lat;   // 纬度
    }

    /// <summary>
    /// 天气简单描述
    /// </summary>
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    /// <summary>
    /// 天气略详描述
    /// </summary>
    public class WeatherMain
    {
        public double temp;
        public double feels_like;
        public double temp_min;
        public double temp_max;
        public double pressure;
        public double humidity;
        public double sea_level;
        public double grnd_level;
    }

    /// <summary>
    /// 风速，风向
    /// </summary>
    public class WeatherWind
    {
        public double speed;
        public double deg;
        public double gust;
    }

    /// <summary>
    /// 云量
    /// </summary>
    public class WeatherClouds
    {
        public double all;
    }

    /// <summary>
    /// 雨速
    /// </summary>
    public class WeatherRain
    {

        // 由于变量并不能以数字开头,把原来的 1h / 3h 转为 _1h/_3h
        public double _1h;
        public double _3h;
    }

    /// <summary>
    /// 雪速
    /// </summary>
    public class WeatherSnow
    {

        // 由于变量并不能以数字开头,把原来的 1h / 3h 转为 _1h/_3h
        public double _1h;
        public double _3h;
    }

    /// <summary>
    /// 天气的地理位置
    /// </summary>
    public class WeatherSys
    {
        public double type;
        public int id;
        public double message;
        public string country;
        public int sunrise;
        public int sunset;
    }
    #endregion 天气 json 数据结构 网址：https://openweathermap.org/current#current_JSON 
}