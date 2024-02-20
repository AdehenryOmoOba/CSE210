// Add appropriate using statements here

public class MaintenanceItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaintenanceIntervalDays { get; set; }
    public DateTime LastMaintenanceDate { get; set; }
    public List<MaintenanceHistory> History { get; set; }

    public MaintenanceItem(int id, string name, int intervalDays)
    {
        Id = id;
        Name = name;
        MaintenanceIntervalDays = intervalDays;
        History = new List<MaintenanceHistory>();
    }

    public DateTime CalculateNextMaintenanceDate()
    {
        return LastMaintenanceDate.AddDays(MaintenanceIntervalDays);
    }

    public void AddMaintenanceHistory(DateTime date, string notes)
    {
        LastMaintenanceDate = date;
        History ??= new List<MaintenanceHistory>();
        History.Add(new MaintenanceHistory { MaintenanceDate = date, Notes = notes });
    }
}

public class MaintenanceHistory
{
    public DateTime MaintenanceDate { get; set; }
    public string Notes { get; set; }
}

public class ReminderTimer
{
    private System.Threading.Timer _timer;

    public ReminderTimer(int intervalInMinutes, Action callback)
    {
        _timer = new System.Threading.Timer(_ => callback.Invoke(), null, 0, intervalInMinutes * 60 * 1000);
    }

    public void StopTimer()
    {
        _timer?.Change(System.Threading.Timeout.Infinite, 0);
    }
}

public class MaintenanceFileManager
{
    public void SaveMaintenanceItems(string fileName, List<MaintenanceItem> items)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var item in items)
            {
                writer.WriteLine($"{item.Id},{item.Name},{item.MaintenanceIntervalDays},{item.LastMaintenanceDate}");
                if (item.History != null)
                {
                    foreach (var history in item.History)
                    {
                        writer.WriteLine($"{history.MaintenanceDate},{history.Notes}");
                    }
                }
            }
        }
    }

    public List<MaintenanceItem> LoadMaintenanceItems(string fileName)
    {
        List<MaintenanceItem> items = new List<MaintenanceItem>();

        try
        {
            if (File.Exists(fileName))
            {
                using StreamReader reader = new(fileName);
                while (!reader.EndOfStream)
                {
                    var itemData = reader.ReadLine().Split(',');

                    if (itemData.Length >= 4)
                    {
                        int id = int.Parse(itemData[0]);
                        string name = itemData[1];
                        int intervalDays = int.Parse(itemData[2]);
                        DateTime lastMaintenanceDate = DateTime.Parse(itemData[3]);

                        var item = new MaintenanceItem(id, name, intervalDays)
                        {
                            LastMaintenanceDate = lastMaintenanceDate
                        };

                        while (!reader.EndOfStream)
                        {
                            var historyData = reader.ReadLine().Split(',');
                            if (historyData.Length >= 2)
                            {
                                DateTime historyDate = DateTime.Parse(historyData[0]);
                                string notes = historyData[1];
                                item.History.Add(new MaintenanceHistory { MaintenanceDate = historyDate, Notes = notes });
                            }
                            else
                            {
                                break;
                            }
                        }

                        items.Add(item);
                    }
                }
            }
            else
            {
                Console.WriteLine($"File '{fileName}' does not exist. Creating a new file.");
                File.Create(fileName).Close();  // Create a new file
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading maintenance items: {ex.Message}");
        }

        return items;
    }
}

public class Program
{
    private static List<MaintenanceItem> _items;
    // private static ReminderTimer _reminderTimer;
    // private static MaintenanceFileManager _fileManager;

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
        int choice = 0;
        string fileName;

        Console.Write("\nWelcome to Maintenance Reminder App\n");

