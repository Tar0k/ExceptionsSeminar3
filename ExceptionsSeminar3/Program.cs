using ExceptionsSeminar3;

while (true)
{
    Console.Write("Введите в додну строчку фамилию, имя, отчество, дату рождения, номертелефона, пол: ");
    var inputData = Console.ReadLine();
    // inputData = "Иванов Иван Иванович 20.20.2000 832573354761234 f";
    // inputData = "Петров Петр Петрович 12.12.1912 832565 f";

    if (string.IsNullOrEmpty(inputData))
    {
        Console.WriteLine("Данные не введены");
        continue;
    }
    
    try
    {
        var result = Person.Create(inputData);
        if (result.ErrorCode != 0)
        {
            switch (result.ErrorCode)
            {
                case 99:
                    Console.WriteLine("Введеных параметров меньше чем нужно");
                    break;
                case 101:
                    Console.WriteLine("Введеных параметров больше чем нужно");
                    break;
            }
            continue;
        }
        using var writer = new StreamWriter($"{result.Person?.LastName}.txt", true);
        writer.WriteLine(result.Person);
    }
    catch (InputStringException ex)
    {
        Console.WriteLine(ex);
    }
    catch (IOException ex)
    {
        Console.WriteLine(ex.StackTrace);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.StackTrace);
    }
    


    Console.Write("Выйти? Введите \"да\" (нет, что угодно): ");
    var decisionToExit = Console.ReadLine();
    if (decisionToExit?.ToLower() == "да")
    {
        Console.WriteLine("Выход");
        break;
    }
}
