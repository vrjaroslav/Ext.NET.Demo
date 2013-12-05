using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Services.Services
{
    public class PermissionsService : IPermissionsService
    {
        private IBaseRepository<Role> repository { get; set; }
        private IBaseRepository<User> usersRepository { get; set; }

		#region Constructors

		public PermissionsService()
		{
			repository = new RolesRepository();
            usersRepository = new UsersRepository();
		}

        public PermissionsService(IBaseRepository<Role> repository, IBaseRepository<User> usersRepository)
		{
            this.repository = repository;
            this.usersRepository = usersRepository;
		}

        #endregion

        public List<string> GetSecurableObjects()
        { 
            var result = from type in Assembly.Load("Uber.Core").GetTypes()
                where Attribute.IsDefined(type, typeof(Securable))
                select type.Name;
            return result.ToList();
        }

        public Dictionary<string, Dictionary<string, bool>> GetSecurableDictionaryByUserName(string userName)
        {
            User user = usersRepository.GetAll().FirstOrDefault(u => u.UserName == userName);

            if (user == null)
                throw new ApplicationException("Couldn't find a user with the name " + userName);

            return GetSecurableDictionaryByRoleName(user.Role.Name);
        }

        public Dictionary<string, Dictionary<string, bool>> GetSecurableDictionaryByRoleName(string roleName)
        {
            Role role = repository.GetAll().FirstOrDefault(o => o.Name == roleName);
            if (role == null)
            {
                throw new ApplicationException("Couldn't find role with the name " + roleName);
            }

            var result = new Dictionary<string, Dictionary<string, bool>>();
            List<string> objects = this.GetSecurableObjects();

            foreach (var item in objects)
            {
                result.Add(item, GetPermissionListForObject(role, item));
            }

            return result;
        }

        public Dictionary<string, Dictionary<string, bool>> GetSecurableDictionaryByRoleId(int roleId)
        {
            Role role = repository.Get(roleId);
            if (role == null)
            {
                throw new ApplicationException("Couldn't find role with the Id " + roleId);
            }

            var result = new Dictionary<string, Dictionary<string, bool>>();
            List<string> objects = this.GetSecurableObjects();

            foreach (var item in objects)
            {
                result.Add(item, GetPermissionListForObject(role, item));
            }

            return result;
        }

        private Dictionary<string, bool> GetPermissionListForObject(Role role, string objectType)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            
            List<string> permissions = role.Permisions
                    .Where(p => p.ObjectType == objectType)
                    .Select(p => p.PermissionType.ToString()).ToList();

            // We want to create a full list for all Permissions even if Role doesn't have some of them
            foreach (var type in Enum.GetValues(typeof (PermissionType)))
            {
                result.Add(type.ToString(), permissions.Contains(type.ToString()));
            }
            
            return result;
        }

        public bool CheckPermission(string userName, string objectType, string permissionType)
        {
            User user = usersRepository.GetAll().FirstOrDefault(u => u.UserName == userName);

            if (user == null)
                throw new ApplicationException("Couldn't find a user with the name " + userName);

            return CheckPermission(user.Role, objectType, permissionType);
        }

        public bool CheckPermission(Role role, string objectType, string permissionType)
        {
            PermissionType requriedPermission;
            if (!Enum.TryParse<PermissionType>(permissionType, out requriedPermission))
            {
                throw new ApplicationException("Couldn't parse passed Permission, check the spelling");
            }

            return CheckPermission(role, objectType, requriedPermission);
        }

        public bool CheckPermission(Role role, string objectType, PermissionType permissionType)
        {
            return repository.Get(role.Id).Permisions.FirstOrDefault(p => p.PermissionType == permissionType && p.ObjectType == objectType) != null;
        }

        public bool CheckPermision<TObject>(Role role, string permissionType)
        {
            PermissionType requriedPermission;
            if (!Enum.TryParse<PermissionType>(permissionType, out requriedPermission))
            {
                throw new ApplicationException("Couldn't parse passed Permission, check the spelling");
            }
            return CheckPermission(role, typeof(TObject), requriedPermission);
        }

        public bool CheckPermision<TObject>(Role role, PermissionType permissionType)
        {
            return CheckPermission(role, typeof(TObject), permissionType);
        }

        public bool CheckPermission(Role role, Type objectType, PermissionType permissionType)
        {
            return repository.Get(role.Id).Permisions.FirstOrDefault(p => p.PermissionType == permissionType && p.ObjectType == objectType.Name) != null;
        }

        
    }
}
