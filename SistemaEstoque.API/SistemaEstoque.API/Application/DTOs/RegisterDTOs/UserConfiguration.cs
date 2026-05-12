namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class UserConfiguration
    {
        public long Id { get; set; }
        public bool PermissionCreate { get; set; }
        public bool PermissionDelete { get; set; }
        public bool PermissionEdit { get; set; }
        public bool PermissionInactive { get; set; }
        public bool PermissionApprove { get; set; }
        public bool PermissionExport { get; set; }
        public bool PermissionControlUser { get; set; }
    }
}
