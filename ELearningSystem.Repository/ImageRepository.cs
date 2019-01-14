using ELearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Repository
{
    public class ImageRepository : BaseRepository
    {

        public Image GetItemImage(int itemId)
        {
            return QueryFirst<Image>("spGetImage",
                new
                {
                    @lectureId = itemId
                });
        }

        public List<Image> GetItemImages(int lectureId)
        {
            return QueryMultiple<Image>("spGetImages",
                new
                {
                    @lectureId = lectureId
                });
        }

        public Image GetImage(int Id)
        {
            return QueryFirst<Image>("GetImage",
                new
                {
                    @Id = Id
                });
        }

        public Image InsertImage(Image image, int lectureId)
        {
            return QueryFirst<Image>("spInsertImage", new
            {
                @id = image.Id,
                @name = image.Name,
                @data = image.Data,
                @length = image.Length,
                @contentType = image.ContentType,
                @lectureId = lectureId
            });
        }

        //public List<VideoFile> AddNewVideoFile(string name, int fileSize, string filePath, int lectureId)
        //{
        //    return QueryMultiple<VideoFile>("spAddNewVideoFile",
        //       new
        //       {
        //           @Name = name,
        //           @pFileSize = fileSize,
        //           @pFilePath = filePath,
        //           @plectureId = lectureId
        //       });
        //}

        //public List<VideoFile> GetAllVideoFile(int lectureId)
        //{
        //    return QueryMultiple<VideoFile>("spGetAllVideoFile",
        //       new
        //       {
        //           @plectureId = lectureId
        //       });
        //}
    }
}