        do
        {
            Console.WriteLine("\nSelect 1 to open an existing maintenance list or 2 to create a new list");
            Console.Write("\n1. Open an existing file \n2. Create a new file \n\n>> ");
            if (int.TryParse(Console.ReadLine(), out int userChoice) && (userChoice == 1 || userChoice == 2))
            {
                choice = userChoice;

                if (choice == 1)
                {
                    Console.Write("Enter an existing file name: ");
                    fileName = Console.ReadLine();
                    if (CheckFile(fileName))
                    {
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

        // _items = LoadItems(fileName);
        Console.WriteLine("Items loaded successfully");
        // _reminderTimer = new ReminderTimer(1, CheckMaintenanceDue);
        // _fileManager = new MaintenanceFileManager();

        // DisplayItems();

        // Remember to remove from this line

        _items = new List<MaintenanceItem>
{
    new MaintenanceItem(1, "Generator", 30)
    {
        LastMaintenanceDate = DateTime.Now.AddDays(-10),
        History = new List<MaintenanceHistory>
        {
            new MaintenanceHistory { MaintenanceDate = DateTime.Now.AddDays(-20), Notes = "Replaced oil filter" },
            new MaintenanceHistory { MaintenanceDate = DateTime.Now.AddDays(-40), Notes = "Checked spark plug" }
        }
    },
    new MaintenanceItem(2, "Air Conditioner", 90)
    {
        LastMaintenanceDate = DateTime.Now.AddDays(-5),
        History = new List<MaintenanceHistory>
        {
            new MaintenanceHistory { MaintenanceDate = DateTime.Now.AddDays(-15), Notes = "Cleaned filters" },
            new MaintenanceHistory { MaintenanceDate = DateTime.Now.AddDays(-30), Notes = "Inspected coolant levels" }
        }
    }
};

        // To this line

        while (true)
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1. Add a New Item");
            Console.WriteLine("2. Record a Maintenance Event");
            Console.WriteLine("3. View Maintenance History");
            Console.WriteLine("4. Check for Maintenance Due");
            Console.WriteLine("5. Save and Quit");

            int operationChoice = GetChoice();

            switch (operationChoice)
            {
                case 1:
                    // AddNewItem();
                    break;
                case 2:
                    // RecordMaintenanceEvent();
                    break;
                case 3:
                    // ViewMaintenanceHistory();
                    break;
                case 4:
                    // CheckMaintenanceDue();
                    break;
                case 5:
                    // SaveAndQuit(fileName);
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            // DisplayItems();
            return;
        }
    }

    private static void CheckMaintenanceDue()
    {
        Console.WriteLine("\nChecking for maintenance due...");
        DateTime now = DateTime.Now;
        foreach (var item in _items)
        {
            DateTime nextMaintenanceDate = item.CalculateNextMaintenanceDate();
            TimeSpan remainingTime = nextMaintenanceDate - now;
            if (remainingTime <= TimeSpan.Zero)
            {
                Console.WriteLine($"Item '{item.Name}' is due for maintenance! Next maintenance in: {remainingTime}");
            }
            else
            {
                Console.WriteLine($"Item '{item.Name}' is not due for maintenance. Next maintenance in: {remainingTime}");
            }
        }
        Console.WriteLine();
    }

    private static void DisplayItems()
    {
        Console.WriteLine("\nCurrent Items:");
        foreach (var item in _items)
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Due in: {item.CalculateNextMaintenanceDate() - DateTime.Now}");
        }
        Console.WriteLine();
    }

    private static void AddNewItem()
    {
        Console.Write("Enter an item name: ");
        string name = Console.ReadLine();

        Console.Write("Enter a maintenance interval (days): ");
        int intervalDays = GetIntegerInput();

        int id = _items.Count + 1;
        var newItem = new MaintenanceItem(id, name, intervalDays);
        _items.Add(newItem);

        Console.WriteLine($"Item '{name}' added successfully. ID: {id}\n");
    }

    private static void RecordMaintenanceEvent()
    {
        Console.Write("Enter an item ID for maintenance: ");
        int itemId = GetIntegerInput();

        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            Console.Write("Enter maintenance notes: ");
            string notes = Console.ReadLine();

            DateTime maintenanceDate = DateTime.Now;
            item.AddMaintenanceHistory(maintenanceDate, notes);

            Console.WriteLine($"Maintenance recorded for item '{item.Name}' on {maintenanceDate}\n");
        }
        else
        {
            Console.WriteLine($"Item with ID {itemId} not found.\n");
        }
    }

    private static void ViewMaintenanceHistory()
    {
        Console.Write("Enter an item ID to view maintenance history: ");
        int itemId = GetIntegerInput();

        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            Console.WriteLine($"\nMaintenance History for Item '{item.Name}':");
            foreach (var history in item.History)
            {
                Console.WriteLine($"Date: {history.MaintenanceDate}, Notes: {history.Notes}");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"Item with ID {itemId} not found.\n");
        }
    }

    private static void SaveAndQuit(string fileName)
    {
        SaveItems(fileName);
        // _reminderTimer.StopTimer();
    }

    private static void SaveItems(string fileName)
    {
        // _fileManager.SaveMaintenanceItems(fileName, _items);
        Console.WriteLine($"Items saved to file: {fileName}");
    }

    // private static List<MaintenanceItem> LoadItems(string fileName)
    // {
    // List<MaintenanceItem> loadedItems = _fileManager.LoadMaintenanceItems(fileName);

    // if (loadedItems != null)
    // {
    //     _items = loadedItems.Select(item =>
    //         new MaintenanceItem(item.Id, item.Name, item.MaintenanceIntervalDays)
    //         {
    //             LastMaintenanceDate = item.History?.LastOrDefault()?.MaintenanceDate ?? DateTime.MinValue,
    //             History = item.History
    //         }).ToList();
    //     return _items;
    // }
    // else
    // {
    //     Console.WriteLine($"No items loaded from file: {fileName}. Creating a new list.");
    //     _items = new List<MaintenanceItem>();
    //     return _items;
    // }
    // }

    private static int GetChoice()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid input. Enter a number.");
        }
        return choice;
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
