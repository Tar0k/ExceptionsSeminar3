namespace ExceptionsSeminar3;

public class InputStringException : ArgumentException
{
    public InputStringException(string inputString, string fieldName)
    {
        base.Data.Add("input", inputString);
        base.Data.Add("fieldName", fieldName);
    }
    
    public InputStringException(string fieldName)
    {
        base.Data.Add("input", string.Empty);
        base.Data.Add("fieldName", fieldName);
    }

    protected string FieldName => base.Data["fieldName"] as string ?? "Неизвестно";
    protected string InputString => base.Data["input"] as string ?? "Неизвестно";

    public override string ToString()
    {
        return InputString == string.Empty ?
            $"\"{FieldName}\" введено не корректно" :
            $"Значение {InputString} не корректно для поля \"{FieldName}\"";
    }
}