
using OpenAI.Net.Models;
using System.Reflection;

namespace OpenAI.Net.Extensions
{
    public static class MultipartFormDataContentExtensions
    {
        public static void AddField(this MultipartFormDataContent formData, PropertyInfo prop, object @object)
        {
            var value = prop.GetValue(@object);

            if (value != null)
            {
                if (value is FileContentInfo)
                {
                    var fileInfo = value as FileContentInfo;
                    if (fileInfo != null)
                    {
                        formData.Add(fileInfo.FileContent.ToHttpContent(), prop.GetPropertyName(), $"@{fileInfo.FileName}");
                    }
                }
                else
                {
                    formData.Add(value.ToHttpContent(), prop.GetPropertyName());
                }
            }
        }
    }
}
