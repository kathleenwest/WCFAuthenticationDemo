REM Passwords: password

makecert -r -pe -n "CN=WCF Demo Server" -b 01/01/2020 -e 01/01/2045 -sky exchange WCFDemoServer.cer -sv WCFDemoServer.pvk
pvk2pfx.exe -pvk WCFDemoServer.pvk -spc WCFDemoServer.cer -pfx WCFDemoServer.pfx -po password

