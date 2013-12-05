using System;
using System.Collections.Generic;
using Uber.Core;

namespace Uber.Services.Services
{
    public interface IPermissionsService
    {
        List<string> GetSecurableObjects();
        Dictionary<string, Dictionary<string, bool>> GetSecurableDictionaryByUserName(string userName);
        Dictionary<string, Dictionary<string, bool>> GetSecurableDictionaryByRoleName(string roleName);
        Dictionary<string, Dictionary<string, bool>> GetSecurableDictionaryByRoleId(int roleId);
        bool CheckPermission(string userName, string objectType, string permissionType);
        bool CheckPermission(Role role, string objectType, string permissionType);
        bool CheckPermission(Role role, string objectType, PermissionType permissionType);
        bool CheckPermision<TObject>(Role role, string permissionType);
        bool CheckPermision<TObject>(Role role, PermissionType permissionType);
        bool CheckPermission(Role role, Type objectType, PermissionType permissionType);
    }
}