using System;
using System.Collections.Generic;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Services
{
    public class RolesService : IRolesService
    {
        private IBaseRepository<Role> repository { get; set; }
        private IBaseRepository<Permission> permissionRepository { get; set; }

		#region Constructors

		public RolesService()
		{
			repository = new RolesRepository();
            permissionRepository = new PermissionsRepository();
		}

        public RolesService(IBaseRepository<Role> repository, IBaseRepository<Permission> permissionRepository)
		{
            this.repository = repository;
            this.permissionRepository = permissionRepository;
		}

		#endregion

        #region Data methods

        public List<Role> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public void Disable(int id)
        {
            var profile = repository.Get(id);
            profile.Disabled = true;
            repository.Update(profile);
        }

        public Role Save(Role role, Dictionary<string, List<string>> permissions)
        {
            var r = repository.Get(role.Id);
            r.Name = role.Name;

            var deletedPermissions = r.Permisions.ToList();
            r.Permisions.Clear();

            foreach (string objectType in permissions.Keys)
            {
                foreach (string permission in permissions[objectType])
                {
                    r.Permisions.Add(new Permission
                    {
                        ObjectType = objectType,
                        PermissionType = (PermissionType)Enum.Parse(typeof(PermissionType), permission)
                    });
                }
            }

            repository.AddOrUpdate(r);

            // Clear database
            foreach (var p in deletedPermissions)
            {
                permissionRepository.Delete(p.Id);
            }

            return r;
        }

        public Role Get(int id)
        {
            return repository.Get(id);
        }

        #endregion
    }
}
