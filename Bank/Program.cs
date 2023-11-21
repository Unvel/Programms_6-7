using System;
using System.Collections.Generic;
using System.Linq;

class Account
{
    public int AccountNumber { get; set; }
    public string Currency { get; set; }
    public double Balance { get; set; }
    public bool IsLocked { get; set; }
}

class Client
{
    public string FIO { get; set; }
    public string PassportNumber { get; set; }
    public List<Account> Accounts { get; set; } = new List<Account>();
}

class Employee
{
    public string FIO { get; set; }
    public string ID { get; set; }
    public long PhoneNumber { get; set; }
    public string Department { get; set; }
}

class Program
{
    static void Main()
    {
        List<Client> clients = Clients();
        ClientInfo(clients);
        Console.WriteLine();

        List<Employee> employees = Employees();
        EmployeeInfo(employees);
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("1. Поиск клиента");
            Console.WriteLine("2. Добавление клиента");
            Console.WriteLine("3. Поиск сотрудника");
            Console.WriteLine("4. Удаление клиента");
            Console.WriteLine("5. Выход из программы");
            Console.WriteLine();
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine();
                    Console.Write("Введите фамилию или номер паспорта: ");

                    string searchKey = Console.ReadLine();
                    Console.WriteLine();
                    var client = FindClient(clients, searchKey);
                    if (client != null)
                    {
                        Console.WriteLine($"Найден клиент: {client.FIO}");
                        ClientAccountInfo(client);
                        Console.WriteLine();
                        ManageClientAccounts(client);

                    }
                    else
                    {
                        Console.WriteLine("Клиент не найден. Хотите добавить нового клиента?");
                        Console.Write("Да/Нет: ");
                        string response = Console.ReadLine();
                        if (response.ToLower() == "да") AddNewClient(clients);
                    }
                    break;
                case "2":
                    Console.WriteLine();
                    AddNewClient(clients);
                    break;
                case "3":
                    Console.WriteLine();
                    Console.Write("Введите фамилию или ID: ");
                    string searchKeyEmployee = Console.ReadLine();
                    var employee = FindEmployee(employees, searchKeyEmployee);
                    if (employee != null) Console.WriteLine($"Найден сотрудник: {employee.FIO} {employee.PhoneNumber}");
                    else Console.WriteLine("Сотрудник не найден.");
                    break;
                case "4":
                    Console.WriteLine();
                    Console.Write("Введите фамилию или номер паспорта клиента для удаления: ");
                    string searchKeyToDelete = Console.ReadLine();
                    RemoveClient(clients, searchKeyToDelete);
                    ClientInfo(clients);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }
            Console.WriteLine();
        }
    }
    static void ClientInfo(List<Client> clients)
    {
        Console.WriteLine(new string('-', 110));
        Console.WriteLine($"|{"ФИО",-32}|{"Номер паспорта",-22}|{"Счета",-52}|");
        Console.WriteLine(new string('-', 110));
        foreach (var client in clients)
        {
            var accountsInfo = client.Accounts.Select(a => $"{a.AccountNumber}: {a.Currency} ({a.Balance})");
            string accountsList = string.Join(", ", accountsInfo);
            Console.WriteLine($"| {client.FIO,-30} | {client.PassportNumber,-20} | {accountsList,-48}   |");
        }
        Console.WriteLine(new string('-', 110));
    }
    static void EmployeeInfo(List<Employee> employees)
    {
        Console.WriteLine(new string('-', 91));
        Console.WriteLine($"|{"ФИО",-32}|         ID         |{"Номер телефона",-18}|{"Отдел",-16}|");
        Console.WriteLine(new string('-', 91));

        foreach (var employee in employees) Console.WriteLine($"| {employee.FIO,-30} | {employee.ID,-18} | {employee.PhoneNumber,-16} | {employee.Department,-15}|");

        Console.WriteLine(new string('-', 91));
    }
    static void ClientAccountInfo(Client client)
    {
        Console.WriteLine();
        Console.WriteLine($"Состояние счетов для клиента {client.FIO}:");

        foreach (var account in client.Accounts)
        {
            Console.Write($"Счет {account.Currency} (Номер счета: {account.AccountNumber}): ");

            if (account.Balance == 0) Console.WriteLine("баланс  0");

            else if (account.Balance > 0) Console.WriteLine($"баланс {account.Balance}");

            else Console.WriteLine($"баланс {account.Balance}");

            if (account.IsLocked) Console.WriteLine("Счет заблокирован");
        }
    }

    static Client FindClient(List<Client> clients, string searchKey)
    {
        var matchingClients = clients.Where(client => client.FIO.ToLower().Contains(searchKey.ToLower()) || client.PassportNumber == searchKey);

        if (matchingClients.Count() == 1) return matchingClients.First();

        else if (matchingClients.Count() > 1)
        {
            Console.WriteLine("Найдено несколько совпадений:");
            int index = 1;
            foreach (var client in matchingClients) Console.WriteLine($"{index++}. {client.FIO} ({client.PassportNumber})");

            Console.Write("Выберите номер клиента: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice > 0 && choice <= matchingClients.Count()) return matchingClients.ElementAtOrDefault(choice - 1);

            else
            {
                Console.WriteLine("Неверный выбор.");
                return null;
            }
        }
        else
        {
            Console.WriteLine("Клиент не найден.");
            return null;
        }
    }
    static Employee FindEmployee(List<Employee> employees, string key)
    {
        var matchingEmployees = employees.Where(employee => employee.FIO.ToLower().Contains(key.ToLower()) || employee.ID == key);

        if (matchingEmployees.Count() == 1) return matchingEmployees.First();

        else if (matchingEmployees.Count() > 1)
        {
            Console.WriteLine("Найдено несколько совпадений:");
            int index = 1;
            foreach (var employee in matchingEmployees) Console.WriteLine($"{index++}. {employee.FIO} ({employee.ID})");

            Console.Write("Выберите номер сотрудника: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice > 0 && choice <= matchingEmployees.Count()) return matchingEmployees.ElementAtOrDefault(choice - 1);
            else
            {
                Console.WriteLine("Неверный выбор.");
                return null;
            }
        }
        else
        {
            Console.WriteLine("Сотрудник не найден.");
            return null;
        }
    }
    static void AddNewClient(List<Client> clients)
    {
        Console.WriteLine("Введите ФИО нового клиента:");
        string fullName = Console.ReadLine();
        Console.WriteLine("Введите номер паспорта нового клиента:");
        string passportNumber = Console.ReadLine();

        if (clients.Any(client => client.PassportNumber == passportNumber))
        {
            Console.WriteLine("Клиент с таким номером паспорта уже существует. Новый клиент не будет добавлен.");
        }
        else
        {
            var newClient = new Client
            {
                FIO = fullName,
                PassportNumber = passportNumber,
            };

            OpenAccount(newClient, "RUB PMR");

            clients.Add(newClient);

            Console.WriteLine("Новый клиент добавлен.");
            ClientInfo(clients);
            Console.WriteLine();
        }
    }
    static void ManageClientAccounts(Client client)
    {
        while (true)
        {
            Console.WriteLine("1. Открыть счет RUB");
            Console.WriteLine("2. Открыть счет USD");
            Console.WriteLine("3. Пополнить счет");
            Console.WriteLine("4. Снять со счета");
            Console.WriteLine("5. Заблокировать счет");
            Console.WriteLine("6. Разблокировать счет");
            Console.WriteLine("7. Удалить счет");
            Console.WriteLine("8. Вернуться в главное меню");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    OpenAccount(client, "RUB RF");
                    break;
                case "2":
                    OpenAccount(client, "USD");
                    break;
                case "3":
                    Deposit(client);
                    break;
                case "4":
                    Withdraw(client);
                    break;
                case "5":
                    LockAccount(client);
                    break;
                case "6":
                    UnlockAccount(client);
                    break;
                case "7":
                    RemoveAccount(client);
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }
            Console.WriteLine();
        }
    }
    static void OpenAccount(Client client, string currency)
    {

        if (client.Accounts.Any(account => account.Currency == currency))
        {
            Console.WriteLine();
            Console.WriteLine($"У клиента уже существует счет в {currency}. Нельзя создать дубликат.");
            ClientAccountInfo(client);
            return;
        }

        int accountNumber = client.Accounts.Count + 1;

        var newAccount = new Account
        {
            AccountNumber = accountNumber,
            Currency = currency,
            Balance = 0,
            IsLocked = false,
        };
        client.Accounts.Add(newAccount);
        Console.WriteLine();
        Console.WriteLine($"Открыт новый {currency} счет (Номер счета: {accountNumber}).");
        ClientAccountInfo(client);
    }

    static void Deposit(Client client)
    {
        Console.Write("Введите номер счета: ");
        int accountNumber = int.Parse(Console.ReadLine());

        var account = client.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

        if (account != null)
        {
            if (account.IsLocked)
            {
                Console.WriteLine();
                Console.WriteLine("Счет заблокирован. Нельзя пополнить.");
            }
            else
            {
                Console.Write("Введите сумму для пополнения: ");
                double amount = double.Parse(Console.ReadLine());
                account.Balance += Math.Abs(amount);
                Console.WriteLine();
                Console.WriteLine($"Счет {account.Currency} счет {accountNumber} пополнен на {Math.Abs(amount)}.");
                ClientAccountInfo(client);
            }
        }
        else Console.WriteLine("Счет не найден.");
    }

    static void Withdraw(Client client)
    {
        Console.Write("Введите номер счета: ");
        int accountNumber = int.Parse(Console.ReadLine());

        var account = client.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

        if (account != null)
        {
            if (account.IsLocked)
            {
                Console.WriteLine();
                Console.WriteLine("Счет заблокирован. Нельзя снять средства.");
                ClientAccountInfo(client);
            }
            else
            {
                Console.Write("Введите сумму для снятия: ");
                double amount = double.Parse(Console.ReadLine());

                if (account.Balance >= amount)
                {
                    account.Balance -= Math.Abs(amount);
                    Console.WriteLine();
                    Console.WriteLine($"Со счета {account.Currency} счет {accountNumber} снято {Math.Abs(amount)}.");
                    ClientAccountInfo(client);

                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Недостаточно средств на счете.");
                    ClientAccountInfo(client);
                }
            }
        }
        else Console.WriteLine("Счет не найден.");
    }
    static void LockAccount(Client client)
    {
        Console.Write("Введите номер счета: ");
        int accountNumber = int.Parse(Console.ReadLine());

        var account = client.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account != null)
        {
            account.IsLocked = true;
            Console.WriteLine();
            Console.WriteLine($"Счет {account.Currency} счет {accountNumber} заблокирован.");
            ClientAccountInfo(client);
        }
        else Console.WriteLine("Счет не найден.");
    }
    static void UnlockAccount(Client client)
    {
        Console.Write("Введите номер счета: ");
        int accountNumber = int.Parse(Console.ReadLine());

        var account = client.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

        if (account != null)
        {
            account.IsLocked = false;
            Console.WriteLine();
            Console.WriteLine($"Счет {account.Currency} счет {accountNumber} разблокирован.");
            ClientAccountInfo(client);
        }
        else Console.WriteLine("Счет не найден.");
    }
    static void RemoveAccount(Client client)
    {
        Console.Write("Введите номер счета для удаления: ");
        int accountNumber = int.Parse(Console.ReadLine());

        var account = client.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

        if (account != null)
        {
            if (account.Balance != 0)
            {
                Console.WriteLine($"На счету {account.Balance},для удаления счета необходимо его обнулить");
                Console.WriteLine("Обнулить счет(да/нет):");
                string answer = Console.ReadLine();
                if (answer == "да")
                {
                    Withdraw(client);
                    client.Accounts.Remove(account);
                    Console.WriteLine();
                    Console.WriteLine($"Счет {account.Currency} счет {accountNumber} удален.");
                    ClientAccountInfo(client);
                }
            }
            else if (account.Balance == 0)
            {
                client.Accounts.Remove(account);
                Console.WriteLine();
                Console.WriteLine($"Счет {account.Currency} счет {accountNumber} удален.");
                ClientAccountInfo(client);
            }
        }
        else Console.WriteLine("Счет не найден.");
    }

    static void RemoveClient(List<Client> clients, string searchKey)
    {
        var client = FindClient(clients, searchKey);

        if (client != null)
        {
            Console.WriteLine($"Вы точно хотите удалить клиента {client.FIO}?(да/нет)");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "да")
            {
                clients.Remove(client);
                Console.WriteLine();
                Console.WriteLine($"Клиент {client.FIO} удален.");
            }
        }
        else Console.WriteLine("Клиент не найден.");
    }

    static List<Client> Clients()
    {
        var clients = new List<Client>
        {
            new Client
            {
                FIO = "Николаев Николай Николаевич",
                PassportNumber = "1111111111",
                Accounts = new List<Account>
                {
                   new Account { AccountNumber = 1, Currency = "RUB PMR", Balance = 13412.43, IsLocked = false }
                }
            },
            new Client
            {
                FIO = "Дмитриев Дмитрий Дмитриевич",
                PassportNumber = "222222222",
                Accounts = new List<Account>
                {
                    new Account { AccountNumber = 1, Currency = "RUB PMR", Balance = 5230.00, IsLocked = false }
                }
            },
            new Client
            {
                FIO = "Петров Пётр Петрович",
                PassportNumber = "3333333333",
                Accounts = new List<Account>
                {
                  new Account { AccountNumber = 1, Currency = "RUB PMR", Balance = 15040.10, IsLocked = false },
                }
            },
            new Client
            {
                 FIO = "Иванов Иван Иванович",
                 PassportNumber = "4444444444",
                 Accounts = new List<Account>
                 {
                   new Account { AccountNumber = 1, Currency = "RUB PMR", Balance = 7189.24, IsLocked = false },

                 }
            },
        };
        return clients;
    }

    static List<Employee> Employees()
    {
        var employees = new List<Employee>
        {
           new Employee { FIO = "Манкаш Василий Сергеевич", ID = "EMP111", PhoneNumber = 77999216, Department = "IT" },
           new Employee { FIO = "Качурка Даниил Александрович", ID = "EMP123", PhoneNumber = 77948498, Department = "IT" },
           new Employee { FIO = "Сидоров Валерий Витальевич", ID = "EMP883", PhoneNumber = 77583791, Department = "Продажи" },
        };
        return employees;
    }
}