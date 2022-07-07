 
using System;
using System.Linq;

namespace Demo.Accounts
{
    public class User
    { 
        public int id
        {
            get;
            set;
        }
        public string name { get; set; }        
        public DateTime birthdate { get; set; }
        public String username { get; set; }

        //public Review[] reviews {get; set; }

        //public Review[] GetReviews(int id=5) { 
        //    return new ReviewRepository().GetReviewsByAuthorId(id).ToArray(); ;
        //}
    }
   // public record User(int Id, string Name, DateTime Birthdate, string Username);
}