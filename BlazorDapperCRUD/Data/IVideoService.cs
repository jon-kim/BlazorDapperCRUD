using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDapperCRUD.Data
{
    public interface IVideoService
    {
        Task<bool> VideoDelete(int videoID);
        Task<Video> VideoGetOne(int videoID);
        Task<bool> VideoInsert(Video video);
        Task<IEnumerable<Video>> VideoList();
        Task<bool> VideoUpdate(Video video);
    }
}