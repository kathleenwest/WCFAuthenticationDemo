using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SharedLibrary;

namespace ServiceLibrary
{
    /// <summary>
    /// HelloAuthenticateService
    /// Manages the Registration, Login, LogOff, Client Messaging, 
    /// and Greeting services to clients
    /// Single Instance "Shared Service"
    /// Implements the IHelloAuthenticateService contracts
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class HelloAuthenticateService : ServiceErrorHandler, IHelloAuthenticateService
    {

        #region Properties

        // List of Registered Users
        public static List<User> RegisteredUsers { get; private set; }

        // List of LoggedIn Users
        public static List<User> LoggedInUsers { get; private set; }

        #endregion Properties

        #region Constructor

        public HelloAuthenticateService()
        {
            RegisteredUsers = new List<User>();
            LoggedInUsers = new List<User>();
        }

        #endregion Constructor

        #region IHelloAuthenticate Implementation

        /// <summary>
        /// Register
        /// The client sends a request to register themselves
        /// with a username and password. If they are already registered
        /// the CustomValidation tries to validate their credentials
        /// </summary>
        /// <param name="username">(string) user name</param>
        /// <param name="password">(string) password</param>
        /// <returns>(boolean) if successful, false otherwise</returns>
        public bool Register(string username, string password)
        {
            // Registration Success Flag
            bool registered = false;

            // Obtain callback for the client
            IServiceCallback callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();

            // First Verify User Does Not Already Exist in Registration
            if (!(RegisteredUsers.Any(x => x.UserName == username)))
            {
                // User is Not Registered, Register User 

                // Create new user
                User user = new User(username, password, callback);

                /// Add to Registered Users
                RegisteredUsers.Add(user);

                // Set Return Value
                registered = true;

                Console.WriteLine($"User {username} registered...");
            }
            else
            {
                DuplicateUserFault fault = new DuplicateUserFault()
                { Reason = "User '" + username + "' already registered." };
                throw new FaultException<DuplicateUserFault>(fault, fault.Reason);
            }

            return registered;

        } // end of Register method

        /// <summary>
        /// GreetMe
        /// Returns a custom greeting to an authenticated user
        /// or a default general message to a stranger
        /// </summary>
        /// <param name="username">(string) user name</param>
        /// <returns>(string) custom greeting to a user, or default response</returns>
        public string GreetMe(string username)
        {
            // Find the user by username (distinct)
            User user = LoggedInUsers?.Find(x => x.UserName == username);

            if (user != null)
            {
                // User already validated
                return $"Hello, {username}. You are successfully registered and logged in.";
            }
            else
            {
                return "Stranger: Please first register, then login to get your custom user greeting.";
            }
        } // end of Greet Me

        /// <summary>
        /// Login
        /// Logs in a registered user.
        /// Throws faults (to client) if there are issues
        /// </summary>
        /// <param name="username">(string) username to login</param>
        /// <returns>(boolean) true if successful, false otherwise</returns>
        public bool Login(string username)
        {
            bool loggedin = false;

            // First Verify User Not Already Logged In
            if (!(LoggedInUsers.Any(x => x.UserName == username)))
            {
                // See if User is Registered
                if (RegisteredUsers.Any(x => x.UserName == username))
                {
                    // User is Already Authenticated Per Custom Validator
                    LoggedInUsers.Add(RegisteredUsers.Find(x => x.UserName == username));

                    loggedin = true;

                    Console.WriteLine($"User {username} logged in.");

                    // Send message to callback
                    SendMessageToUsers($"User {username} logged in.");
                }
                // User is Not Registered
                else
                {
                    loggedin = false;

                    // This will be sent to client as a generic fault
                    throw new ArgumentException("User: " + username + " is not registered");
                }
            }
            else
            {
                loggedin = false;

                // Send custom fault to client
                DuplicateUserFault fault = new DuplicateUserFault()
                { Reason = "User '" + username + "' already logged in!" };
                throw new FaultException<DuplicateUserFault>(fault, fault.Reason);
            }

            return loggedin;

        } // end of method

        /// <summary>
        /// LoggOff
        /// Logs off an existing registered and logged in user
        /// Throws exception (then handled as a generic fault to client) if
        /// the user is not logged in. 
        /// </summary>
        /// <param name="username">(string) username to log off</param>
        /// <returns>(boolean) if logoff is successful, false otherwise</returns>
        public bool LogOff(string username)
        {
            bool loggedoff = false;

            // Find the user by username (distinct)
            User user = LoggedInUsers?.Find(x => x.UserName == username);

            if (user != null)
            {
                // User already validated

                LoggedInUsers.Remove(user);
                loggedoff = true;

                Console.WriteLine($"User {username} logged out...");

                SendMessageToUsers($"User {username} logged out.");
            }
            //  throws fault to user
            else
            {
                throw new ArgumentException("User: " + username + " is not logged in.");
            }

            return loggedoff;

        } // end of method

        #endregion IHelloAuthenticate Implementation

        #region Methods

        /// <summary>
        /// SendMessageToUsers
        /// Sends a message to all logged in users
        /// </summary>
        /// <param name="message">(string) message to user</param>
        private void SendMessageToUsers(string message)
        {
            // Sens message for each logged in user
            foreach (User user in LoggedInUsers)
            {
                try
                {
                    IServiceCallback callback = user.Callback;
                    callback.SendClientMessage(message);
                }

                // Catches an issue with a user disconnect and logs off that user
                catch (Exception)
                {                    
                    // Remove the disconnected client
                    LogOff(user.UserName);
                }
            } // end of foreach
        } // end of method

        #endregion Methods

    } // end of class
} // end of namespace
