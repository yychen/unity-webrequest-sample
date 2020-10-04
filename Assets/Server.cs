using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
using System.Web;
using UnityEngine.UI;

[Serializable]
public class MachineSetting
{
    public int runtime;
}

[Serializable]
public class MachinesSetting
{
    public MachineSetting front;
    public MachineSetting back;
}

[Serializable]
public class Settings
{
    public MachinesSetting machines;
}

public class Server : MonoBehaviour
{
    public int Runtime = 30;
    public Text RuntimeText;

    public Text Serial;
    public Text Score;

    // Start is called before the first frame update
    void Start()
    {
        Settings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReflectSettings()
    {
        RuntimeText.text = $"Play Time: {Runtime}s";
    }


    public async void Record()
    {
        HttpClient client = new HttpClient();

        try
        {
            var builder = new UriBuilder("http://localhost");
            builder.Path = "/record";
            builder.Port = 8000;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["serial"] = Serial.text;
            query["score"] = Score.text;
            builder.Query = query.ToString();

            Debug.Log(builder);
            // DISCLAIMER: This is for demo purpose only!
            // Do not use this in production!
            // Create Record should not be used with GET
            // And there should be at least some form of authentication
            HttpResponseMessage response = await client.GetAsync(builder.ToString());
            string responseBody = await response.Content.ReadAsStringAsync();
            Debug.Log(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }

    public async void Query()
    {
        HttpClient client = new HttpClient();

        try
        {
            var builder = new UriBuilder("http://localhost");
            builder.Path = "/query";
            builder.Port = 8000;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["serial"] = Serial.text;
            builder.Query = query.ToString();

            Debug.Log(builder);
            HttpResponseMessage response = await client.GetAsync(builder.ToString());
            string responseBody = await response.Content.ReadAsStringAsync();

            // Expected result for this demo looks like:
            // {"records": [100, 200, 50]}
            Debug.Log(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }

    async void Settings()
    {
        HttpClient client = new HttpClient();

        try
        {
            var builder = new UriBuilder("http://localhost");
            builder.Path = "/settings";
            builder.Port = 8000;

            HttpResponseMessage response = await client.GetAsync(builder.ToString());
            string responseBody = await response.Content.ReadAsStringAsync();
            Debug.Log(responseBody);

            // Expected result for this demo looks like:
            // {"machines": {"front": {"runtime": 45}, "back": {"runtime": 30}}}
            var settings = JsonUtility.FromJson<Settings>(responseBody);
            Debug.Log(settings);
            Debug.Log(settings.machines.front.runtime);
            Runtime = settings.machines.front.runtime;

            ReflectSettings();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }
}
