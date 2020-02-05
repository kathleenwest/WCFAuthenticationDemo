using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    /// <summary>
    /// Duplicate User Fault
    /// Specific fault model for describing exceptions
    /// when a user tries to register or log in with an existing
    /// username. 
    /// </summary>
    [DataContract]
    public class DuplicateUserFault
    {
        #region Properties
        [DataMember]
        public string Reason { get; set; }
        #endregion Properties

    } // end of class

} // end of namespace
