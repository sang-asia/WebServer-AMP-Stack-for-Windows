using Microsoft.Win32;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Management;
using System.ServiceProcess;

namespace WebServer.Libraries
{
    enum EServiceStartupType
    {
        Boot,
        System,
        Automatic,
        Manual,
        Disabled
    }

    class Services
    {
        /// <summary>
        /// Create service installer
        /// </summary>
        public static ServiceInstaller GetInstaller(ServiceController service)
        {
            ServiceInstaller installer = new ServiceInstaller();
            InstallContext context = new InstallContext();
            installer.Context = context;
            installer.ServiceName = service.ServiceName;
            return installer;
        }

        /// <summary>
        /// Delete a service
        /// </summary>
        public static void Delete(ServiceController service)
        {
            GetInstaller(service).Uninstall(null);
        }

        /// <summary>
        /// Find services by image path
        /// </summary>
        public static ServiceController[] FindByImage(string path)
        {
            ServiceController[] services = ServiceController.GetServices();
            List<ServiceController> result = new List<ServiceController> { };
            path = path.ToLower();

            foreach (ServiceController s in services)
            {
                string image_path = GetImagePath(s);

                if (image_path == null || image_path.Length == 0)
                {
                    continue;
                }

                image_path = image_path.Replace("\"", "").ToLower();

                if (image_path.StartsWith(path))
                {
                    result.Add(s);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Get service's registry key
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        private static RegistryKey GetRegistryKey(ServiceController service)
        {
            string registryPath = @"SYSTEM\CurrentControlSet\Services\" + service.ServiceName;
            RegistryKey key;
            
            if (service.MachineName != "" && service.MachineName != ".")
            {
                key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, service.MachineName).OpenSubKey(registryPath);
            }
            else
            {
                key = Registry.LocalMachine.OpenSubKey(registryPath);
            }

            return key;
        }

        /// <summary>
        /// Get image path of a service
        /// </summary>
        public static string GetImagePath(ServiceController service)
        {
            RegistryKey key = GetRegistryKey(service);

            if (key == null)
            {
                return null;
            }

            string value = key.GetValue("ImagePath").ToString();
            key.Close();
            
            return System.Environment.ExpandEnvironmentVariables(value);
        }

        /// <summary>
        /// Set service startup type
        /// </summary>
        private static bool SetStartType(ServiceController service, EServiceStartupType type)
        {
            ManagementPath path = new ManagementPath("Win32_Service.Name='" + service.ServiceName + "'");
            ManagementObject obj = new ManagementObject(path);

            if (obj["StartMode"] != null)
            {
                object[] parameters = new object[1] { type.ToString() };
                obj.InvokeMethod("ChangeStartMode", parameters);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Set service start automatically
        /// </summary>
        public static bool SetStartAutomatic(ServiceController service)
        {
            return SetStartType(service, EServiceStartupType.Automatic);
        }

        /// <summary>
        /// Set service start manually
        /// </summary>
        public static bool SetStartManual(ServiceController service)
        {
            return SetStartType(service, EServiceStartupType.Manual);
        }
    }
}
