namespace ExceptionsSeminar3;

public class WrongFormatException(string inputString, string fieldName) : InputStringException(inputString, fieldName)
{
        public override string ToString()
        {
            return InputString == string.Empty ?
                $"\"{FieldName}\" неправильный формат" :
                $"\"{FieldName}\" неправильный формат ({InputString})";
        }
}