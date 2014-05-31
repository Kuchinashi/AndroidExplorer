using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidExplorer {
    class FileInfo {
        public const int TYPE_FILE = 0;
        public const int TYPE_DIR = 1;
        public const int TYPE_LINK = 2;

        private int type;
        private string fileName;
        private string absoluteFilePath;
        private string user;
        private string group;
        private long size;
        private string time;
        private string link;

        public FileInfo(int type, string fileName, string absoluteFilePath, string user, string group,
           long size, string time, string link) {
            this.type = type;
            this.fileName = fileName;
            this.absoluteFilePath = absoluteFilePath;
            this.user = user;
            this.group = group;
            this.size = size;
            this.time = time;
            this.link = link;
        }

        public FileInfo() {

        }
        public new int GetType() {
            return type;
        }

        public string GetFileName() {
            return fileName;
        }

        public string GetAbsoluteFilePath() {
            return absoluteFilePath;
        }

        public string GetUser() {
            return user;
        }

        public string GetGroup() {
            return group;
        }

        public long GetSize() {
            return size;
        }

        public string GetTime() {
            return time;
        }

        public string GetLink() {
            return link;
        }

        public void SetType(int type) {
            this.type = type;
        }

        public void SetFileName(string fileName) {
            this.fileName = fileName;
        }

        public void SetAbsoluteFilePath(string current, string fileName) {
            if (current.EndsWith("/")) {
                this.absoluteFilePath = current + fileName;
                return;
            }
            this.absoluteFilePath = current + "/" + fileName;
        }

        public void SetAbsoluteFilePath(string absoluteFilePath) {
            this.absoluteFilePath = absoluteFilePath;
        }

        public void SetUser(string user) {
            this.user = user;
        }

        public void SetGroup(string group) {
            this.group = group;
        }

        public void SetSize(long size) {
            this.size = size;
        }

        public void SetTime(string time) {
            this.time = time;
        }

        public void SetLink(string link) {
            this.link = link;
        }

        public override string ToString() {
            return "FileInfo [type=" + type + ", fileName=" + fileName + ", absoluteFilePath="
                    + absoluteFilePath + ", user=" + user + ", group=" + group + ", size=" + size
                    + ", time=" + time + ", link=" + link + "]";
        }

        public string[] ToStringArray() {
            return new string[] {
                fileName,
                user,
                group,
                size.ToString(),
                time,
                link
            };
        }
    }


}
