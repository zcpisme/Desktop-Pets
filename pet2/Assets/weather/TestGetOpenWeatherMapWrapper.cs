using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XANTools;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using LitJson;
using UnityEngine.Networking;
//using System;
//using System.Device.Location;

public class TestGetOpenWeatherMapWrapper : MonoBehaviour
{


    //GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
    private Text weatherInfo;
    string myip = "";
    float lon = -118.41f;
    float lat = 34.0194f;
    string ipurl = "http://ip-api.com/json/";
    IPforLoc myIPinfo;

    public void OnEnable()
    {

        weatherInfo = GameObject.Find("Weather(Text)").GetComponent<Text>();
        // 测试经纬度

        GetOpenWeatherMapWrapper.Instance.StartGetWeather(lat, lon,
            (weatherData) =>
            {

                //Debug.Log(GetType() + "/Start()/ weatherData.weather[0].main :" + weatherData.weather[0].main);
                //Debug.Log(GetType() + "/Start()/ weatherData.main.temp_max :" + weatherData.main.temp_max);
                //Debug.Log(GetType() + "/Start()/ weatherData.main.temp_min :" + weatherData.main.temp_min);
                Debug.Log(lat + ":" + lon);
                weatherInfo.text = weatherData.main.temp.ToString() + "°C";

            }, (failInfo) =>
            {
                Debug.Log(GetType() + "/Start()/ failInfo :" + failInfo);
            });
    }



    void Start()
    {
        //watcher.TryStart(false, TimeSpan.FromMilliseconds(5000));
        //GeoCoordinate coor = watcher.Position.Location;

        //if (coor.IsUnknown != true)
        //{
        //    Debug.Log(coor.Latitude + ":" + coor.Longitude);

        //}
        //else
        //{
        //    Debug.Log("no loc");
        //}

        string HostName = Dns.GetHostName();
        IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
        for (int i = 0; i < IpEntry.AddressList.Length; i++)
        {
            //从IP地址列表中筛选出IPv4类型的IP地址
            //AddressFamily.InterNetwork表示此IP为IPv4,
            //AddressFamily.InterNetworkV6表示此地址为IPv6类型
            if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork && IpEntry.AddressList[i].ToString().Substring(0, 3) != "192")
            {
                myip = IpEntry.AddressList[i].ToString();
                Debug.Log(IpEntry.AddressList[i].ToString());
            }
        }
        if (myip != null)
        {
            StartCoroutine(GetText());
        }

    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(ipurl + myip);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            string results = www.downloadHandler.text;
            results = results.Replace("\"as\":", "\"_as\":");
            myIPinfo = JsonMapper.ToObject<IPforLoc>(results);
            //Debug.Log(myIPinfo.lat + ":" + myIPinfo.lon);
            lat = (float)myIPinfo.lat;
            lon = (float)myIPinfo.lon;
        }
    }

    void Update()
    {

    }

}

public class IPforLoc
{
    public string query;
    public string status;
    public string country;
    public string countryCode;
    public string region;
    public string regionName;
    public string city;
    public string zip;
    public double lat;
    public double lon;
    public string timezone;
    public string isp;
    public string org;
    public string _as;
}