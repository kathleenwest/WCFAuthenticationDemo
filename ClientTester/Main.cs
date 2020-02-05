using System;
using System.Windows.Forms;
using SharedLibrary;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Diagnostics;
using System.ServiceModel.Security;
using System.Text;

namespace ClientTester
{
    /// <summary>
    /// Service "Tester" Client
    /// Simple GUI for testing the service application
    /// that can be expanded for additional features and
    /// capabilities. This not a UI/UX demo but a helper
    /// client to test the WCF service.
    /// </summary>
    /// <remarks>
    /// Note that the CallbackBehavior attribute has been set 
    /// with the UseSynchronizationContext = false value.  
    /// This prevents the callback from being called on the same
    /// thread as the UI thread which would lead to a deadlock.
    /// </remarks>
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class Main : Form, IServiceCallback
    {
        #region Fields

        // username for user     
        private string username = "";

        // password for user
        private string password = "";

        // The greeting from the service
        private string message = "";

        // true if user is registered on the service
        private bool registered = false;

        // true if the user is logged in to the service
        private bool loggedin = false;

        // holds the string object of callback messages from the service
        private StringBuilder sb = new StringBuilder("");

        #endregion Fields

        #region Constructor

        public Main()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Events

        /// <summary>
        /// btnRegister_Click
        /// Event Handler for the user clicking on the button requesting
        /// to register with the serivice. 
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate and Parse the User Text Entry
                if (!ValidateReadUserDataEntry())
                {
                    return;
                }

                // Make a ChannelFactory Proxy to the Service
                DuplexChannelFactory<IHelloAuthenticateService> cf = new DuplexChannelFactory<IHelloAuthenticateService>(this, "myTcp");

                #region How to Set Client Credentials on DuplexChannelFactory

                // First find and remove default endpoint behavior 
                ClientCredentials defaultCredentials = cf.Endpoint.Behaviors.Find<ClientCredentials>();
                cf.Endpoint.Behaviors.Remove(defaultCredentials);

                // Create New Credentials
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = username;
                credentials.UserName.Password = password;

                // Set new Credentials as new endpoint behavior on factory
                cf.Endpoint.Behaviors.Add(credentials); //add required ones

                #endregion How to Set Client Credentials on DuplexChannelFactory

                // Open the ChannelFactory Channel
                cf.Open();

                // Create ChannelFactory Channel Proxy
                IHelloAuthenticateService proxy = cf.CreateChannel();

                // Verify Proxy is Valid
                if (proxy != null)
                {
                    // Try Communications
                    try
                    {
                        // Call the Registration Service
                        registered = proxy.Register(username, password);

                        // User Registration Successful
                        if (registered)
                        {
                            MessageBox.Show("Registration Successful", "Registration", MessageBoxButtons.OK);
                        }
                        // User Registration Failed
                        else
                        {
                            MessageBox.Show("Registration Failed", "Registration", MessageBoxButtons.OK);
                        }

                    } // end of try

                    // catch duplicate user fault
                    catch (FaultException<DuplicateUserFault> ex)
                    {
                        MessageBox.Show("Error occurred: Duplicate User: " + ex.Reason, "FaultException<DuplicateUserFault> Caught");
                    }

                    // catch generic faults
                    catch (FaultException<GenericFault> ex)
                    {
                        MessageBox.Show("Error occurred: Generica Fault: " + ex.Reason, "FaultException<GenericFault> Caught");
                    }

                    // Due to when the custom UserNamePasswordValidator is called in WCF, a fault cannot be sent securely from the
                    // service as the user has not yet been authenticated and security established.
                    // So, you will end up getting a MessageSecurityException with the following message:
                    // "An unsecured or incorrectly secured fault was received from the other party. See the inner FaultException for the fault code and detail."
                    // Since this is the only place we should get this error you can assume a bad login.
                    // Also, the channel will be in a faulted state so kill it and create another!
                    catch (MessageSecurityException)
                    {
                        // User is Not Logged In
                        loggedin = false;

                        MessageBox.Show("The username or password is incorrect.", "Invalid Credentials");
                    }

                    // General Exception catch
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending message: " + ex.Message, "Error");
                    }

                } // end of if

