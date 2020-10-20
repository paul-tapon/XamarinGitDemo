using System.ComponentModel.DataAnnotations.Schema;

namespace BAI.Adir.Api.Domain.Model
{
    public class ContentFile
    {
        public int ContentFileId { get; set; }

        [ForeignKey("ContentFileType")]
        public int ContentFileTypeId { get; set; }

        public virtual ContentFileType ContentFileType { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public decimal FileSize { get; set; }

    }

}