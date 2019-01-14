using ELearningSystem.Models;
using ELearningSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ELearningSystem.Services
{
    public class ImageService
    {
        private ImageRepository imageRepository;

        public ImageService()
        {
            imageRepository = new ImageRepository();
        }
        public Image GetItemImage(int lectureId)
        {
            return imageRepository.GetItemImage(lectureId);
        }

        public List<Image> GetItemImages(int lectureId)
        {
            return imageRepository.GetItemImages(lectureId);
        }

        public Image GetImage(int Id)
        {
            return imageRepository.GetImage(Id);
        }

        public void UploadImageToDB(HttpPostedFileBase file, int lectureId)
        {
            Image img = new Image();
            img.ContentType = file.ContentType;
            img.Name = file.FileName;
            img.Data = ConvertToBytes(file);
            img.LectureId = lectureId;
            imageRepository.InsertImage(img, lectureId);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase Image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(Image.InputStream);
            imageBytes = reader.ReadBytes((int)Image.ContentLength);
            return imageBytes;
        }

        //public List<VideoFile> AddNewVideoFile(string name, int fileSize, string filePath, int lectureId)
        //{
        //    return imageRepository.AddNewVideoFile(name, fileSize, filePath, lectureId);
        //}

        //public List<VideoFile> GetAllVideoFile(int lectureId)
        //{
        //    return imageRepository.GetAllVideoFile(lectureId);
        //}
    }
}