using System;

namespace maintenance_app
{
    public class MaintenanceFileManager
    {
        public void SaveMaintenanceItems(string fileName, List<MaintenanceItem> items)
        {
            string line = "";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var item in items)
                {
                    line = $"{item.Id},{item.Name},{item.MaintenanceIntervalDays},{item.LastMaintenanceDate}|-|";

                    foreach (var history in item.History)
                    {
                        line += $"{history.MaintenanceDate}=={history.Notes},";
                    }
                    writer.WriteLine(line);
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
}