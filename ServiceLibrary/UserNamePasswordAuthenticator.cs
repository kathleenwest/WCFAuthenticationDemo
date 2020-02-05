using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    /// <summary>
    /// UserNamePasswordAuthenticator
    /// Custom User Name and Password Validator
    /// ........................
    /// Description:
    /// By default, when a user name and password is used for authentication, 
    /// Windows Communication Foundation (WCF) uses Windows to validate the 
    /// user name and password. However, WCF allows for custom user name and 
    /// password authentication schemes, also known as validators. 
    /// .......................
    /// App.config in Service Host must include under <serviceCredentials>
    /// <userNameAuthentication userNamePasswordValidationMode="Custom" customUserNamePasswordValidatorType="ServiceLibrary.UserNamePasswordAuthenticator, ServiceLibrary"/>
    /// ........................
    /// App.config in Service Host should include under <serviceBehaviors> ... <behavior name = "ServerBehavior" > ... <serviceDebug includeExceptionDetailInFaults="true"/>
    /// ........................
    /// Documentation Reference
    /// https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-use-a-custom-user-name-and-password-validator
    /// .......................
    /// See project blog article for lessons learned and project architecture
    /// </summary>
    public class UserNamePasswordAuthenticator : UserNamePasswordValidator
    {
        /// <summary>
        /// Validate
        /// Note: Project uses Message security on net.tcp binding
        /// to securely receive and process the client credentials
        /// Client must build and send their username and password credentials
        /// in their communications to the service. See project article for
        /// help on how to build and securely send client credentials. 
        /// </summary>
        /// <param name="username">(string) User credential "username"</param>
        /// <param name="password">(string) User credential "password"</param>
        public override void Validate(string username, string password)
        {
            #region ValidateUserRegistration

            // First try to find a registered user
            User user = HelloAuthenticateService.RegisteredUsers.FirstOrDefault(x => x.UserName == username);

            // Allow the Service Registration to Handle Unregistered User
            if (user == null)
            {
                // Do not validate non-existing users
                return;
            }

            #endregion ValidateUserRegistration

            #region AuthenticateRegisteredUser

            // Convert Password to Compare to Database Password
            // Password-Based Key Derivation Function
            byte[] bytesPassword = Encoding.Unicode.GetBytes(password);
            // Note: Cryptology values may not be suitable for production level, this is only a demo
            byte[] passwordHash = ServiceCryptology.GenerateHash(bytesPassword, user.Salt, user.WorkFactor, 256);

            // Authenticate the Existing User Password
            if (user != null && System.Text.Encoding.Default.GetString(user.Password) == System.Text.Encoding.Default.GetString(passwordHash))
            {
                // Authenticated, no other action necessary
            }
            else
            {              
                throw new SecurityTokenException("Unknown Username or Incorrect Password");
            }

            #endregion AuthenticateRegisteredUser

        } // end of method

    } // end of class
} // end of namespace
