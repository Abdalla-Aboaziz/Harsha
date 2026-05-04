using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di_Contract
{
    public interface ICiteiesServices
    {
        Guid ServiceInstanceId { get; }
        public List<string> GetCities();
       
    }
}
