using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AndroidExplorer {
    class FileUtils {
        public static string getParentDirPath(string current) {
            string[] dirs = current.Split('/');
            StringBuilder sb = new StringBuilder("/");
            for (int i = 0; i < dirs.Length - 1; i++) {
                string dir = dirs[i];
                if (dir.Length > 0) { 
                    sb.Append(dir + "/");
                } 
            }
            if (sb.Length > 1) {
                sb.Length = (sb.Length - 1);
            }
            return sb.ToString();
        }

        public static string md5(string path) {
            FileStream fs = null;
            try {
                fs = new FileStream(path, FileMode.Open);
                return BitConverter.ToString(MD5.Create().ComputeHash(fs)).ToLower().Replace("-", "");
            } catch (Exception e) {
                return null;
            } finally {
                if (fs != null) {
                    fs.Close();
                }
            }
        }
    }
}
