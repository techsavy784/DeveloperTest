using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGL_ProgrammingChallenge.Models;

namespace AGL_ProgrammingChallenge.Helper
{
    public interface IWebServiceRestClient
    {
        List<PetModel> GetAll();
    }
}