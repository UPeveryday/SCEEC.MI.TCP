using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography;

namespace SCEEC.Config
{
    public struct user
    {
        public string name;
        public string password;
        public int authority;
    }

    public static class Users
    {
        private static string folder;
        private  static string path;

        private static string sha1sign(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] data = SHA1.Create().ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach(byte t in data)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static bool userConfigInit()
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SCEEC\\TTM";
                path = folder + "\\users.cfg";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                if (File.Exists(path))
                    if (File.ReadAllText(path) != string.Empty)
                        xml.Load(path);
            }
            catch (Exception ex)
            {
#if DEBUG 
                throw ex;
#else
                return false;
#endif
            }
            XmlNode root = xml.SelectSingleNode("UserVerification");
            if (root == null)
            {
                xml = new XmlDocument();
                root = xml.CreateElement("UserVerification");
            }
            
            XmlNodeList xnl = root.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                if (xn.Name == "sceec")
                    root.RemoveChild(xn);
            }

            XmlElement xe;
            xe = xml.CreateElement("sceec");
            xe.SetAttribute("pw", "3E649A024EB4DC5E4123773C39CA74AD69C5256C");
            xe.SetAttribute("auth", "255");
            xe.SetAttribute("verification", "D7A1D4A7EF6F44D14978E659AFB8FA9801E98646");
            root.AppendChild(xe);
            xml.AppendChild(root);
            xml.Save(path);

            return true;
        }

        private static bool userNodeVerify(XmlNode xn, out user usr)
        {
            usr = new user();
            if (xn == null) return false;
            if (xn.Attributes["pw"] == null) return false;
            if (xn.Attributes["auth"] == null) return false;
            string name = xn.Name;
            string password = xn.Attributes["pw"].Value;
            string authority = xn.Attributes["auth"].Value;
            string verification = xn.Attributes["verification"].Value;
            string str = 
                "nm = " + name +
                ", pw = " + password +
                ", auth = " + authority
                ;
            string sign = sha1sign(str);
            if (!Microsoft.VisualBasic.Information.IsNumeric(authority)) return false;
            if (sign == verification)
            {
                usr.name = name;
                usr.password = password;
                usr.authority = int.Parse(authority);
                return true;
            }
            return false;
        }

        public static List<user> getUsers()
        {
            List<user> users = new List<user>();
            if (!userConfigInit()) return users;

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            foreach (XmlNode xn in xml.SelectSingleNode("UserVerification").ChildNodes)
            {
                user usr;
                if (userNodeVerify(xn, out usr))
                {
                    users.Add(usr);
                }
            }
            return users;
        }

        public static bool removeUser(string username)
        {
            if (username == string.Empty) return false;
            if (!userConfigInit()) return false;

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            XmlNode xn = xn = xml.SelectSingleNode("UserVerification").SelectSingleNode(username);
            if (xn != null)
            {
                xml.SelectSingleNode("UserVerification").RemoveChild(xn);
                xml.Save(path);
                return true;
            }
            return false;
        }

        public static bool addUser(string username, string password, string auth)
        {
            if (username == string.Empty) return false;
            if (password == string.Empty) return false;
            if (!Microsoft.VisualBasic.Information.IsNumeric(auth)) return false;
            int authority = int.Parse(auth);
            if ((authority > 255) || (authority < 0)) return false;
            if (!userConfigInit()) return false;

            removeUser(username);

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            XmlElement xe;
            xe = xml.CreateElement(username);
            xe.SetAttribute("pw", sha1sign(password)); 
            xe.SetAttribute("auth", auth);
            string str =
                "nm = " + username +
                ", pw = " + sha1sign(password) +
                ", auth = " + auth
                ;
            string sign = sha1sign(str);
            xe.SetAttribute("verification", sign);
            xml.SelectSingleNode("UserVerification").AppendChild(xe);
            xml.Save(path);
            return true;
        }

        public static bool addUser(user usr)
        {
            return addUser(usr.name, usr.password, usr.authority.ToString());
        }

        public static bool userConfigRefresh()
        {
            var users = getUsers();
            if (users.Count < 1) return false;
            System.IO.File.WriteAllText(path, string.Empty);
            foreach (user usr in users)
                addUser(usr);
            return true;
        }

        public static bool userVerify(ref user usr, out string errorText)
        {
            var users = getUsers();
            if (users.Count < 1)
            {
                errorText = "用户数据遭到破坏无法读取";
                return false;
            }
            bool nameVerified = false;
            foreach (user verifiedUser in users)
            {
                if (verifiedUser.name == usr.name)
                {
                    nameVerified = true;
                    if (verifiedUser.password == sha1sign(usr.password))
                    {
                        usr.password = verifiedUser.password;
                        usr.authority = verifiedUser.authority;
                        errorText = string.Empty;
                        return true;
                    }
                }
            }
            if (!nameVerified)
                errorText = "用户名不存在";
            else
                errorText = "用户密码错误";
            return false;
        }

        public static bool userVerify(string username, string password, out int authority, out string errorText)
        {
            user usr = new user();
            usr.name = username;
            usr.password = password;
            authority = 0;
            if (!userVerify(ref usr, out errorText)) return false;
            authority = usr.authority;
            return true;
        }

        public static bool userVerify(string username, string password)
        {
            user usr = new user();
            usr.name = username;
            usr.password = password;
            string errorText;
            return userVerify(ref usr, out errorText);
        }
    }

}
