using Microsoft.EntityFrameworkCore;
using Quartz;
using TombolaTest.Data;

namespace TombolaTest.Jobs
{
    public class BeanOfTheDayJob(DataContext context) : IJob
    {
        private readonly DataContext _context = context;
        public async Task Execute(IJobExecutionContext context)
        {
            var dbBeanCurrent = await _context.CoffeeBeans.Where(b => b.IsBOTD == true).ToListAsync();
            var isOldBean = true;
            while (isOldBean)
            {
                Random rand = new();
                int index = rand.Next(0, _context.CoffeeBeans.Count());
                var dbBeanNew = _context.CoffeeBeans.Skip(index).Take(1).First();
                if (!dbBeanNew.IsBOTD)
                {
                    dbBeanCurrent[0].IsBOTD = false;
                    dbBeanNew.IsBOTD = true;
                    isOldBean = false;
                }
                ;
            }
        }
    }
}
