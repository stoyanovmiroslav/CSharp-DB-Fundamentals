using PhotoShare.Models.Enums;

namespace PhotoShare.Client.Core.Dtos
{
    public class AlbumRoleDto
    {
        public string Username { get; set; }

        public string AlbumName { get; set; }

        public Role Role { get; set; }
    }
}
