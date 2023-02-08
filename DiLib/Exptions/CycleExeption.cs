using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiLib.Exptions
{

    [Serializable]
    public class CycleExeption : Exception
    {
        public CycleExeption() { }
        public CycleExeption(string message) : base(message) { }
        public CycleExeption(string message, Exception inner) : base(message, inner) { }
        protected CycleExeption(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
