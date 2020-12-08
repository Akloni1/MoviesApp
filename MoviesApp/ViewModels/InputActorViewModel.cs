using System;
using MoviesApp.Filters;

namespace MoviesApp.ViewModels
{
    public class InputActorViewModel
    {
        [ShortName(4)]
        public string Name { get; set; }
        
        [ShortName(4)]
        public  string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}