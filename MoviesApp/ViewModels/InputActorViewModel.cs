using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.ViewModels;

public class InputActorViewModel
{
    [Required]
    [ActorAttribute(3)]
    public string FirstName { get; set; }
    
    [Required]
    [ActorAttribute(3)]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }
    
    
    
}