using Di_Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiService
{
    public class CitiesServices: ICiteiesServices, IDisposable
    {
        private List<string> _cities;
        private Guid _serviceInstanceId ;
        public Guid ServiceInstanceId { 
            get
            {
                return _serviceInstanceId;
            }
        }
            
        public CitiesServices()
        {
            _serviceInstanceId = Guid.NewGuid();
            _cities = new List<string>()
            {
                "London",
                "Paris",
                "New York",
                "Tokyo"
            };

            // To Do :Add Logic To Open Db Connection

        }
        public List<string> GetCities()
        {
            return _cities;
        }

        public void Dispose()
        {
            // To Do :Add Logic To Close Db Connection
        }
    }
}
