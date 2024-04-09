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
    private int _humdity;
    public int Humdity
    {
        get { return _humdity; }
        set 
        { 
            _humdity = value;
            OnPropertyChanged();
        }
    }
    private double _windspeed;
    public double Windspeed
    {
        get { return _windspeed; }
        set 
        { 
            _windspeed = value;
            OnPropertyChanged();
        }
    }
    private string _country;
    public string Country
    {
        get { return _country; }
        set 
        { 
            _country = value;
            OnPropertyChanged();
        }
    }
    private string _icons;
    public string Icons
    {
        get { return _icons; }
        set 
        { 
            _icons = value;
            OnPropertyChanged();
        }
    }
    private string _currentlocation;

    public string CurrentLocation
    {
        get { return _currentlocation; }
        set 
        { 
            _currentlocation = value;
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
        //GetWeather(_httpClient);
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        GetWeather(_httpClient);
    }

    public async Task<WeatherInformtaion> GetWeather(object parameters)
    {
        var details = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (details != PermissionStatus.Granted)
        {
            details = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }
        var location = await Geolocation.GetLocationAsync();
        var lon = location.Longitude;
        var lat = location.Latitude;

        // Gets the Weather Api and its data and saves it in "response"
        string response = await _httpClient.GetStringAsync(new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid=22d84222850961e3fe8cc19c603cab33"));

        // Turning the response from JSON to C#
        WeatherInformtaion info = JsonConvert.DeserializeObject<WeatherInformtaion>(response);

        // Getting the info out the class and displaying it
        if (info != null)
        {
            Tempreture = ((int)info.main.temp);
            Windspeed = info.wind.speed;
            Humdity = info.main.humidity;
            Country = info.sys.country;
            CurrentLocation = info.name;

            if (info.weather.Count > 0)
            {
                var weather = info.weather[0];
                if (weather.main == "Clouds")
                {
                    Icons = "Resource/Images/cloudy.png";
                }
                else if (weather.main == "Rain")
                {
                    Icons = "Images/rainy.png";
                }
                else
                {
                    Icons = "sunny.png";
                }
            }
        }
        return info;
    }

    //public async void GetWeather(object parameters)
    //{
        
    //}
}