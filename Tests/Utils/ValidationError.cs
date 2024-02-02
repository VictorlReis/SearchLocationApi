namespace Tests.Utils;
public class ValidationError
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
    public string AttemptedValue { get; set; }
    public ValidationError(string propertyName, string errorMessage, string attemptedValue)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
        AttemptedValue = attemptedValue;
    }
}