                // Cannot Connect to Server 
                else
                {
                    MessageBox.Show("Cannot Create a Proxy Channel. Check Your Configuration Settings.", "Proxy", MessageBoxButtons.OK);

                } // end of else
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Create a Channel to the Service. Check your configuration settings." + ex.Message, "Channel Setup", MessageBoxButtons.OK);
            }

        } // end of method

        /// <summary>
        /// btnLogin_Click
        /// Event Handler for the user button click event requesting
        /// the login of the user to the service
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate and Parse the User Text Entry
                if (!ValidateReadUserDataEntry())
                {
                    return;
                }

                // Make a ChannelFactory Proxy to the Service
                DuplexChannelFactory<IHelloAuthenticateService> cf = new DuplexChannelFactory<IHelloAuthenticateService>(this, "myTcp");

                #region How to Set Client Credentials on DuplexChannelFactory

                // First find and remove default endpoint behavior 
                ClientCredentials defaultCredentials = cf.Endpoint.Behaviors.Find<ClientCredentials>();
                cf.Endpoint.Behaviors.Remove(defaultCredentials);

                // Create New Credentials
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = username;
                credentials.UserName.Password = password;

                // Set new Credentials as new endpoint behavior on factory
                cf.Endpoint.Behaviors.Add(credentials); //add required ones

                #endregion How to Set Client Credentials on DuplexChannelFactory

                // Make a ChannelFactory Channel to Service
                cf.Open();

                // Create ChannelFactory Channel Proxy
                IHelloAuthenticateService proxy = cf.CreateChannel();

                if (proxy != null)
                {
                    try
                    {
                        // Call the Proxy
                        loggedin = proxy.Login(username);

                        // If User Login Successful
                        if (loggedin)
                        {
                            MessageBox.Show("Login Successful", "Login", MessageBoxButtons.OK);
                        }
                        // User Login Failed
                        else
                        {
                            MessageBox.Show("Login Failed", "Login", MessageBoxButtons.OK);
                        }

                    } // end of try

                    // Due to when the custom UserNamePasswordValidator is called in WCF, a fault cannot be sent securely from the
                    // service as the user has not yet been authenticated and security established.
                    // So, you will end up getting a MessageSecurityException with the following message:
                    // "An unsecured or incorrectly secured fault was received from the other party. See the inner FaultException for the fault code and detail."
                    // Since this is the only place we should get this error you can assume a bad login.
                    // Also, the channel will be in a faulted state so kill it and create another!
                    catch (MessageSecurityException)
                    {
                        // User is Not Logged In
                        loggedin = false;

                        MessageBox.Show("The username or password is incorrect.", "Invalid Credentials");
                    }

                    // Catch Duplicate User Fault
                    catch (FaultException<DuplicateUserFault> ex)
                    {
                        MessageBox.Show("Error occurred: Duplicate User: " + ex.Reason, "FaultException<DuplicateUserFault> Caught");
                    }

                    // Catch Generic Fault
                    catch (FaultException<GenericFault> ex)
                    {
                        MessageBox.Show("Error occurred: Generic Fault: " + ex.Reason, "FaultException<GenericFault> Caught");
                    }

                    // Catch General Exception
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending message: " + ex.Message, "Error");
                    }

                } // end of if

                // Cannot Connect to Server 
                else
                {                    
                    MessageBox.Show("Cannot Create a Channel to a Proxy. Check Your Configuration Settings.", "Proxy", MessageBoxButtons.OK);
                } // end of else
            } // end of try

            // General Error Catch
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Create a Channel to the Service. Check your configuration settings." + ex.Message, "Channel Setup", MessageBoxButtons.OK);
            }

        } // end of method

        /// <summary>
        /// btnGreetMe_Click
        /// Event Handler for the user button click requesting
        /// to retrieve a greeting from the service
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void btnGreetMe_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate and Parse the User Text Entry
                if (!ValidateReadUserDataEntry())
                {
                    return;
                }

                // Make a ChannelFactory Proxy to the Service
                DuplexChannelFactory<IHelloAuthenticateService> cf = new DuplexChannelFactory<IHelloAuthenticateService>(this, "myTcp");

                #region How to Set Client Credentials on DuplexChannelFactory

                // First find and remove default endpoint behavior 
                ClientCredentials defaultCredentials = cf.Endpoint.Behaviors.Find<ClientCredentials>();
                cf.Endpoint.Behaviors.Remove(defaultCredentials);

                // Create New Credentials
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = username;
                credentials.UserName.Password = password;

                // Set new Credentials as new endpoint behavior on factory
                cf.Endpoint.Behaviors.Add(credentials); //add required ones

                #endregion How to Set Client Credentials on DuplexChannelFactory

                // Open the Channel ChannelFactory the Service
                cf.Open();

                // Create ChannelFactory Channel Proxy
                IHelloAuthenticateService proxy = cf.CreateChannel();

                if (proxy != null)
                {
                    try
                    {
                        // Call the Proxy
                        message = proxy.GreetMe(username);

                        // Update the GreetMet Result on the UI
                        txtResult.Text = message.ToString();

                    } // end of try

                    // Duplicate User Fault
                    catch (FaultException<DuplicateUserFault> ex)
                    {
                        MessageBox.Show("Error occurred: Duplicate User: " + ex.Reason, "FaultException<DuplicateUserFault> Caught");
                    }

                    // Generic Fault
                    catch (FaultException<GenericFault> ex)
                    {
                        MessageBox.Show("Error occurred: " + ex.Reason, "FaultException<GenericFault> Caught");
                    }

                    // Due to when the custom UserNamePasswordValidator is called in WCF, a fault cannot be sent securely from the
                    // service as the user has not yet been authenticated and security established.
                    // So, you will end up getting a MessageSecurityException with the following message:
                    // "An unsecured or incorrectly secured fault was received from the other party. See the inner FaultException for the fault code and detail."
                    // Since this is the only place we should get this error you can assume a bad login.
                    // Also, the channel will be in a faulted state so kill it and create another!
                    catch (MessageSecurityException)
                    {
                        // User is Not Logged In
                        loggedin = false;

                        MessageBox.Show("The username or password is incorrect.", "Invalid Credentials");
                    }
                    // Other Exception
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error");
                    }

                } // end of if

                // Cannot Connect to Service 
                else
                {
                    MessageBox.Show("Cannot Create a Channel to a Proxy. Check Your Configuration Settings.", "Proxy", MessageBoxButtons.OK);
                } // end of else
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Create a Channel to the Service. Check your configuration settings." + ex.Message, "Channel Setup", MessageBoxButtons.OK);
            }
        } // end of method

        /// <summary>
        /// btnLogOff_Click
        /// Event Handler that processes the user button click
        /// requesting to Log Off
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void btnLogOff_Click(object sender, EventArgs e)
        {
            try
            {
                // Make a ChannelFactory Proxy to the Service
                DuplexChannelFactory<IHelloAuthenticateService> cf = new DuplexChannelFactory<IHelloAuthenticateService>(this, "myTcp");

                #region How to Set Client Credentials on DuplexChannelFactory

                // First find and remove default endpoint behavior 
                ClientCredentials defaultCredentials = cf.Endpoint.Behaviors.Find<ClientCredentials>();
                cf.Endpoint.Behaviors.Remove(defaultCredentials);

                // Create New Credentials
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = username;
                credentials.UserName.Password = password;

                // Set new Credentials as new endpoint behavior on factory
                cf.Endpoint.Behaviors.Add(credentials); //add required ones

                #endregion How to Set Client Credentials on DuplexChannelFactory

                // Open a ChannelFactory Channel
                cf.Open();

                // Create ChannelFactory Channel Proxy
                IHelloAuthenticateService proxy = cf.CreateChannel();

                if (proxy != null)
                {
                    try
                    {
                        // Call the Proxy
                        loggedin = !(proxy.LogOff(username));

                        // User Logged Out Successful
                        if (!loggedin)
                        {
                            MessageBox.Show("Logout Successful", "Logout", MessageBoxButtons.OK);
                        }
                        // User Logout Failed
                        else
                        {
                            MessageBox.Show("Logout Failed", "Logout", MessageBoxButtons.OK);
                        }

                    } // end of try

                    // Duplicate User Fault
                    catch (FaultException<DuplicateUserFault> ex)
                    {
                        MessageBox.Show("Error occurred: Duplicate User: " + ex.Reason, "FaultException<DuplicateUserFault> Caught");
                    }

                    // Generic Fault
                    catch (FaultException<GenericFault> ex)
                    {
                        MessageBox.Show("Error occurred: " + ex.Reason, "FaultException<GenericFault> Caught");
                    }

                    // Due to when the custom UserNamePasswordValidator is called in WCF, a fault cannot be sent securely from the
                    // service as the user has not yet been authenticated and security established.
                    // So, you will end up getting a MessageSecurityException with the following message:
                    // "An unsecured or incorrectly secured fault was received from the other party. See the inner FaultException for the fault code and detail."
                    // Since this is the only place we should get this error you can assume a bad login.
                    // Also, the channel will be in a faulted state so kill it and create another!
                    catch (MessageSecurityException)
                    {
                        // User is Not Logged In
                        loggedin = false;

                        MessageBox.Show("The username or password is incorrect.", "Invalid Credentials");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending message: " + ex.Message, "Error");
                    }

                } // end of if

                // Cannot Connect to Server 
                else
                {
                    MessageBox.Show("Cannot connect to service. Check Your Configuration Settings.", "Proxy", MessageBoxButtons.OK);
                } // end of else
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Create a Channel to the Service. Check your configuration settings." + ex.Message, "Channel Setup", MessageBoxButtons.OK);
            }

        } // end of method

        #endregion Events

        #region Methods

        /// <summary>
        /// ValidateReadUserDataEntry
        /// Validates the user data entry for the username
        /// and password fields, then unsecurely stores 
        /// them for passing to service as credentials
        /// </summary>
        /// <returns>(boolean) if valid</returns>
        private bool ValidateReadUserDataEntry()
        {
            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username must not be empty", "Invalid Data", MessageBoxButtons.OK);
                return false;
            }

            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password must not be empty", "Invalid Data", MessageBoxButtons.OK);
                return false;
            }

            // Set the Username and Password

            username = txtUsername.Text;
            password = txtPassword.Text;

            return true;

        } // end of method

        #endregion Methods

        #region IServiceCallback Implementation

        /// <summary>
        /// SendClientMessage
        /// Callback method implementation for the service
        /// to send a message to the client caller. Client
        /// receives the message here and updates the UI
        /// </summary>
        /// <param name="message">(string) message from the service</param>
        public void SendClientMessage(string message)
        {
            try
            {
                // Check if UI Threading Sync is Required
                if (txtServiceMessage.InvokeRequired)
                {
                    // Required: Invoke the Request on the UI thread
                    Action<string> del = new Action<string>(SendClientMessage);
                    txtServiceMessage.BeginInvoke(del, message);
                }
                // Add the recent callback message to the listbox
                else
                {
                    // Append the message to the string builder
                    sb.Append(message + Environment.NewLine);

                    // Update the UI
                    txtServiceMessage.Text = sb.ToString();
                }
            }
            catch (Exception ex)
            {               
                MessageBox.Show("Error updating UI thread with callback message: " + ex.Message);
            }
        } // end of method

        #endregion IServiceCallback Implementation

    } // end of class
} // end of namespace
