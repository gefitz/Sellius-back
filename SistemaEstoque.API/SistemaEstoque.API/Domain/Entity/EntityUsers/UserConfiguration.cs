using System.ComponentModel.DataAnnotations;

namespace Sellius.API.Domain.Entity.EntityUsers
{
    public class UserConfiguration
    {
        [Key]
        public int TpUserId { get; set; }

        public bool PermissionCreate { get; set; }
        public bool PermissionDelete { get; set; }
        public bool PermissionEdit { get; set; }
        public bool PermissionInactivate { get; set; }
        public bool PermissionApprove { get; set; }
        public bool PermissionExport { get; set; }
        public bool PermissionControlUser { get; set; }

        public TypeUser? TpUser { get; set; }
        
    }
}
