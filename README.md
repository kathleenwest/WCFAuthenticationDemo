# WCFAuthenticationDemo
WCF Authentication Service Application + Custom Validator + Secure Password Storage + Error Handler + Certificates & Client Tester Windows Form Application

Project Blog Article Here: https://portfolio.katiegirl.net/2020/02/12/wcf-authentication-service-application-custom-validator-secure-password-storage-error-handler-certificates-client-tester-windows-form-application/


This project presents a Visual Studio solution including a simple demo WCF Authentication Service Application and a “Tester” Client (Windows Form Application) that allows the user to test the user registration, login, logout, and service operations. In addition to demonstrating standard authentication capabilities, the WCF service implements a custom username and password validator pattern. Passwords are stored securely using Password-Based Key Derivation Function PBKD cryptology of which the implementation is discussed. A custom error handler ensures that exceptions are properly wrapped into WCF Faults and communicated to the client caller. Certificates are discussed along with how to implement a server certificate on a client machine for development testing of “integrity” and application trust. The project includes a demo certificate and script for generating self-signed dev certificates, which must be installed into the client certificate store for the client tester application to trust and access the demo service.  The client “tester” windows form application is not intended as a UX/UI demo but used to test and verify that the backend authentication service registration, login, logout, service operations, and callbacks are working as expected and sending proper WCF fault messages. Lastly, the project is shown in the demo section with a video and screen captures.



Architecture


The demo project consists of these simple component topics:

SharedLibrary – Class library shared amongst server and client describing service contract interfaces and common fault models.

ServiceLibrary – Class library that implements the service contracts and backend features

ServiceHost – Console Application that launches and hosts the Service

ClientTester – Client “Tester to Service” Windows Form Application Project
