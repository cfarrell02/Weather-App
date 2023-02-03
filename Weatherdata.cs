using System;
using System.Collections.Generic;
public class WeatherData
{
	private float latitude, longitude, generationtime_ms, utc_offset_seconds, elevation;
	private string timezone, timezone_abbreviation;
	Dictionary<string, string> hourlyUnits, dailyUnits;

	public WeatherData(dynamic data)
	{
		latitude = data.latitude;
		longitude = data.longitude;
		generationtime_ms = data.generationtime_ms;
		utc_offset_seconds = data.utc_offset_seconds;
		elevation = data.elevation;
		timezone = data.timezone;
		timezone_abbreviation = data.timezone_abbreviation;
		hourlyUnits = data.hourly_units;
		dailyUnits = data.daily_units;
	}


	private class HourlyData
	{
		List<string> time;
		List<float> temperature, relativehumidity, dewpoint, apparentTemperature, precipitation, surfacePressure, visibility, windspeed;
		List<int> weathercode;

		HourlyData(dynamic hourly)
		{
			this.time = hourly.time;
			this.temperature = hourly.temperature_2m;
			this.relativehumidity = hourly.relativehumidity_2m;
			this.dewpoint = hourly.dewpoint_2m;
			this.apparentTemperature = hourly.apparent_temperature;
			this.precipitation = hourly.precipitation;
			this.weathercode = hourly.weathercode;
			this.surfacePressure = hourly.surface_pressure;
			this.visibility = hourly.visibility;
			this.windspeed = hourly.windspeed;
		}

	}

	private class DailyData
    {
		List<string> time, sunrise, sunset;
		List<int> weathercode;

		DailyData(dynamic daily)
        {
			this.time = daily.time;
			this.weathercode = daily.weathercode;
			this.sunrise = daily.sunrise;
			this.sunset = daily.sunset;
        }

    }
	
	private class CurrentWeather
    {
		float temperature, windSpeed, windDirection;
		int weathercode;
		string time;

		CurrentWeather(dynamic weather)
		{
			temperature = weather.temperature;
			windSpeed = weather.windspeed;
			windDirection = weather.winddirection;
			weathercode = weather.weathercode;
			time = weather.time;
		}
	}
}
