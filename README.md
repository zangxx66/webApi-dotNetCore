```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <aspNetCore processPath="dotnet" arguments=".\WebAPI.dll" stdoutLogEnabled="true" stdoutLogFile=".\logs\stdout" />
        <security>
            <requestFiltering>
                <verbs>
                    <add verb="GET" allowed="true" />
                    <add verb="POST" allowed="true" />
                    <add verb="PUT" allowed="true" />
                    <add verb="DELETE" allowed="true" />
                </verbs>
            </requestFiltering>
        </security>
        <handlers>
        <remove name="aspNetCore" />
            <remove name="WebDAV" />
            <!-- I removed the following handlers too, but these
                 can probably be ignored for most installations -->
            <remove name="ExtensionlessUrlHandler-Integrated-4.0" />
            <remove name="OPTIONSVerbHandler" />
            <remove name="TRACEVerbHandler" />

            <add name="aspNetCore" 
                 path="*" 
                 verb="*" 
                 modules="AspNetCoreModule" 
                 resourceType="Unspecified" />
        </handlers>
		<modules runAllManagedModulesForAllRequests="false" runManagedModulesForWebDavRequests="false">
    <remove name="WebDAVModule" />
  </modules>
  </system.webServer>
</configuration>
```
