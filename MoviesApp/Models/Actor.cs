using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public  string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
            
    }
}