using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceLibrary;

namespace ServiceHost
{
    /// <summary>
    /// Program
    /// Hosts a service using a console application
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main
        /// Main entry for program
        /// </summary>
        /// <param name="args">none used</param>
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Hello Authenticate Demo Service...");
                // Note: Do not put this service host constructor within a using clause.
                // Errors in Open will be trumped by errors from Close (implicitly called from ServiceHost.Dispose).
                System.ServiceModel.ServiceHost host = new System.ServiceModel.ServiceHost(typeof(HelloAuthenticateService));
                host.Open();

                Console.WriteLine("The Hello Authenticate Demo Service has started.");
                Console.WriteLine("Press <ENTER> to quit.");
                Console.ReadLine();
                host.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.WriteLine("Press <ENTER> to quit.");
                Console.ReadLine();
            }

        } // end of main method

    } // end of class
} // end of namespace