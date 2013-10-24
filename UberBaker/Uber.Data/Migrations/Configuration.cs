namespace Uber.Data
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using Uber.Core;

    internal sealed class Configuration : DbMigrationsConfiguration<UberContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
			this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}