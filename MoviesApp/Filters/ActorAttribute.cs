using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Filters;

public class ActorAttribute : ValidationAttribute
{
    public ActorAttribute( int Name)
    {
        NameLen = Name;
        
    }

    public int NameLen { get; }
    public string GetErrorMessageAttribute() => $"Actor must have name longer than {NameLen} and lastname  longer than {NameLen}.";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        
        if (value is string)
        {
            var length = ((string)value).Length;
            if (length<NameLen)
            {
                return new ValidationResult(GetErrorMessageAttribute());
            }
            return ValidationResult.Success;
        }

        return new ValidationResult("Enter name correctly");

    }
}