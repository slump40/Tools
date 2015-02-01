using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Runescape_Cache_Reset
{
    class MultiTool
    {
        string USERNAME = Environment.UserName;
        

 
        public void CacheDelete()
        {
            string path1 = (@"c:\.jagex_cache_32");
            string path2 = (@"c:\Users\" + USERNAME + @"\jagexcache");
            string success = ("Successfully Cleared C:/Users/" + USERNAME + "/jagexcache \nSuccessfully Cleared C:/.jagex_cache_32");

            //Begin Cache Delete Process
            if (Directory.Exists(path1))
            {
                DeleteDirectory(path1, true);
            }
            else
            {
                //Do Nothing
            }
            //Move To Next Directory
            if (Directory.Exists(path2))
            {
                DeleteDirectory(path2, true);
                MessageBox.Show(success);
            }
            else
            {
                MessageBox.Show(success);
            }
        }


        public void DeleteDirectory(string path)
        {
            DeleteDirectory(path, false);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }

                // Delete the file
                File.Delete(f);
            }

            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder

            Directory.Delete(path);


/* 
Delete all files from the folder 'c:\Games', but
keep all sub-folders and its files:
 
DeleteDirectory(@"c:\Games");
 
Delete the folder 'c:\Projects' and all of its content:

DeleteDirectory(@"c:\Projects", true);
 
Delete all files from the folder 'c:\Software', but
keep all sub-folders and its files:
 
DeleteDirectory(@"c:\Software", false);                                        */
        }


    }
}
