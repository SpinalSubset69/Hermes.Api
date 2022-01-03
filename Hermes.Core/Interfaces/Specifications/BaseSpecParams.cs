using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications
{
    public class BaseSpecParams
    {
        private const int MAX_SIZE = 50;
        public int PageIndex { get; set; } = 1;
        private int _size = 4;
        public int PageSize { get => _size; set => _size = (value > MAX_SIZE) ? MAX_SIZE : value; }
    }
}
