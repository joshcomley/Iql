namespace Iql.Entities.PropertyGroups.Files
{
    public interface IFileState
    {
        string ContentType { get; set; }
        bool IsUploading { get; set; }
        string UploadedByUserId { get; set; }
        string UploadedFromDeviceId { get; set; }
        string Url { get; set; }
        bool DeleteAfterUpload { get; set; }
    }
}