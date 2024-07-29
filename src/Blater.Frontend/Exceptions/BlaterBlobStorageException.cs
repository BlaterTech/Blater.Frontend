using Blater.Results;

namespace Blater.Frontend.Exceptions;

public class BlaterBlobStorageException : Exception
{
    public BlaterBlobStorageException(string message) : base(message)
    {
    }
    
    public BlaterBlobStorageException(string message, params object?[] args) : base(message)
    {
    }

    public BlaterBlobStorageException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public BlaterBlobStorageException()
    {
    }

    public BlaterBlobStorageException(List<BlaterError> errors) : base(string.Join(", ", errors.Select(e => e.Message)))
    {
    }
}