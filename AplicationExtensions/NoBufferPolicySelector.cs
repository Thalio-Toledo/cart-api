using System.Web.Http.Hosting;

namespace carrinho_api.AplicationExtensions
{
    public class NoBufferPolicySelector : IHostBufferPolicySelector
    {
        public bool UseBufferedInputStream(object hostContext) => false;
        public bool UseBufferedOutputStream(HttpResponseMessage response) => false;
    }
}
