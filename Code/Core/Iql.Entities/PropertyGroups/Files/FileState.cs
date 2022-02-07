using Iql.Serialization;
using Newtonsoft.Json;

namespace Iql.Entities.PropertyGroups.Files
{
    public class FileState : IFileState
    {
        public string UploadedByUserId { get; set; }
        public string UploadedFromDeviceId { get; set; }
        public bool IsUploading { get; set; }
        public string Url { get; set; }
        public bool DeleteAfterUpload { get; set; }
        public string ContentType { get; set; }

        public static IFileState TryGetFileState(IProperty stateProperty, object entity)
        {
            if (entity != null && stateProperty != null)
            {
                var stateValue = (string)stateProperty.GetValue(entity);
                if (!string.IsNullOrWhiteSpace(stateValue))
                {
                    try
                    {
                        var state = JsonConvert.DeserializeObject<FileState>(stateValue);
                        return state;
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }

        public static bool TrySetFileState(IProperty stateProperty, object entity, IFileState state)
        {
            if (entity != null && stateProperty != null)
            {
                try
                {
                    if (state == null)
                    {
                        stateProperty.SetValue(entity, null);
                    }
                    else
                    {
                        var stateJson = IqlJsonSerializer.Serialize(state);
                        stateProperty.SetValue(entity, stateJson);
                    }
                    return true;
                }
                catch
                {
                }
            }
            return false;
        }
    }
}