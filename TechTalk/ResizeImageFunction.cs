using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace TechTalk
{
    public static class ResizeImageFunction
    {
        [FunctionName("BlobFunction")]
        public static void Run(
            [BlobTrigger("images/{name}")]Stream inputBlob, string name,
            [Blob("images-thumb/{name}", FileAccess.Write)] Stream outputBlob,
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {inputBlob.Length} Bytes");
            using var image = Image.Load(inputBlob);
            image.Mutate(x => x
                .Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Crop,
                    Size = new Size(180, 180)
                }));

            image.Save(outputBlob, new PngEncoder());
        }
    }
}
