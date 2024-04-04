//using Android.Content;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Pages;

public partial class WeatherPage : ContentPage
{
    // Properties
    private double _tempreture;
    public double Tempreture
    {
        get { return _tempreture; }
        set 
        { 
            _tempreture = value;
            OnPropertyChanged();
        }
    }

    private double _test;
    public double Test
    {
        get { return _test; }
        set 
        { 
            _test = value;
            OnPropertyChanged();
        }
    }


    private HttpClient _httpClient;
    public WeatherPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        BindingContext = this;

        // Request Header
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json"); 
        GetWeather(_httpClient);
    }

    public async void GetWeather(object parameters)
    {
        // Gets the Weather Api and its data and saves it in "response"
        string response = await _httpClient.GetStringAsync(new Uri("https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=22d84222850961e3fe8cc19c603cab33"));

        // Turning the response from JSON to C#
        WeatherInformtaion info = JsonConvert.DeserializeObject<WeatherInformtaion>(response);

        // Getting the info out the class and displaying it
        if (info != null)
        {
            Tempreture = info.main.temp;
            Test = info.main.temp;
            //WindSpeed = info.wind.speed;
            //Humidity = info.main.humidity;
        }
    }
}