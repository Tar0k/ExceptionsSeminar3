namespace ExceptionsSeminar3;

public class NotOnlyLettersException(string inputString, string fieldName) : InputStringException(inputString, fieldName)
{
    public override string ToString()
    {
        return InputString == string.Empty ?
            $"\"{FieldName}\" не состоит только из букв" :
            $"\"{FieldName}\" не состоит только из букв ({InputString})";
    }
}