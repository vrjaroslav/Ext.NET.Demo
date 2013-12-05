using System.Collections.Generic;
using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
    public class RolesRepository : IBaseRepository<Role>
    {
        private UberContext DbContext { get; set; }

		#region Constructors

		public RolesRepository() : this(new UberContext())
		{
		}

        public RolesRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

        public Role Get(int id)
		{
            return this.DbContext.Roles.Include("Permisions").SingleOrDefault(u => u.Id == id);
		}

        public IQueryable<Role> GetAll(bool includingDisabled = false)
		{
            return includingDisabled ?
                this.DbContext.Roles.Include("Permisions") :
                this.DbContext.Roles.Include("Permisions").Where(u => !u.Disabled);
		}

        public Role Add(Role role)
		{
            this.DbContext.Roles.Add(role);
            this.DbContext.SaveChanges();

            return role;
		}

        public Role Update(Role role)
		{
            this.DbContext.Entry(role).State = EntityState.Modified;
            this.DbContext.SaveChanges();

            return role;
		}

		public void Delete(int id)
		{
            Role u = Get(id);
            u.Disabled = true;
            
            this.DbContext.SaveChanges();
		}

        public Role AddOrUpdate(Role role)
		{
            return role.IsNew ? this.Add(role) : this.Update(role);
		}

        #endregion
    }
}
