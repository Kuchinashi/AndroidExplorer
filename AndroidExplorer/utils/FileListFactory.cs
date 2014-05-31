using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AndroidExplorer.Properties;

namespace AndroidExplorer {
    class FileListFactory {
        public static IList<FileInfo> createFileList(string lsResult, string current) {
            IList<FileInfo> fileInfoList = new List<FileInfo>();
            fileInfoList.Add(new FileInfo(FileInfo.TYPE_DIR, "/", "/", null, null, -1, null, null));
            if (current != "/") {
                fileInfoList.Add(new FileInfo(FileInfo.TYPE_DIR, "..", FileUtils.getParentDirPath(current),
                    null, null, -1, null, null));
            }
            if (lsResult == null) {
                return fileInfoList;
            }
            string[] fileRows = lsResult.Split(new char[] { '\r', '\n' });
            foreach (string row in fileRows) {
                if (row == "" || row == null) {
                    continue;
                }
                FileInfo fileInfo = convertFileInfo2LsRow(row, current);
                if (fileInfo == null) {
                    MessageBox.Show(lsResult, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                fileInfoList.Add(fileInfo);
            }
            return fileInfoList;
        }

        private static FileInfo convertFileInfo2LsRow(string row, string current) {

            FileInfo fileInfo = new FileInfo();

            Regex regex = new Regex("\\s+");
            string[] infoArray = regex.Split(row);
            if (infoArray.Length < 6) {
                return null;
            }
            int offset = 0;
            char type = infoArray[0][0];
            switch (type) {
                case 'd':
                    fileInfo.SetType(FileInfo.TYPE_DIR);
                    break;
                case 'l':
                    fileInfo.SetType(FileInfo.TYPE_LINK);
                    break;
                case '-':
                    fileInfo.SetType(FileInfo.TYPE_FILE);
                    offset = 1;
                    break;
                default:
                    return null;
            }
            fileInfo.SetUser(infoArray[1]);
            fileInfo.SetGroup(infoArray[2]);
            if (fileInfo.GetType() == FileInfo.TYPE_FILE) {
                fileInfo.SetSize(long.Parse(infoArray[3]));
            } else {
                fileInfo.SetSize(-1);
            }
            fileInfo.SetTime(infoArray[3 + offset] + " " + infoArray[4 + offset]);
            StringBuilder fileAndLink = new StringBuilder();
            for (int i = 5 + offset; i < infoArray.Length; i++) {
                fileAndLink.Append(infoArray[i] + " ");
            }
            fileAndLink.Length--;
            Regex reg = new Regex(" -> ");
            string[] splitNameAndLink = reg.Split(fileAndLink.ToString());
            fileInfo.SetFileName(splitNameAndLink[0]);
            fileInfo.SetAbsoluteFilePath(current, fileInfo.GetFileName());
            if (fileInfo.GetType() == FileInfo.TYPE_LINK) {
                fileInfo.SetLink(splitNameAndLink[1]);
            }
            return fileInfo;
        }

        public static string[][] createDataArray(List<FileInfo> fileList) {
            string[][] data = new string[fileList.Count][];
            for (int i = 0; i < fileList.Count; i++) {
                data[i] = fileList[i].ToStringArray();
            }
            return data;
        }
    }
}
