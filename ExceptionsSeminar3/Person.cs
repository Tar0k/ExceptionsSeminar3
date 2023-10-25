using System.Text.RegularExpressions;

namespace ExceptionsSeminar3;

public partial class Person
{
    private readonly string _lastName;
    private readonly string _firstName;
    private readonly string _middleName;
    private readonly DateOnly _birthDate;
    private readonly long _phoneNumber;
    private readonly string _gender;

    public string LastName => _lastName;
    
    
    /// <summary>
    /// Результат операции создания Person
    /// </summary>
    /// <param name="person">созданный персонаж</param> 
    /// <param name="errorCode">код ошибки
    /// <para>99: Введеных параметров меньше требуемого</para>
    /// <para>100: Данные не введены</para>
    /// <para>101: Введенных парамтеров больше требуемого</para></param> 
    public class CreateResult(Person? person, long errorCode)
    {
        public Person? Person { get; } = person;
        public long ErrorCode { get; } = errorCode;
    }
    
    
    /// <summary>
    /// Создает класс Person из входной строки
    /// </summary>
    /// <param name="data">входная строка</param>
    /// <returns>Объект CreateResult, в котором Person и код ошибки при создании</returns>
    /// <exception cref="NotOnlyLettersException">Входной параметр заявленный как строка из букв найдены другие символы</exception>
    /// <exception cref="WrongFormatException">Входной параметр в некорректном формате</exception>
    /// <exception cref="InputStringException">Входной параметр некорректен</exception>
    public static CreateResult Create(string data)
    {

        var separatedData = data.Split();
        var argsCount = separatedData.Length;
        switch (argsCount)
        {
            case < 6:
                return new CreateResult(null, errorCode: 99);
            case > 6:
                return new CreateResult(null, errorCode: 101);
        }

        var lastName = separatedData[0];
        var firstName = separatedData[1];
        var middleName = separatedData[2];
        var birthDateRaw = separatedData[3];
        var phoneNumberRaw = separatedData[4];
        var gender = separatedData[5];
        
        if (!ValidateInput(lastName, OnlyLettersRegex())) throw new NotOnlyLettersException(lastName, "Фамилия");
        if (!ValidateInput(firstName, OnlyLettersRegex())) throw new NotOnlyLettersException(firstName,"Имя");
        if (!ValidateInput(middleName, OnlyLettersRegex())) throw new NotOnlyLettersException(middleName,"Отчество");
        if (!ValidateInput(birthDateRaw, BirthDateRegex())) throw new WrongFormatException(birthDateRaw, "Дата рождения");
        if (!ValidateInput(phoneNumberRaw, PhoneNumberRegex())) throw new WrongFormatException(phoneNumberRaw, "Номер телефона");
        if (!ValidateInput(gender, GenderRegex())) throw new InputStringException(gender, "Пол укзаан неверно");

        var birthDate = DateOnly.ParseExact(birthDateRaw, "dd.mm.yyyy");
        var phoneNumber = long.Parse(phoneNumberRaw);

        return new CreateResult(new Person(lastName, firstName, middleName, birthDate, phoneNumber, gender), 0);
        
        bool ValidateInput(string input, Regex regex) => regex.IsMatch(input);
    }

    private Person(string lastName, string firstName, string middleName, DateOnly birthDate, long phoneNumber,
        string gender)
    {
        _lastName = lastName;
        _firstName = firstName;
        _middleName = middleName;
        _birthDate = birthDate;
        _phoneNumber = phoneNumber;
        _gender = gender;
    }
    

    [GeneratedRegex(@"\b\D+\b")]
    private static partial Regex OnlyLettersRegex();
    
    [GeneratedRegex(@"\b\d{2}.\d{2}.\d{4}\b")]
    private static partial Regex BirthDateRegex();
    
    [GeneratedRegex(@"\b\d{6,15}\b")]
    private static partial Regex PhoneNumberRegex();
    
    [GeneratedRegex(@"\bf|m\b")]
    private static partial Regex GenderRegex();

    public override string ToString()
    {
        return $"{_lastName} {_firstName} {_middleName} {_birthDate} {_phoneNumber} {_gender}";
    }
}