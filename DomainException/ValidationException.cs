using System;

namespace ProdApi.DomainException;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}