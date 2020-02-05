using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace SharedLibrary
{
    /// <summary>
    /// GenericFault
    /// General Fault object model
    /// for all exceptions on service
    /// for client to understand.
    /// </summary>
    [DataContract]
    public class GenericFault
    {
        [DataMember]
        public string Reason { get; set; }

    } // end of class
} // end of namespace
