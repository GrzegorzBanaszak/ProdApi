using System;

namespace ProdApi.DomainException;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
