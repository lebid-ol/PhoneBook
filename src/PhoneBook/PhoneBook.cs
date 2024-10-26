using System.Runtime.CompilerServices;

namespace PhoneBook;

public class PhoneBook
{
    public PhoneBook(List<Contact> contacts)
    {
        Contacts = contacts;
    }

    public PhoneBook()
    {
        Contacts = new List<Contact>();
    }


    public List<Contact> Contacts { get; set; }
    public IConsoleReader ConsoleReader = new ConsoleReader();
    
    public void AddContact()
    {
       
        ConsoleReader.Write("Введите имя: ");
        string firstName = ConsoleReader.ReadLine();        
        
        ConsoleReader.Write("Введите фамилию: ");
        string lastName = ConsoleReader.ReadLine();

        ConsoleReader.Write("Введите номер телефона: ");
        string phoneNumber = ConsoleReader.ReadLine().Trim();

        Contacts.Add(new Contact(firstName, lastName, phoneNumber));

        ConsoleReader.WriteLine("Контакт добавлен.");


    }

    public void ViewContacts()
    {
        if (Contacts.Count == 0)
        {
            ConsoleReader.WriteLine("Список контактов пуст.");
        }
        else
        {
            int i = 1;
            foreach (Contact contact in Contacts)
            {
                
                ConsoleReader.WriteLine(i +". " + "Имя: " + contact.FirstName + " Фамилия: " + contact.LastName + " Номер телефона: " + contact.PhoneNumber);
                i++;
            }
        }
    }

    public void UpdateContact()
    {
       ConsoleReader.Write("Введите номер телефона контакта, который хотите обновить: ");
       string chosePhoneNumber = ConsoleReader.ReadLine().Trim();

        bool contactFound = false;

        foreach (Contact contact in Contacts)
        {
            if (contact.PhoneNumber == chosePhoneNumber)
            {
                ConsoleReader.Write("Введите новое имя: ");
                string newFirstName = ConsoleReader.ReadLine();
                contact.FirstName = newFirstName;

                ConsoleReader.Write("Введите новую фамилию: ");
                string newLastName = ConsoleReader.ReadLine();
                contact.LastName = newLastName;

                ConsoleReader.WriteLine("Контакт обновлен.");

                contactFound = true;

                break;
            }
        }

            if (!contactFound) {

                ConsoleReader.WriteLine("Контакт с таким номером телефона не найден.");
            }
    }

    public void DeleteContact()
    {
        ConsoleReader.Write("Введите номер телефона контакта, который хотите удалить: ");
        string deletePhoneNumber = ConsoleReader.ReadLine().Trim();

        bool contactForDelete = false;

        foreach (Contact contact in Contacts)
        {
            if (contact.PhoneNumber == deletePhoneNumber)
            {
                Contacts.Remove(contact);

                ConsoleReader.WriteLine("Контакт удален.");

                contactForDelete = true;

                break;
            }
        }

        if (!contactForDelete)
        {
           ConsoleReader.WriteLine("Контакт с таким номером телефона не найден.");
        }
    }

    public void SearchContact()
    {
        ConsoleReader.Write("Введите имя или номер телефона для поиска: ");
        string tryFindeContact = ConsoleReader.ReadLine();

        bool contactForSearch = false;

        foreach (Contact contact in Contacts)
        {
            if (contact.PhoneNumber.StartsWith(tryFindeContact) || contact.FirstName.StartsWith(tryFindeContact))
            {
                ConsoleReader.WriteLine("Имя: " + contact.FirstName + " Фамилия: " + contact.LastName + " Номер телефона: " + contact.PhoneNumber);

                contactForSearch = true;
            }
        }

        if (!contactForSearch)
        {
            ConsoleReader.WriteLine("Контакты не найдены.");
        }

    }

    public void SaveBook()
    {
        using (StreamWriter writer = new StreamWriter("contacts.txt"))
        {
            foreach (Contact contact in Contacts)
            {
                writer.WriteLine(contact.FirstName+ "," +contact.LastName + "," + contact.PhoneNumber);
            }
        }

        ConsoleReader.WriteLine("Книга сохранена.");
    }

    public void LoadBook()
    {
        string filePath = "D:/Образование/Progects_C#/PhoneBook/src/PhoneBook/bin/Debug/net6.0/contacts.txt";

            if (!File.Exists(filePath))
            {
               ConsoleReader.WriteLine("Файл не найден.");
               return;
            }

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                  string firstName = parts[0].Trim();
                  string lastName = parts[1].Trim();
                  string phoneNumber = parts[2].Trim();
                  Contact contact = new Contact(firstName, lastName, phoneNumber);
                  Contacts.Add(contact);
                }
                else
                {
                Console.WriteLine("Неверный формат строки: " + line);
                }
                line = reader.ReadLine();
            }

        }

        Console.WriteLine("Загрузка контактов завершена.");
      
    }
}
