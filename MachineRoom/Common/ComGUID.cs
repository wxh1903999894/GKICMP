﻿using System;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BBT.Common
{
    public class ComGUID
    {
        private static string computerGUID = string.Empty;
        public static string Value()
        {
            if (string.IsNullOrEmpty(computerGUID))
            {


                computerGUID = GetHash("CPU >> " + cpuId() + "\nBIOS >> " +
            biosId() + "\nBASE >> " + baseId() + videoId() + "\nMAC >> " + macId()
                                     );
                computerGUID = computerGUID.Substring(0, 4) + computerGUID.Substring(5, computerGUID.Length - 5);
                computerGUID = computerGUID.Substring(0, 24) + computerGUID.Substring(24, computerGUID.Length - 25).Replace("-", "");
            }
            return computerGUID;
        }
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            string str = GetHexString(sec.ComputeHash(bt));
            return str;

        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }

        #region Original Device ID Getting Code
        private static string identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }


        /// <summary>
        ///    //获取CPU序列号
        /// </summary>
        /// <returns></returns>
        private static string cpuId()
        {
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "")
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "")
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "")
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }

                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }


        /// <summary>
        /// //BIOS序列号 
        /// </summary>
        /// <returns></returns>
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }


        /// <summary>
        /// 获取驱动信息
        /// </summary>
        /// <returns></returns>
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }


        // 主板序列号
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }


        /// <summary>
        /// 显卡信息
        /// </summary>
        /// <returns></returns>
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }


        /// <summary>
        /// MAC地址
        /// </summary>
        /// <returns></returns>
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }
        #endregion


        #region 获取mac地址
        public static string GetNetworkAdpaterID()
        {
            try
            {
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                moc = null;
                mc = null;
                return mac.Trim().Replace(":", "");
            }
            catch (Exception e)
            {
                return "error:" + e.Message;
            }
        }
        #endregion

        #region 获取ip地址
        public static string GetNetworkIP()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        } 
        #endregion
    }
}
