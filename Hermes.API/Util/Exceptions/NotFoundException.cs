using System.Threading.Tasks;

namespace Hermes.API.Util.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg): base(msg)
        {

        }
    }
}
