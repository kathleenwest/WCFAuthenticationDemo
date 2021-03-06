﻿using System.Text;
using SharedLibrary;


namespace ServiceLibrary
{
    /// <summary>
    /// User
    /// Model of a user in the application
    /// Password uses PBDK for security
    /// </summary>
    public class User
    {

        #region properties

        // Username of the Registered User
        public string UserName { get; private set; }

        // Callback to Client User for Duplex Communications
        public IServiceCallback Callback { get; private set; }

        #region Cryptology Implementation

        // unique cryptological salt for user
        public byte[] Salt { get; private set; }

        // derived key password (after PBKD)
        public byte[] Password { get; private set; }

        // number of iterations for the PBKD function
        public int WorkFactor { get; private set; } = 5000;

        #endregion Cryptology Implementation

        #endregion properties

        #region constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="username">(string) username for the user</param>
        /// <param name="password">(string) password for the user</param>
        /// <param name="callback">(IServiceCallback) for the user</param>
        public User(string username, string password, IServiceCallback callback)
        {
            UserName = username;
            Callback = callback;

            #region Use PBDK to Securely Create and Store Password

            // Generate a salt
            // The US National Institute of Standards and Technology 
            // recommends a salt length of 128 bits.
            Salt = ServiceCryptology.GenerateSalt(128);

            // Convert String Password to Unicode Byte Array
            byte[] bytesPassword = Encoding.Unicode.GetBytes(password);

            // Create a Derived Key Password
            // Use default workfactor
            Password = ServiceCryptology.GenerateHash(bytesPassword, Salt, WorkFactor, 256);

            #endregion Use PBDK to Securely Create and Store Password

        } // end of method

        #endregion constructor

    } // end of class
} // end of namespace
