using Microsoft.OpenApi.Models;
namespace Colegios.Support
{
    public class Info : OpenApiInfo
    {
        public new string Title { get; set; }

        public new string Version { get; set; }
    }
}
