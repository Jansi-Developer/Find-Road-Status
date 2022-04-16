using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace RoadStatusAPI
{
    //Initialize Exit Code for Success and Failure
    enum ExitCode : int
    {
        Success = 0,
        Error = 1
    }

    public class Program
    {
        public static async Task Main(string[] roadName)
        {
            try
            {
                RoadStatus roadStatus = new RoadStatus(roadName);
                await roadStatus.GetRoadStatusDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class RoadStatus
    {

        private static readonly HttpClient client = new HttpClient();
        private static int _statusCode = 0;
        private static string _roadName = string.Empty;
        private static Dictionary<string, string> _queryString = null;

        /// <summary>
        /// Initialize and validate input values road name, app_id and app_key
        /// </summary>
        /// <param name="roadName"></param>
        public RoadStatus(string[] roadName)
        {
            GetValidRoadName(roadName);

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json")
                                                          .Build();
            _queryString = new Dictionary<string, string>
            {
                ["app_id"] = configuration["app_id"],
                ["app_key"] = configuration["app_key"],
            };
        }
        /// <summary>
        /// Get and display Road Details based on Status code from Web API.
        /// Success Status Code is 200 and Not Found Status Code is 404
        /// All remaining status codes considered as Invalid Request
        /// </summary>
        /// <returns></returns>
        internal async Task GetRoadStatusDetails()
        {
            var roadDetails = await RoadStatusRequest();

            if (_statusCode == 200)
            {
                foreach (var road in roadDetails)
                {
                    Console.WriteLine("The status of the " + road.Name + " is as follows");
                    Console.WriteLine("         Road Status is " + road.Status);
                    Console.WriteLine("         Road Status Description is " + road.StatusDescription);
                }
                Environment.ExitCode = (int)ExitCode.Success;
            }
            else if (_statusCode == 404)
            {
                Console.WriteLine(_roadName + " is not a valid road");
                Environment.ExitCode = (int)ExitCode.Error;
            }
            else
            {
                Console.WriteLine("Invalid Request");
                Environment.ExitCode = (int)ExitCode.Error;
            }
        }

        /// <summary>
        /// Http call to get road details as response
        /// If response code is 200, return road details
        /// Else return null
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoadDetails>> RoadStatusRequest()
        {
            HttpResponseMessage response = await client.GetAsync(QueryHelpers.AddQueryString("https://api.tfl.gov.uk/Road/" + _roadName, _queryString));
            _statusCode = (int)response.StatusCode;
            if (_statusCode == 200)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<RoadDetails>>(responseBody);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Validate user input for road name.
        /// Get input until road name is not null
        /// </summary>
        /// <param name="roadName"></param>
        private async void GetValidRoadName(string[] roadName)
        {
            if (roadName.Length == 0)
            {
                while (string.IsNullOrEmpty(_roadName))
                {
                    Console.Write("Enter a Road Name to Find: ");
                    _roadName = Console.ReadLine();
                }
            }
            else
            {
                _roadName = roadName[0];
            }
        }
    }
}