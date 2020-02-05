using System.ServiceModel;

namespace SharedLibrary
{
    /// <summary>
    /// IHelloAuthenticateService
    /// Service contract interface for the client to 
    /// consume its services. 
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface IHelloAuthenticateService
    {
        /// <summary>
        /// GreetMe
        /// Receive a customized greeting by the service 
        /// (if registered and logged in) or a default one.
        /// </summary>
        /// <param name="username">(string) user name</param>
        /// <returns>(string) greeting by the service</returns>
        [OperationContract]
        [FaultContract(typeof(GenericFault))]
        string GreetMe(string username);

        /// <summary>
        /// Register
        /// Register the user with the service. Only distinct
        /// usernames are allowed, and the client will receive
        /// a DuplicateUserFault if the username is already
        /// existing on the service. 
        /// </summary>
        /// <param name="username">(string) user name</param>
        /// <param name="password">(string) password</param>
        /// <returns>(boolean) if successful</returns>
        [OperationContract]
        [FaultContract(typeof(GenericFault))]
        [FaultContract(typeof(DuplicateUserFault))]
        bool Register(string username, string password);

        /// <summary>
        /// Login
        /// Logs in a user after it passes the custom
        /// validation (precursor) to the method
        /// </summary>
        /// <param name="username">(string) username</param>
        /// <returns>(boolean) if successful</returns>
        [OperationContract]
        [FaultContract(typeof(GenericFault))]
        [FaultContract(typeof(DuplicateUserFault))]
        bool Login(string username);

        /// <summary>
        /// LogOff
        /// Logs out the user from the service after
        /// it has passed the custom validation (precursor)
        /// </summary>
        /// <param name="username">(string) username </param>
        /// <returns>(boolean) if successful</returns>
        [OperationContract]
        [FaultContract(typeof(GenericFault))]
        bool LogOff(string username);

    } // end of interface

} // end of namespace
