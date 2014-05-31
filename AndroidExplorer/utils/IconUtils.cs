using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AndroidExplorer {

    public class Win32 {
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(
                 string pszPath,
                 uint dwFileAttributes,
                 ref SHFILEINFO psfi,
                 uint cbSizeFileInfo,
                 uint uFlags);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHFILEINFO {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    class IconUtils {
        const uint SHGFI_LARGEICON = 0x00000000;
        const uint SHGFI_SMALLICON = 0x00000001;
        const uint SHGFI_OPENICON = 0x000000002; 
        const uint SHGFI_USEFILEATTRIBUTES = 0x00000010;
        const uint SHGFI_ICON = 0x00000100;
        const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DestroyIcon(IntPtr hIcon);

        public static Image FileAssociatedImage(string path, bool isLarge) {
            return FileAssociatedImage(path, isLarge, File.Exists(path));
        }

        public static Image FileAssociatedImage(string path, bool isLarge, bool isExist) {
            SHFILEINFO fileInfo = new SHFILEINFO();
            uint flags = SHGFI_ICON;
            if (!isLarge) flags |= SHGFI_SMALLICON;
            if (!isExist) flags |= SHGFI_USEFILEATTRIBUTES;
            try {
                SHGetFileInfo(path, 0, ref fileInfo, (uint)Marshal.SizeOf(fileInfo), flags);
                if (fileInfo.hIcon == IntPtr.Zero) {
                    return null;
                } else {
                    return Icon.FromHandle(fileInfo.hIcon).ToBitmap();
                }
            } finally {
                if (fileInfo.hIcon != IntPtr.Zero) {
                    DestroyIcon(fileInfo.hIcon);
                }
            }
        }

        public static Image getFolderIcon(string filePath) {
            SHFILEINFO shinfo = new SHFILEINFO();

            IntPtr imageLarge = Win32.SHGetFileInfo(
                filePath,
                0,
                ref shinfo,
                (uint)Marshal.SizeOf(shinfo),
                Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
            return Icon.FromHandle(shinfo.hIcon).ToBitmap();
        }
    }
}
