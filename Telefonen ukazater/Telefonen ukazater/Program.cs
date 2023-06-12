using System.IO;

namespace Telefonen_ukazater
{
    internal class Program
    {
        /*public static void Search(List<Contact> contacts, string option) 
        {
            var findNames = new List<Contact>();
            Console.WriteLine("Name of contact:");
            var searched = Console.ReadLine();
            if (string.IsNullOrEmpty(searched))
            {
                findNames = contacts.FindAll(x => x.Name.Contains(searched));

                if (findNames.Count > 0)
                {
                    Console.WriteLine("\nContacts found:");
                    foreach (var cont in findNames)
                    {
                        Console.WriteLine("{0}. Name: {1}, Number: {2}, Email: {3}", cont.Id, cont.Name, cont.Number, cont.Email);
                    }

                    switch (option)
                    {
                        case "4":
                        case "5":
                            if (int.TryParse(Console.ReadLine(), out int selectedContactIndex) && selectedContactIndex >= 1 && selectedContactIndex <= findNames.Count)
                                
                            {
                                var selectedContact = findNames[selectedContactIndex - 1];
                                if (option == "4")
                                {
                                    contacts.Remove(selectedContact);
                                    Console.WriteLine("Contact '{0}' removed", selectedContact.Name);
                                }
                                else
                                {
                                    Console.WriteLine("What to edit?\n" + "1. Name\n" + "2. Number\n" + "3. Email\n" + "x for Exit");
                                    var userInput = Console.ReadLine();
                                    do
                                    {
                                        switch (userInput)
                                        {
                                            case "1":
                                                Console.WriteLine("Write new name:");
                                                selectedContact.Name = Console.ReadLine();
                                                Console.WriteLine("Name edited");
                                                break;

                                            case "2":
                                                Console.WriteLine("Write new number:");
                                                Console.WriteLine("Contact number:");
                                                ulong.TryParse(Console.ReadLine(), out ulong selectedNumber);
                                                selectedContact.Number = selectedNumber;
                                                Console.WriteLine("Number edited");
                                                break;

                                            case "3":
                                                Console.WriteLine("Write new email:");
                                                selectedContact.Email = Console.ReadLine();
                                                Console.WriteLine("Email edited");
                                                break;

                                            default:
                                                Console.WriteLine("Unvalid operation!");
                                                break;
                                        }

                                        Console.WriteLine("What to edit?\n" + "1. Name\n" + "2. Number\n" + "3. Email\n" + "x for Exit");
                                        userInput = Console.ReadLine();

                                    } while (userInput != "x");
                                }
                            }
                            else if (selectedContactIndex == 0)
                            {
                                Console.WriteLine("Operation canceled.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Operation canceled.");
                            }

                            Console.WriteLine("");
                            break;
                    }
                }
                else Console.WriteLine("No such name");
            }
            else
            {
                Console.WriteLine("No search input");
            }

            

        }*/

        static List<Contact> contacts = new();

        public static Contact Search(bool returnContact = false)
        {
            var findNames = new List<Contact>();
            Console.WriteLine("Name of contact:");
            var name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                findNames = contacts.FindAll(x => x.Name.Contains(name));

                if (findNames.Count > 0)
                {
                    Console.WriteLine("\nContacts found:");
                    foreach (var cont in findNames)
                    {
                        Console.WriteLine("{0}. Name: {1}, Number: {2}, Email: {3}", cont.Id, cont.Name, cont.Number, cont.Email);
                    }

                    if (!returnContact)
                    {
                        return null;
                    }

                    if (int.TryParse(Console.ReadLine(), out int selectedContactIndex) && selectedContactIndex >= 1 && selectedContactIndex <= findNames.Count)
                    {
                        return contacts.FirstOrDefault(c => c.Id == selectedContactIndex);
                    }
                    else if (selectedContactIndex == 0)
                    {
                        Console.WriteLine("Operation canceled.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Operation canceled.");
                    }
                }
                else
                {
                    Console.WriteLine("No such name");
                }

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("No search input");
            }

            return null;
        }

