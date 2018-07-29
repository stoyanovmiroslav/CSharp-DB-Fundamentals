namespace PhotoShare.Services.Contracts
{
    using Models;
    using PhotoShare.Models.Enums;

    public interface IAlbumRoleService
    {
        AlbumRole PublishAlbumRole(int albumId, int userId, Role role);

        bool Exists(int albumId, int userId);

        TModel ByIds<TModel>(int albumId, int userId);
    }
}
