namespace GanLink.IAM.Domain.Models.ValueObjects;

public record RUC
{
    public string Number { get; }
    
    public RUC(string number)
    {
        // ⬇️ PERMITIR VACÍO SOLO PARA INICIALIZACIÓN DE EF CORE
        if (string.IsNullOrWhiteSpace(number))
        {
            Number = string.Empty;
            return;
        }
        
        if (number.Length != 11)
        {
            throw new ArgumentException("RUC number must be 11 digits");
        }
        if (!number.StartsWith("10") && !number.StartsWith("20"))
        {
            throw new ArgumentException("RUC number must start with 10 or 20");
        }
        Number = number;
    }
    
    public override string ToString() => Number;
}