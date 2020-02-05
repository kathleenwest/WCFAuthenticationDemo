using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace SharedLibrary
{
    /// <summary>
    /// Client Contract
    /// Client must implement this contract in order
    /// to particpate in duplex communications and
    /// receive messages from the service for normal
    /// (non-fault) information (ex: chat messages). 
    /// </summary>
    [ServiceContract]
    public interface IServiceCallback
    {
        /// <summary>
        /// SendClientMessage
        /// Sends the Client recent messages
        /// (When it is connected)
        /// The service does not want to wait on a response from the client
        /// so there is no secondary callback to the service. This is oneway
        /// to the client only. 
        /// </summary>
        /// <param name="message">(string) a message from the service</param>
        [OperationContract(IsOneWay = true)]
        void SendClientMessage(string message);

    } // end of interface
} // end of class
