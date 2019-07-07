using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace VsmacHelper.Shared {
    public class ZipHelper {
        public void CreateZipFromFolder(string folderToZip, string destfilepath) {
            string destfilefullpath = new PathHelper().GetFullpath(destfilepath);

            if (File.Exists(destfilefullpath)) throw new InvalidDataException($"File already exists at {destfilefullpath}");

            // write to a temp file and then copy to final dest at the end
            string tempfile = Path.GetTempFileName();

            using (FileStream fsOut = File.Create(tempfile))
            using (ZipOutputStream zipStream = new ZipOutputStream(fsOut)) {
                zipStream.SetLevel(5);

                // This setting will strip the leading part of the folder path in the entries, to
                // make the entries relative to the starting folder.
                // To include the full path for each entry up to the drive root, assign folderOffset = 0.
                int folderOffset = folderToZip.Length + (folderToZip.EndsWith("\\", StringComparison.Ordinal) ? 0 : 1);

                CompressFolder(folderToZip, zipStream, folderOffset);

                zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
                zipStream.Close();
                //fsOut.Close();
            }

            File.Move(tempfile, destfilefullpath);
        }

        private void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset) {

            string[] files = Directory.GetFiles(path);

            foreach (string filename in files) {

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename)) {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders) {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }
        // Recurses down the folder structure
        //
        private void CompressFolder_old1(string path, ZipOutputStream zipStream, int folderOffset) {
            string[] files = Directory.GetFiles(path);

            zipStream.IsStreamOwner = false;
            foreach (string filename in files) {
                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset);
                entryName = ZipEntry.CleanName(entryName); 
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename)) {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();

                
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders) {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }
    }
}
