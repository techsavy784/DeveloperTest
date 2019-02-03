using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGL_ProgrammingChallenge.Models
{
    /// <summary>
    /// This class defines properties exactly in required format that is 
    /// </summary>
    public class PetOutput
    {
        public string OwnerGender { get; set; }
        public List<Pet> pets { get; set; }
    }
}