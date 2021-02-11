using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace BlazorDapperCRUD.Data
{
    public class VideoService : IVideoService
    {
        //Db Connection
        private readonly SqlConnectionConfiguration _configuration;
        public VideoService(SqlConnectionConfiguration configuration) => _configuration = configuration;

        public async Task<bool> VideoInsert(Video video)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", video.Title);
                parameters.Add("DatePublished", video.DatePublished);
                parameters.Add("IsActive", video.IsActive);

                await conn.ExecuteAsync("Video_Insert", parameters, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<IEnumerable<Video>> VideoList()
        {
            IEnumerable<Video> videos;

            using (var conn = new SqlConnection(_configuration.Value))
                videos = await conn.QueryAsync<Video>("Video_GetAll", commandType: CommandType.StoredProcedure);

            return videos;
        }

        public async Task<Video> VideoGetOne(int videoID)
        {
            Video video = new Video();

            var parameters = new DynamicParameters();
            parameters.Add("VideoID", videoID);

            using (var conn = new SqlConnection(_configuration.Value))
                video = await conn.QueryFirstOrDefaultAsync<Video>("Video_GetOne", parameters, commandType: CommandType.StoredProcedure);

            return video;
        }

        public async Task<bool> VideoUpdate(Video video)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", video.Title);
                parameters.Add("DatePublished", video.DatePublished);
                parameters.Add("IsActive", video.IsActive);
                parameters.Add("VideoID", video.VideoID);

                await conn.ExecuteAsync("Video_Update", parameters, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<bool> VideoDelete(int videoID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("VideoID", videoID);

            using (var conn = new SqlConnection(_configuration.Value))
                await conn.ExecuteAsync("Video_Delete", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
