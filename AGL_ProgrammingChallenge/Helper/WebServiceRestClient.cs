using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AGL_ProgrammingChallenge.Models;
using Newtonsoft.Json;
using RestSharp;

namespace AGL_ProgrammingChallenge.Helper
{
    public class WebServiceRestClient : IWebServiceRestClient
    {
        private readonly RestClient _client;
        private readonly string _url = ConfigurationManager.AppSettings["webservicebaseurl"];
        public WebServiceRestClient()
        {
            _url = "http://agl-developer-test.azurewebsites.net/people.json";
            _client = new RestClient(_url);
        }
        public List<PetModel> GetAll()
        {
            var request = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };

            var response = _client.Execute<List<PetModel>>(request);
            //List<PetModel> deserialized = JsonConvert.DeserializeObject<List<PetModel>>(response.Content);
            dynamic jsonResponse = JsonConvert.DeserializeObject<List<PetModel>>(response.Content);       

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            return jsonResponse;
        }
    }
}