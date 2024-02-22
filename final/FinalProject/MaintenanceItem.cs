using System;

namespace maintenance_app
{
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
            LastMaintenanceDate = DateTime.Now;
            MaintenanceHistory inittialHistory = new MaintenanceHistory();
            inittialHistory.MaintenanceDate = DateTime.Now;
            inittialHistory.Notes = "Item added to list";
            History = new List<MaintenanceHistory> { inittialHistory };

        }

        public DateTime CalculateNextMaintenanceDate()
        {
            return LastMaintenanceDate.AddDays(MaintenanceIntervalDays);
        }

        public void AddMaintenanceHistory(DateTime date, string notes)
        {
            LastMaintenanceDate = date;
            // History ??= new List<MaintenanceHistory>(); // not needed
            History.Add(new MaintenanceHistory { MaintenanceDate = date, Notes = notes });
        }
    }

}