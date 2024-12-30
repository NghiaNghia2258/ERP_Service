namespace ERP_Service.Shared.Exceptions;

public class RecordAlreadyExistsException : Exception
{
    public RecordAlreadyExistsException() : base("Record already exists !!!") { }
    public RecordAlreadyExistsException(string message) : base(message) { }
    public RecordAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
}
