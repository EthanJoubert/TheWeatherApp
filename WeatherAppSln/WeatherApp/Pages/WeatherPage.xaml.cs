using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Pages;

public partial class WeatherPage : ContentPage
{
    private string _test;

    public string Test
    {
        get { return _test; }
        set { _test = value; }
    }

    private HttpClient _httpClient;
    public WeatherPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        // Request Header
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async void GetLatestJoke(object parameters)
    {
        // Gets the Dad Joke Api and its data and saves it in "response"
        string response = await _httpClient.GetStringAsync(new Uri("https://icanhazdadjoke.com"));

        // Turning the response from JSON to C#
        WeatherInfo latestjoke = JsonConvert.DeserializeObject<WeatherInfo>(response);

        // Getting the joke out the class and displaying it
        if (latestjoke != null)
        {
            Test = latestjoke.Temperture;
        }
    }
}