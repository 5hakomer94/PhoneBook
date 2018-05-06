using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PhonebookProgram
{
    public class PhoneBook
    {
        private Contact contacts;
        public PhoneBook()
        {
            this.contacts = new Contact();
        }
        private string[] _menu = new string[]
        { "1. Show contacts","2. Add new contact","3. Search for contact","4. Edit contact","5. Remove Contact","6. Exit"};
        public void ShowMenu()
        {
            Console.WriteLine("Please enter number of task:");
            foreach (var i in this._menu)
            {
                Console.WriteLine(i);
            }
        }
        public bool SelectMenu()
        {
            this.ShowMenu();
            var userChoice = Int16.Parse(Console.ReadLine());
            Console.Clear();
            switch (userChoice)
            {
                case 1:
                    while (true)
                    {
                        this.contacts.ShowList();
                        Console.WriteLine("Show menu? (Y/N)");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            Console.Clear();
                            break;
                        }
                        Console.Clear();
                    }
                    break;
                case 2:
                    while (true)
                    {
                        this.contacts.ShowList();
                        Console.WriteLine("----------------------------------------------------");
                        Console.Write("Enter Name:\t");
                        string name = Console.ReadLine();
                        Console.Write("Enter Family:\t");
                        string family = Console.ReadLine();
                        Console.Write("Enter Phone:\t");
                        string phone = Console.ReadLine();
                        this.contacts.AddPerson(name, family, phone);
                        Console.WriteLine("Add more? (Y/N)");
                        if (Console.ReadLine().ToLower() == "n")
                        {
                            Console.Clear();
                            break;
                        }
                        Console.Clear();
                    }
                    break;
                case 3:
                    while (true)
                    {
                        Console.WriteLine("Enter person's phone number:");
                        var phoneNo = Console.ReadLine();
                        Person p;
                        int i;
                        this.contacts.SearchPersonInList(phoneNo, out p, out i);
                        if (p.Phone == phoneNo)
                        {
                            Console.WriteLine("Name\t|Family\t|Phone");
                            Console.WriteLine(p);
                        }
                        else
                            Console.WriteLine("Person is not found.");

                        Console.WriteLine("Find another person? (Y/N)");
                        if (Console.ReadLine().ToLower() == "n")
                        {
                            Console.Clear();
                            break;
                        }
                        Console.Clear();
                    }
                    break;
                case 4:
                    while (true)
                    {
                        if (this.contacts.List.Length == 0)
                        {
                            Console.WriteLine("No items available");
                            Console.WriteLine("Show menu? (Y/N)");
                            if (Console.ReadLine().ToLower() == "y")
                            {
                                Console.Clear();
                                break;
                            }
                            Console.Clear();
                            continue;
                        }
                        this.contacts.ShowList();
                        Console.WriteLine("----------------------------------------------------");
                        Console.Write("Enter row number of item you want to edit:\t");
                        var index = Int16.Parse(Console.ReadLine())-1;
                        if (!(index.GetType()== typeof(int))|| index < 0 || index >= this.contacts.List.Length)
                        {
                            Console.Write("Please enter one of the numbers in the list above.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            continue;
                        }
                        Person tempPerson = this.contacts.List[index];
                        Console.Write("Enter Name: (Leave blank to use current Name)\t");
                        var tempPersonName = Console.ReadLine();
                        Console.Write("Enter Family: (Leave blank to use current Family)\t");
                        var tempPersonFamily = Console.ReadLine();
                        Console.Write("Enter Phone: (Leave blank to use current Phone)\t");
                        var tempPersonPhone = Console.ReadLine();
                        tempPerson.Name = tempPersonName == "" ? tempPerson.Name : tempPersonName;
                        tempPerson.Family = tempPersonFamily == "" ? tempPerson.Family : tempPersonFamily;
                        tempPerson.Phone = tempPersonPhone == "" ? tempPerson.Phone : tempPersonPhone;
                        this.contacts.EditListPerson(tempPerson, index);
                        Console.WriteLine("Edit another contact? (Y/N)");
                        if (Console.ReadLine().ToLower() == "n")
                        {
                            Console.Clear();
                            break;
                        }
                        Console.Clear();
                    }
                    break;
                case 5:
                    while (true)
                    {
                        if (this.contacts.List.Length == 0)
                        {
                            Console.WriteLine("No items available");
                            Console.WriteLine("Show menu? (Y/N)");
                            if (Console.ReadLine().ToLower() == "y")
                            {
                                Console.Clear();
                                break;
                            }
                            Console.Clear();
                            continue;
                        }
                        this.contacts.ShowList();
                        Console.WriteLine("----------------------------------------------------");
                        Console.Write("Enter row number of item you want to edit:\t");
                        var rowNum = Int16.Parse(Console.ReadLine())-1;
                        if (!(rowNum.GetType() == typeof(int)) || rowNum < 0 || rowNum >= this.contacts.List.Length)
                        {
                            Console.Write("Please enter one of the numbers in the list above.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            continue;
                        }
                        this.contacts.RemovePerson(rowNum);
                        Console.WriteLine("Remove another contact? (Y/N)");
                        if (Console.ReadLine().ToLower() == "n")
                        {
                            Console.Clear();
                            break;
                        }
                        Console.Clear();
                    }
                    break;
                case 6:
                    return true;
                default:
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write("Please enter the correct number");
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Thread.Sleep(1000);
                    Console.Write(new String(' ', "Please enter the correct number".Length));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    return SelectMenu();
            }
            return false;
        }
        public void StartPhoneBook()
        {
            Console.WriteLine("Welcome to phonebook.\n");
            Console.WriteLine("Press any key to continue...");
            Console.CursorVisible = false;
            Console.ReadKey();
            Console.Clear();
            Console.CursorVisible = true;
            while (true)
            {
                if (SelectMenu())
                {
                    break;
                }
            }
        }

    }
    public class Person
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }
        public override string ToString()
        {
            return $"{this.Name}\t|{this.Family}\t|{this.Phone}";
        }
    }
    public class Contact
    {
        private Person[] _list;

        public Person[] List
        {
            get { return _list; }
            set { _list = value; }
        }


        public Contact()
        {
            this._list = new Person[0];
        }
        public void ShowList()
        {
            Console.WriteLine("#\t|Name\t|Family\t|Phone");
            for (int i = 0; i < _list.Length; i++)
            {
                Console.WriteLine("{0}\t|{1}", i + 1, _list[i]);
            }
        }
        public void AddPerson(string name, string family, string phone)
        {
            Person[] extendedList = new Person[_list.Length + 1];
            Person p = new Person
            {
                Name = name,
                Family = family,
                Phone = phone
            };
            for (int i = 0; i < _list.Length; i++)
            {
                extendedList[i] = _list[i];
            }
            extendedList[extendedList.Length - 1] = p;
            _list = extendedList;
        }
        public void RemovePerson(int index)
        {
            Person[] shortenedList = new Person[_list.Length - 1];
            int i = 0;
            for (; i < _list.Length; i++)
            {
                if (i == index)
                {
                    i++;
                    break;
                }
                shortenedList[i] = _list[i];
            }
            for (; i < _list.Length; i++)
            {
                shortenedList[i - 1] = _list[i];
            }
            _list = shortenedList;

        }
        public void SearchPersonInList(string phoneNo, out Person p, out int i)
        {
            for (i = 0; i < _list.Length; i++)
            {
                if (_list[i].Phone == phoneNo)
                {
                    p = _list[i];
                    return;
                }
            }
            p = new Person();
            //TODO: check if (p.phone == PhoneNo)
        }
        public void SearchPersonInList(string phoneNo, out int i)
        {
            for (i = 0; i < _list.Length; i++)
            {
                if (_list[i].Phone == phoneNo)
                {
                    return;
                }
            }
        }
        public void EditListPerson(Person p, int i)
        {
            _list[i] = p;
        }
    }
}
