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
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //
    public partial class MainWindow : Window
    {
        private Dictionary<int, string> weatherCodes;
        private Dictionary<int, Image> weatherIcons;


        public MainWindow()
        {
            InitializeComponent();
            timeLabel.Content = DateTime.Now.TimeOfDay.ToString()[..5];

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

                weatherIcons = new Dictionary<int, Image>()
            {
            {-3, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\10.png",UriKind.RelativeOrAbsolute))} },
            {-2, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\10.png",UriKind.RelativeOrAbsolute))} },
            {-1, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\4.png",UriKind.RelativeOrAbsolute))} },
            {0, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\20.png",UriKind.RelativeOrAbsolute))} },
            {1, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\1.png",UriKind.RelativeOrAbsolute))} },
            {2, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\3.png",UriKind.RelativeOrAbsolute))} },
            {3, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\2.png",UriKind.RelativeOrAbsolute))} },
            {45, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\8.png",UriKind.RelativeOrAbsolute))} },
            {48, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\8.png",UriKind.RelativeOrAbsolute))} },
            {51, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\12.png",UriKind.RelativeOrAbsolute))} },
            {53, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\15.png",UriKind.RelativeOrAbsolute))} },
            {55, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\17.png",UriKind.RelativeOrAbsolute))} },
            {56, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\12.png",UriKind.RelativeOrAbsolute))} },
            {57, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\17.png",UriKind.RelativeOrAbsolute))} },
            {61, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\14.png",UriKind.RelativeOrAbsolute))} },
            {63, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\13.png",UriKind.RelativeOrAbsolute))} },
            {65, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\6.png",UriKind.RelativeOrAbsolute))} },
            {66, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\14.png",UriKind.RelativeOrAbsolute))} },
            {67, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\6.png",UriKind.RelativeOrAbsolute)) } },
            {71, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\17.png",UriKind.RelativeOrAbsolute))} },
            {73, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\16.png",UriKind.RelativeOrAbsolute))} },
            {75, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\18.png",UriKind.RelativeOrAbsolute))} },
            {77, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\19.png",UriKind.RelativeOrAbsolute))} },
            {80, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\14.png",UriKind.RelativeOrAbsolute))} },
            {81, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\13.png",UriKind.RelativeOrAbsolute))} },
            {82, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\6.png",UriKind.RelativeOrAbsolute))} },
            {85, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\17.png",UriKind.RelativeOrAbsolute))} },
            {86, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\18.png",UriKind.RelativeOrAbsolute))} },
            {95, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\9.png",UriKind.RelativeOrAbsolute))} },
            {96, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\5.png",UriKind.RelativeOrAbsolute))} },
            {99, new Image(){ Source = new BitmapImage(new Uri("C:\\Users\\cianf\\source\\repos\\WpfApp1\\images\\5.png",UriKind.RelativeOrAbsolute))} }
            };


            GetWeatherData(52.25f,-7.07f);
            placeNameLabel.Content = "Waterford, Ireland";





        }

        private void searchBar_KeyDown(object sender, KeyEventArgs e)
        {



            if (e.Key != Key.Escape)
            {
                searchBar.IsDropDownOpen = true;
                GetLocationData(searchBar.Text, e.Key == Key.Return);
                
               
            }
        }

        private void searchBar_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void searchBar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                GetLocationData(searchBar.Text, true);
            
        }

        private async void GetLocationData(string str, bool performSearch, int resultIndex = 0)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://geocoding-api.open-meteo.com/v1/search?name="+str);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    if (data.results != null)
                    {
                        try
                        {
                            if (performSearch)
                            {
                             
                                placeNameLabel.Content = data.results[resultIndex].name + ", " + data.results[resultIndex].country;
                                GetWeatherData((float)data.results[resultIndex].latitude, (float)data.results[resultIndex].longitude);
                            }
                            else
                            {
                                searchBar.Items.Clear();
                                foreach(var item in data.results)
                                {
                                    searchBar.Items.Add(item.name+", "+item.country);



                                }

                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }

                }
            }
        }

        private async void GetWeatherData(float latitude, float longitude)  

        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://api.open-meteo.com/v1/forecast?latitude="+latitude+"&longitude="+longitude+ "&hourly=temperature_2m,relativehumidity_2m" +
                    ",dewpoint_2m,apparent_temperature,precipitation,weathercode,surface_pressure,visibility,windspeed_10m&daily=weathercode,sunrise,sunset&timezone=auto&current_weather=true");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    int timeAsHours = DateTime.Now.TimeOfDay.Hours;
                    try { 
                    
                    temperatureLabel.Content = data.current_weather.temperature + " " + data.hourly_units.temperature_2m;
                        int weatherCode = data.current_weather.weathercode;
                        
                    weatherCodeLabel.Content = weatherCodes[weatherCode];
                        stackPanel.Children.Clear();
                        
                        for (int i = timeAsHours -12; i < timeAsHours + 24; ++i)
                        {
                            StackPanel innerStackPanel = new StackPanel();


                            
                            
                            Image image = new Image();
                            int index = (int)data.hourly.weathercode[i];
                            string sunset = data.daily.sunset[0], sunrise = data.daily.sunrise[0];
                            sunset = sunset.Substring(sunset.Length - 5, 2);
                            sunrise = sunrise.Substring(sunrise.Length - 5, 2);
                            int sunriseTime = int.Parse(sunrise), sunsetTime = int.Parse(sunset);
                            

                            if ((index >= 0 && index <= 2) && (i%24 > sunsetTime || i%24 < sunriseTime))
                            {
                                image.Source = weatherIcons[-1 * (index + 1)].Source;
                            }else
                            {
                                image.Source = weatherIcons[index].Source;
                            }

                            Label label = new Label();
                            string time = data.hourly.time[i];
                            time = time.Substring(time.Length - 5);
                            label.Content = time + "\n" + data.hourly.temperature_2m[i] + data.hourly_units.temperature_2m;
                            
                            innerStackPanel.Children.Add(image);
                            innerStackPanel.Children.Add(label);
                            image.Margin = new Thickness(5, 5, 5, 10);
                            if(i == timeAsHours)
                            {
                                innerStackPanel.Background = new SolidColorBrush(Colors.LightBlue);
                            }

                            stackPanel.Children.Add(innerStackPanel);

                        }
                        scrollViewer.ScrollToHorizontalOffset(stackPanel.ActualWidth * timeAsHours / 23);

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
