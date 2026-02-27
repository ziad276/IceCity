using IceCity.Models;

namespace IceCity.Services
{
    public class PrintService
    {
        public void PrintLastMonthDailyUsageWithThreads(House house)
        {
            List<DailyUsage> usages = house.DailyUsages;

            Thread t1 = new Thread(() => PrintWithThreadId(usages));
            Thread t2 = new Thread(() => PrintWithThreadId(usages));

            t1.Start();
            t2.Start();


            t1.Join();
            t2.Join();
        }
        public async Task PrintLastMonthDailyUsageWithTasks(House house)
        {
            List<DailyUsage> usages = house.DailyUsages;
            Task t1 = Task.Run(() => PrintWithThreadId(usages));
            Task t2 = Task.Run(() => PrintWithThreadId(usages));

            await Task.WhenAll(t1, t2);
        }


        private void PrintWithThreadId(List<DailyUsage> usages)
        {
            

            foreach (DailyUsage usage in usages)
            {
                Console.WriteLine($"{usage.Date:yyyy-MM-dd} | HeaterValue={usage.HeaterValue} | Hours={usage.Hours}| Thread={Thread.CurrentThread.ManagedThreadId} ");
            }
        }
    }
}
