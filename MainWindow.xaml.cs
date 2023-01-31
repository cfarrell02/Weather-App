using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int,string> weatherCodes;

        public MainWindow()
        {
            InitializeComponent();
            timeLabel.Content = DateTime.Now.TimeOfDay;
            weatherCodes = new Dictionary<int, string>()
            {
                {0, "Clear sky"},
                {1, "Mainly clear"},
                {2, "Partly cloudy"},
                {3, "Overcast"},
                {45, "Fog"},
                {48, "Depositing rime fog"},
                {51, "Drizzle: Light intensity"},
                {53, "Drizzle: Moderate intensity"},
                {55, "Drizzle: Dense intensity"},
                {56, "Freezing Drizzle: Light intensity"},
                {57, "Freezing Drizzle: Dense intensity"},
                {61, "Rain: Slight intensity"},
                {63, "Rain: Moderate intensity"},
                {65, "Rain: Heavy intensity"},
                {66, "Freezing Rain: Light intensity"},
                {67, "Freezing Rain: Heavy intensity"},
                {71, "Snow fall: Slight intensity"},
                {73, "Snow fall: Moderate intensity"},
                {75, "Snow fall: Heavy intensity"},
                {77, "Snow grains"},
                {80, "Rain showers: Slight intensity"},
                {81, "Rain showers: Moderate intensity"},
                {82, "Rain showers: Violent intensity"},
                {85, "Snow showers: Slight intensity"},
                {86, "Snow showers: Heavy intensity"},
                {95, "Thunderstorm: Slight or moderate"},
                {96,"Thunderstorm with slight hail" },
                {99,"Thunderstorm with heavy hail" }
            };
            GetWeatherData(52.25f,-7.07f);
            

        }

        private void searchBar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                // Call your method here
                GetLocationData(searchBar.Text);
            }
        }

        private async void GetLocationData(string str)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://geocoding-api.open-meteo.com/v1/search?name="+str);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    if(data.results == null)
                    try
                    {
                        placeNameLabel.Content = data.results[0].name + ", " + data.results[0].country;
                        GetWeatherData((float) data.results[0].latitude, (float) data.results[0].longitude);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }


                }
            }
        }

        private async void GetWeatherData(float latitude, float longitude)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://api.open-meteo.com/v1/forecast?latitude="+latitude+"&longitude="+longitude+"&hourly=temperature_2m,relativehumidity_2m,dewpoint_2m,apparent_temperature,precipitation,weathercode,surface_pressure,visibility,windspeed_10m&current_weather=true");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    int timeAsHours = DateTime.Now.TimeOfDay.Hours;
                    try { 
                    
                    temperatureLabel.Content = data.hourly.temperature_2m[timeAsHours] + " " + data.hourly_units.temperature_2m;
                    int weatherCode = data.hourly.weathercode[timeAsHours];
                    weatherCodeLabel.Content = weatherCodes[weatherCode];
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

    }
}
