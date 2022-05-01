using Demo.Reviews;
using System;

namespace Demo.Accounts
{
    public class User
    {
        private Int32 _id { get; set; }
        public int Id
        {
            get
            {
                return _id;
            }
            set { _id = value; }
        }
        public string Name { get; set; }        
        public DateTime Birthdate { get; set; }
        public String UserName { get; set; }

        public Review[] reviews {get; set; }
    }
   // public record User(int Id, string Name, DateTime Birthdate, string Username);
}