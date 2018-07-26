namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface IAlbumTagService
    {
        AlbumTag AddTagTo(int albumId, int tagId);

        TModel ByIds<TModel>(int tagId, int albumId);

        bool Exists(int albumId, int tagId);

    }
}
