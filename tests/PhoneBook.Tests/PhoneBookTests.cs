using Moq;
using Shouldly;

namespace PhoneBook.Tests
{
    public class PhoneBookTests
    {
        readonly Mock<IConsoleReader> _mockConsoleReader;
        readonly PhoneBook _sut;
        readonly string _phoneNumber = "1234567890";
        readonly string _firstName = "John";
        readonly string _lastName = "Doe";

        public PhoneBookTests()
        {
            _sut = new PhoneBook();
            _mockConsoleReader = new Mock<IConsoleReader>();
            _mockConsoleReader.SetupSequence(cr => cr.ReadLine())
                .Returns(_firstName)
                .Returns(_lastName)
                .Returns(_phoneNumber);
            _sut.ConsoleReader = _mockConsoleReader.Object;
        }

        [Fact]
        public void AddContact_Should_Add_Contact()
        {
            // Arrange
            _sut.Contacts.Clear(); 

            // Act
            _sut.AddContact();

            // Assert
            Assert.Single(_sut.Contacts);
            var first = _sut.Contacts[0];
            Assert.Equal(_firstName, first.FirstName);
            Assert.Equal(_lastName, first.LastName);
            Assert.Equal(_phoneNumber, first.PhoneNumber);
        }

        [Fact]
        public void ViewContacts_Should_Return_Contacts()
        {
            // Arrange
            _sut.Contacts.Clear();
            _sut.AddContact();

            // Act

            _mockConsoleReader.Invocations.Clear();
            _sut.ViewContacts();
            string expectedOutput = "Имя: " + _firstName + " Фамилия: " + _lastName + " Номер телефона: " + _phoneNumber;
            _mockConsoleReader.Verify(c => c.WriteLine(It.Is<string>(str => str.Contains(expectedOutput))),Times.Exactly(1));

        }
        
        [Fact]
        public void ViewContacts_Should_Return_Empty_List()
        {
            // Arrange
            _sut.Contacts.Clear();

            // Act

            _sut.ViewContacts();

            string expectedOutput = "Список контактов пуст.";
            _mockConsoleReader.Verify(c => c.WriteLine(It.Is<string>(str => str.Contains(expectedOutput))),Times.Exactly(1));
        }
        
        [Fact]
        public void UpdateContact_Should_Update_Contact()
        {
            // Arrange
            _sut.Contacts.Clear();
            _sut.AddContact();
            _mockConsoleReader.SetupSequence(cr => cr.ReadLine())
                             .Returns(_phoneNumber)
                             .Returns("Jane")
                             .Returns("Smith");
            _sut.ConsoleReader = _mockConsoleReader.Object;
            
            using (StringWriter sw = new StringWriter())
            {
                // Act
                Console.SetOut(sw);
                _sut.UpdateContact();

                // Assert
                Assert.Equal("Jane", _sut.Contacts[0].FirstName);
                Assert.Equal("Smith", _sut.Contacts[0].LastName);
            }
        }
        
        [Fact]
        public void UpdateContact_Should_Fail_IfContactMissing()
        {
            // Arrange
            _sut.Contacts.Clear();
            _mockConsoleReader.SetupSequence(cr => cr.ReadLine())
                .Returns(_phoneNumber)
                .Returns("Jane")
                .Returns("Smith");
            _sut.ConsoleReader = _mockConsoleReader.Object;
            

            // Act
            
            _sut.UpdateContact();
            string expectedOutput = "Контакт с таким номером телефона не найден.";
            _mockConsoleReader.Verify(c => c.WriteLine(It.Is<string>(str => str.Contains(expectedOutput))),Times.Exactly(1));

        }

        [Fact]
        public void DeleteContact_ShouldDelete_Contact()
        {
            // Arrange
            _sut.Contacts.Clear();
            _sut.AddContact();
            _mockConsoleReader.SetupSequence(cr => cr.ReadLine())
                .Returns("Jane")
                .Returns("Smith")
                .Returns("9876543210");
            _sut.ConsoleReader = _mockConsoleReader.Object;
            _sut.AddContact();
            _mockConsoleReader.SetupSequence(cr => cr.ReadLine())
                .Returns(_phoneNumber);
            
            _sut.ConsoleReader = _mockConsoleReader.Object;
            
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                // Act
                _sut.DeleteContact();

                // Assert
                _sut.Contacts.Count.ShouldBe(1);
            }
        }
        
        [Fact]
        public void DeleteContact_ShouldNotDelete_ContactIfMissing()
        {
            // Arrange
            _sut.Contacts.Clear();
            _sut.AddContact();
            
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _sut.UpdateContact();
                // Act
                _sut.DeleteContact();

                // Assert
                Assert.NotEmpty(_sut.Contacts);
                var first = _sut.Contacts[0];
                Assert.Equal(_firstName, first.FirstName);
                Assert.Equal(_lastName, first.LastName);
                Assert.Equal(_phoneNumber, first.PhoneNumber);
            }
        }
        

        [Fact]
        public void SearchContact_Should_Return_Contacts()
        {
            // Arrange
            _sut.Contacts.Clear();
            _sut.AddContact();
            
            _mockConsoleReader.SetupSequence(cr => cr.ReadLine())
                .Returns(_phoneNumber)
                .Returns("Jane")
                .Returns("Smith");
            _sut.ConsoleReader = _mockConsoleReader.Object;
            _sut.AddContact();

            // Act
            _mockConsoleReader.Invocations.Clear();
            _mockConsoleReader.Setup(cr => cr.ReadLine())
                             .Returns("John");
            _sut.ConsoleReader = _mockConsoleReader.Object;
            _sut.SearchContact();

            string expectedOutput = "Имя: John Фамилия: Doe Номер телефона: 1234567890";
            _mockConsoleReader.Verify(c => c.WriteLine(It.Is<string>(str => str.Contains(expectedOutput))),Times.Exactly(1));
        }
        
        [Fact]
        public void SearchContact_ShouldReturnErrorMessage_WhenNotFound()
        {
            // Arrange
            _sut.Contacts.Clear();

            // Act
            _mockConsoleReader.Invocations.Clear();
            _mockConsoleReader.Setup(cr => cr.ReadLine())
                .Returns(_firstName);
            _sut.ConsoleReader = _mockConsoleReader.Object;
            _sut.SearchContact();

            string expectedOutput = "Контакты не найдены.";
            _mockConsoleReader.Verify(c => c.WriteLine(It.Is<string>(str => str.Contains(expectedOutput))),Times.Exactly(1));
        }
        
        [Fact]
        public void SaveBook_Should_Save_Contacts_To_File()
        {
            // Arrange
            _sut.Contacts.Clear();
            _sut.AddContact();
            _mockConsoleReader.SetupSequence(cr => cr.ReadLine())
                .Returns("Jane")
                .Returns("Smith")
                .Returns("9876543210");
            _sut.ConsoleReader = _mockConsoleReader.Object;
            _sut.AddContact();

            
            // Act
            _sut.SaveBook();

            // Assert
      
            string expectedOutput = "Книга сохранена.";
            _mockConsoleReader.Verify(c => c.WriteLine(It.Is<string>(str => str.Contains(expectedOutput))),Times.Exactly(1));

         
            // Check if the contacts were correctly saved to the file
            string[] lines = File.ReadAllLines("contacts.txt");
            Assert.Equal("John,Doe,1234567890", lines[0]);
            Assert.Equal("Jane,Smith,9876543210", lines[1]);
        }
    }
}
