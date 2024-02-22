using System;





namespace maintenance_app
{
    public class Program
    {
        private static List<MaintenanceItem> _items;
        // private static ReminderTimer _reminderTimer;
        private static MaintenanceFileManager _fileManager;

        public static bool CheckFile(string filePath)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (File.Exists(fullPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void CreateNewFile(string filePath)
        {
            try
            {
                File.Create(filePath).Close();
                Console.WriteLine($"File created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating file: {ex.Message}");
            }
        }

        public static void Main()
        {
            int operationChoice = 0;
            string fileName;
            _items = new List<MaintenanceItem>();
            _fileManager = new MaintenanceFileManager();

            Console.Write("\nWelcome to Maintenance Reminder App\n");

            do
            {
                Console.WriteLine("\nSelect 1 to open an existing maintenance list or 2 to create a new list");
                Console.Write("\n1. Open an existing file \n2. Create a new file \n\n>> ");
                if (int.TryParse(Console.ReadLine(), out int userChoice) && (userChoice == 1 || userChoice == 2))
                {
                    int choice = userChoice;

                    if (choice == 1)
                    {
                        Console.Write("Enter an existing file name: ");
                        fileName = Console.ReadLine();
                        if (CheckFile(fileName))
                        {
                            Console.WriteLine("Items loaded successfully");

                            string itemInfo;
                            string itemHistory;

                            using (StreamReader reader = new StreamReader(fileName))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    string[] itemInfoAndItemHistory = line.Split("|-|");

                                    itemInfo = itemInfoAndItemHistory[0];
                                    itemHistory = itemInfoAndItemHistory[1];

                                    string[] itemInfoDetails = itemInfo.Split(",");

                                    string itemID = itemInfoDetails[0];
                                    string itemName = itemInfoDetails[1];
                                    string itemMaintenanceInterval = itemInfoDetails[2];
                                    string itemCreatedAt = itemInfoDetails[3];


                                    //Create item object here
                                    MaintenanceItem newItem = new MaintenanceItem(int.Parse(itemID), itemName, int.Parse(itemMaintenanceInterval));

                                    List<MaintenanceHistory> newItemHistory = new List<MaintenanceHistory>();

                                    List<string> itemHistoryDetailsPairs = itemHistory.Split(",").ToList();

                                    itemHistoryDetailsPairs.ForEach(history =>
                                      {
                                          string[] historyParts = history.Split("==");

                                          if (historyParts.Length == 2)
                                          {
                                              MaintenanceHistory itemHistory = new MaintenanceHistory();

                                              DateTime dateTime;

                                              if (DateTime.TryParse(historyParts[0], out dateTime))
                                              {
                                                  itemHistory.MaintenanceDate = dateTime;
                                              }

                                              itemHistory.Notes = historyParts[1];

                                              newItemHistory.Add(itemHistory);
                                          }
                                      });

                                    newItem.History = newItemHistory;
                                    _items.Add(newItem);
                                }

                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"File {fileName} does not exist");
                            continue;
                        }
                    }
                    else
                    {
                        Console.Write("Enter a new file name: ");
                        fileName = Console.ReadLine();

                        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                        CreateNewFile(fullPath);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Try again");
                }
            } while (true);

            Console.WriteLine($"Working file name is: {fileName}");

            while (operationChoice != 5)
            {

                do
                {
                    operationChoice = GetChoice();

                    if (operationChoice < 1 || operationChoice > 5)
                    {
                        Console.WriteLine($"Invalid input. Enter a number between 1 - 5");
                    }
                    else if (operationChoice == 1)
                    {
                        ListItems();
                    }
                    else
                    {
                        switch (operationChoice)
                        {
                            case 2:
                                AddNewItem();
                                break;
                            case 3:
                                RecordMaintenanceEvent();
                                break;
                            case 4:
                                CheckMaintenanceDue();
                                break;
                            case 5:
                                SaveAndQuit(fileName);
                                break;
                            default:
                                Console.WriteLine("Invalid option. Try again.");
                                break;
                        }
                    }

                } while (operationChoice != 5);
            }
        }

        private static void ListItems()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("Maintenance list is currently empty.");
            }
            else
            {
                int itemsCount = 1;

                Console.WriteLine("List of items: ");

                _items.ForEach(item =>
                {

                    Console.WriteLine($"\n{itemsCount}. {item.Name}");

                    Console.WriteLine("Maintenance history: ");
                    item.History.ForEach(history =>
                    {
                        string note = history.Notes;
                        DateTime maintenanceDate = history.MaintenanceDate;
                        Console.WriteLine($"{maintenanceDate} - ({note})");

                    });


                    itemsCount++;
                });

                Console.WriteLine("\n== END ==");
            }

            // GetChoice();
        }

        private static void CheckMaintenanceDue()
        {
            Console.WriteLine("\nChecking for maintenance due...");
            DateTime now = DateTime.Now;
            foreach (var item in _items)
            {
                DateTime nextMaintenanceDate = item.CalculateNextMaintenanceDate();
                TimeSpan remainingTime = nextMaintenanceDate - now;
                int remainingDays = (int)remainingTime.TotalDays;
                if (remainingDays <= 0)
                {
                    Console.WriteLine($"👷‍♂️ Item '{item.Name}' is due for maintenance!");
                }
                else
                {
                    Console.WriteLine($"✅ Item '{item.Name}' is not due for maintenance. Next maintenance in: {remainingDays} days");
                }
            }
            Console.WriteLine();
        }

        private static void AddNewItem()
        {
            Console.Write("Enter an item name: ");
            string name = Console.ReadLine();

            Console.Write("Enter a maintenance interval as number (days): ");
            int intervalDays = GetIntegerInput();

            int id = _items.Count + 1;
            var newItem = new MaintenanceItem(id, name, intervalDays);
            _items.Add(newItem);

            Console.WriteLine($"Item '{name}' added successfully. ID: {id}\n");
        }

        private static void RecordMaintenanceEvent()
        {
            Console.Write("Enter item ID: ");

            int itemId = GetIntegerInput();

            var item = _items.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                Console.Write("Enter maintenance notes: ");

                string notes = Console.ReadLine();

                DateTime maintenanceDate = DateTime.Now;

                item.AddMaintenanceHistory(maintenanceDate, notes);

                Console.WriteLine($"\nMaintenance recorded for item '{item.Name}' successfully!\n");
            }
            else
            {
                Console.WriteLine($"\nItem with ID {itemId} not found.\n");
            }
        }


        private static void SaveAndQuit(string fileName)
        {
            _fileManager.SaveMaintenanceItems(fileName, _items);
            Console.WriteLine($"Items saved to file: {fileName}");
        }


        private static int GetChoice()
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. List Items");
            Console.WriteLine("2. Add a New Item");
            Console.WriteLine("3. Record a Maintenance Event");
            Console.WriteLine("4. Check Maintenance Due");
            Console.WriteLine("5. Save and Quit");
            Console.Write("\n>> ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return choice;
            }
            return 0;
        }

        private static int GetIntegerInput()
        {
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid input. Enter an integer.");
            }
            return value;
        }

    }
}