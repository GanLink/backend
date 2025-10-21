namespace GanLink.FarmManagement.Application.Exceptions;

/// <summary>
/// Excepción que se lanza cuando no se encuentra un Farm específico.
/// </summary>
public class FarmNotFoundException : Exception
{
    public FarmNotFoundException(int farmId)
        : base($"El Farm con ID '{farmId}' no fue encontrado.")
    {
    }
}