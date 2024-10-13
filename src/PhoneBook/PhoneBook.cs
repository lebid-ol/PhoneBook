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
        string phoneNumber = ConsoleReader.ReadLine();

        Contacts.Add(new Contact(firstName, lastName, phoneNumber));

        ConsoleReader.WriteLine("Контакт добавлен.");


    }

    public void ViewContacts()
    {
    
            //ConsoleReader.WriteLine("Список контактов пуст.");
       
            
            //ConsoleReader.WriteLine("Имя: " + contact.FirstName + " Фамилия: " + contact.LastName + " Номер телефона: " + contact.PhoneNumber);
        
    }

    public void UpdateContact()
    {
       //ConsoleReader.Write("Введите номер телефона контакта, который хотите обновить: ");

        //ConsoleReader.WriteLine("Контакт с таким номером телефона не найден.");

        //ConsoleReader.Write("Введите новое имя: ");
        
        //ConsoleReader.Write("Введите новую фамилию: ");
        
        //ConsoleReader.WriteLine("Контакт обновлен.");
    }

    public void DeleteContact()
    {
        //ConsoleReader.Write("Введите номер телефона контакта, который хотите удалить: ");
       
        //ConsoleReader.WriteLine("Контакт с таким номером телефона не найден.");
       
        //ConsoleReader.WriteLine("Контакт удален.");
    }

    public void SearchContact()
    {
        //ConsoleReader.Write("Введите имя или номер телефона для поиска: ");
       
        //ConsoleReader.WriteLine("Контакты не найдены.");
       
        //ConsoleReader.WriteLine("Имя: " + contact.FirstName + " Фамилия: " + contact.LastName + " Номер телефона: " + contact.PhoneNumber);
        
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
        // Дополнительное задание
        // загрузка контактов из файла contacts.txt
    }
}
