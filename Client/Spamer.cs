using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Spamer
    {
        private readonly Logger _logger;
        private readonly StringGenerator _generator;
        public Spamer(Logger logger, StringGenerator generator)
        {
            this._logger = logger;
            this._generator = generator;
        }

        public void Spam()
        {
            _logger.Log(_generator.RandomString(10));
        }
    }
}
