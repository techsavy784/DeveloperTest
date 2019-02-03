using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGL_ProgrammingChallenge.Models
{ 

    public class PetModel
    {
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public List<Pet> pets { get; set; }
    }

    public class Pet
    {
        public string name { get; set; }
        public string type { get; set; }
    }

}