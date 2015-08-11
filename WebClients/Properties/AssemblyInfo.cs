using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Crusty Bike Sample Web Clients")]
[assembly: AssemblyDescription("Crusty Bike Web Clients")] // regasm.exe uses this description as the Type Library name.
[assembly: AssemblyCompany("Crusty Bicycle Works")]
[assembly: AssemblyProduct("Crusty Bike")]
[assembly: AssemblyCopyright("© Toolsay LLC. Released under MIT license.")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(true)]
[assembly: ClassInterface(ClassInterfaceType.AutoDual)] // Causes CoClass interfaces to be created, which FoxPro uses for IntelliSense.
[assembly: NeutralResourcesLanguageAttribute("en")]