        static void Main(string[] args)
        {
            string filePath = @"C:\Users\VSC2_\Documents\Bogomil\text.txt";

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(','); // Assuming each line has the format: "Id, Name, Number, Email"
                if (parts.Length == 4)
                {
                    if (int.TryParse(parts[0], out int id) && ulong.TryParse(parts[2], out ulong number))
                    {
                        Contact contact = new Contact()
                        {
                            Id = id,
                            Name = parts[1],
                            Number = number,
                            Email = parts[3]
                        };

                        contacts.Add(contact);
                    }
                }
            }
            Console.WriteLine(contacts[0].Name);

            Console.WriteLine("Phonebook\n" + "Select:\n" + "1. Add Contact\n" + "2. See Contacts\n" + "3. Search for contact\n" + "4. Remove contact\n" + "5. Edit contact\n" + "x for Exit ");

            var userInput = Console.ReadLine();
            Console.WriteLine();
            int idCount = 1;
            
            while (true)
            {
                Contact contactResult = null;
                switch (userInput)
                {
                    case "1":
                        Contact contact = new Contact();
                        contact.Id = idCount++;

                        Console.WriteLine("Contact name:");
                        contact.Name = Console.ReadLine();

                        Console.WriteLine("Contact number:");
                        if (!ulong.TryParse(Console.ReadLine(), out ulong Number))
                        {
                            Console.WriteLine("Only numbers allowed.\n");
                            
                            break;
                        }
                        else
                        {
                            contact.Number = Number;
                        }

                        Console.WriteLine("Contact Email:");
                        contact.Email = Console.ReadLine();

                        contacts.Add(contact);
                        using (StreamWriter writer = File.AppendText(filePath))
                        {
                            string line = $"{contact.Id}, {contact.Name}, {contact.Number}, {contact.Email}";
                            writer.WriteLine(line);
                        }

                        break;

                    case "2":
                        Console.WriteLine();
                        foreach (var cont in contacts)
                        {
                            Console.WriteLine("{0}. Name: {1}, Number: {2}, Email: {3}", cont.Id, cont.Name, cont.Number, cont.Email);
                        }
                        break;

                    case "3":
                        Search();
                        break;

                    case "4":
                        contactResult = Search(true);

                        if (contactResult != null)
                        {
                            contacts.Remove(contactResult);
                            Console.WriteLine("Contact '{0}' removed", contactResult.Name);
                        }
                        break;

                    case "5":
                        contactResult = Search(true);

                        Console.WriteLine("What to edit?\n" + "1. Name\n" + "2. Number\n" + "3. Email\n" + "x for Exit");
                        userInput = Console.ReadLine();
                        do
                        {
                            switch (userInput)
                            {
                                case "1":
                                    Console.WriteLine("Write new name:");
                                    contactResult.Name = Console.ReadLine();
                                    Console.WriteLine("Name edited");
                                    break;

                                case "2":
                                    Console.WriteLine("Write new number:");
                                    Console.WriteLine("Contact number:");
                                    ulong.TryParse(Console.ReadLine(), out ulong selectedNumber);
                                    contactResult.Number = selectedNumber;
                                    Console.WriteLine("Number edited");
                                    break;

                                case "3":
                                    Console.WriteLine("Write new email:");
                                    contactResult.Email = Console.ReadLine();
                                    Console.WriteLine("Email edited");
                                    break;

                                default:
                                    Console.WriteLine("Unvalid operation!");
                                    break;
                            }

                            Console.WriteLine("What to edit?\n" + "1. Name\n" + "2. Number\n" + "3. Email\n" + "x for Exit");
                            userInput = Console.ReadLine();

                        } while (userInput != "x");

                        break;
                        
                    case "x":
                        Environment.Exit(1);
                        break;

                    default:
                        Console.WriteLine("Unvalid operation!");                        
                        break;
                }
                Console.WriteLine("\nSelect operation:\n" + "1. Add Contact\n" + "2. See Contacts\n" + "3. Search for contact \n" + "4. Remove contact\n" + "5. Edit contact\n" + "x for Exit ");
                userInput = Console.ReadLine();
            }

        }
    }
}


