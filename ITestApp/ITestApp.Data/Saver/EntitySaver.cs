using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Data.Saver
{
    public class EntitySaver : ISaver
    {
        private readonly ITestAppDbContext context;

        public EntitySaver(ITestAppDbContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public async void SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
