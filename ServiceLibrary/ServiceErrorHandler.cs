using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    /// <summary>
    /// Service-wide error handler class. Classes that inherit from this class can 
    /// throw exceptions directly, and those exceptions will be sent to clients in 
    /// the Detail property of the enclosing FaultException<T> object. 
    /// Furthermore, if a FaultContract is specified for a given exception, a 
    /// FaultException for that exception can be differentiated from others in 
    /// try-catch statements.
    /// </summary>
    public class ServiceErrorHandler : IErrorHandler, IServiceBehavior
    {
        #region IErrorHandler Implementation

        /// <summary>
        /// Enables error-related processing and returns a value that indicates whether
        /// the dispatcher aborts the session and the instance context in certain cases.
        /// This can and will shut down the service sessiona nd communications to a client
        /// under certain circumstances of errors.
        /// </summary>
        /// <param name="error">The exception thrown during processing.</param>
        /// <returns>
        /// True if WCF should not abort the session (if there is one) and instance 
        /// context if the instance context is not Single; otherwise, false.
        /// </returns>
        public bool HandleError(Exception error)
        {
            // Custom actions, if any, here. NOTE: WCF does NOT guarantee which thread calls this method.
            // Not used in this demo
            return true;

        } // end of method

        /// <summary>
        /// Enables the creation of a custom FaultException<GenericFault> 
        /// that is returned from an exception in the course of a service method.
        /// Important! Communications have to be duplex for the client to receive the fault!
        /// </summary>
        /// <param name="ex">The Exception object thrown in the course of the service operation.</param>
        /// <param name="version">The SOAP version of the message.</param>
        /// <param name="msg">The Message object that is returned to the client, or service, in the duplex case.</param>
        public void ProvideFault(Exception ex, MessageVersion version, ref Message msg)
        {
            // If the Exception was already caught as a FaultException type object,
            //  we do not need to create a new one
            if (!(ex is FaultException))
            {
                FaultException<GenericFault> fex = new FaultException<GenericFault>(
                    new GenericFault()
                    {
                        Reason = ex.Message
                    },
                    ex.Message
                );
                // Communications have to be duplex for the client to receive the faults
                MessageFault fault = fex.CreateMessageFault();
                msg = Message.CreateMessage(version, fault, fex.Action);
            }

        } // end of method

        #endregion IErrorHandler Implementation

        #region IServiceBehavior Implementation

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to 
        /// support the contract implementation.
        /// </summary>
        /// <param name="description">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="parameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
        {
            // Not used in my demo

        } // end of method

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom 
        /// extension objects such as error handlers, message or parameter interceptors, 
        /// security extensions, and other custom extension objects.
        /// This behavior can also be placed in a configuration file instead of
        /// programmatically attached as below. See the App.config for an example of
        /// using the configuration approach to attaching the error handler to the
        /// service behaviors. 
        /// </summary>
        /// <param name="description">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        public void ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler = new ServiceErrorHandler();

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;

                if (channelDispatcher != null)
                {
                    channelDispatcher.ErrorHandlers.Add(errorHandler);
                }
            }
        } // end of method


        /// <summary>
        /// Provides the ability to inspect the service host and the service 
        /// description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="description">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        public void Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            // This is not used in this demo
        }

        #endregion IServiceBehavior Implementation

    } // end of class

} // end of namespace
