using System.Collections.Generic;
using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
    public class PermissionsRepository : IBaseRepository<Permission>
    {
        private UberContext DbContext { get; set; }

		#region Constructors

		public PermissionsRepository() : this(new UberContext())
		{
		}

        public PermissionsRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

        public Permission Get(int id)
		{
            return this.DbContext.Permissions.Include("Role").SingleOrDefault(u => u.Id == id);
		}

        public IQueryable<Permission> GetAll(bool includingDisabled = false)
		{
            return includingDisabled ?
                this.DbContext.Permissions.Include("Role") :
                this.DbContext.Permissions.Include("Role").Where(u => !u.Disabled);
		}

        public Permission Add(Permission permission)
		{
            this.DbContext.Permissions.Add(permission);
            this.DbContext.SaveChanges();

            return permission;
		}

        public Permission Update(Permission permission)
		{
            this.DbContext.Entry(permission).State = EntityState.Modified;
            this.DbContext.SaveChanges();

            return permission;
		}

		public void Delete(int id)
		{
            Permission p = Get(id);
            this.DbContext.Permissions.Remove(p);
            this.DbContext.SaveChanges();
		}

        public Permission AddOrUpdate(Permission permission)
		{
            return permission.IsNew ? this.Add(permission) : this.Update(permission);
		}

		#endregion
    }
}
