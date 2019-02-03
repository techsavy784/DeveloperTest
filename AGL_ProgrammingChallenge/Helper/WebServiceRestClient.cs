using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using AGL_ProgrammingChallenge.Models;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace AGL_ProgrammingChallenge.Helper
{
    public class WebServiceRestClient : IWebServiceRestClient
    {
        private readonly RestClient _client;
        private readonly string _url = ConfigurationManager.AppSettings["webservicebaseurl"];
        string path =  Path.Combine( Directory.AssemblyDirectory,@"..\..\Log.txt");
        public WebServiceRestClient()
        {
            _url = "http://agl-developer-test.azurewebsites.net/people.json";
            _client = new RestClient(_url);
        }
        public List<PetModel> GetAll()
        {
            try
            {
                var request = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
                var response = _client.Execute<List<PetModel>>(request);
                dynamic jsonResponse = JsonConvert.DeserializeObject<List<PetModel>>(response.Content);

                if (response.Data == null)
                    throw new Exception(response.ErrorMessage);
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                Log.Information("Web Service Response - {Content}", response.Content);
                return jsonResponse;
            }
            catch(Exception ex)
            {
                dynamic jsonResponse = null;
                Log.Error( ex.Message);
                return jsonResponse;
            }
        }
    }

   public static class Directory
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}