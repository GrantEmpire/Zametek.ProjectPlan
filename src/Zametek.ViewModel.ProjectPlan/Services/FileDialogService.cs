﻿using Microsoft.Win32;
using System.Globalization;
using System.IO;
using Zametek.Contract.ProjectPlan;

namespace Zametek.ViewModel.ProjectPlan
{
    public class FileDialogService
        : IFileDialogService
    {
        #region Private Method

        private bool OpenResult(
            string initialDirectory,
            string associatedFileType,
            string associatedFileExtension,
            FileDialog dlg)
        {
            dlg.InitialDirectory = initialDirectory;
            dlg.DefaultExt = associatedFileExtension;
            dlg.Filter = string.Format(CultureInfo.InvariantCulture, "{0} | *{1}", associatedFileType, associatedFileExtension);
            bool? result = dlg.ShowDialog();
            FileInfo fileInfo = null;
            DirectoryInfo directoryInfo = null;
            if (!string.IsNullOrEmpty(dlg.FileName))
            {
                fileInfo = new FileInfo(dlg.FileName);
                directoryInfo = fileInfo.Directory;
            }
            Filename = fileInfo != null ? fileInfo.FullName : null;
            Directory = directoryInfo != null ? directoryInfo.FullName : null;
            return result.GetValueOrDefault();
        }

        #endregion

        #region IFileDialogService Members

        public string Filename
        {
            get;
            private set;
        }

        public string Directory
        {
            get;
            private set;
        }

        public bool ShowSaveDialog(
            string initialDirectory,
            string associatedFileType,
            string associatedFileExtension)
        {
            return OpenResult(initialDirectory, associatedFileType, associatedFileExtension, new SaveFileDialog());
        }

        public bool ShowOpenDialog(
            string initialDirectory,
            string associatedFileType,
            string associatedFileExtension)
        {
            return OpenResult(initialDirectory, associatedFileType, associatedFileExtension, new OpenFileDialog());
        }

        #endregion
    }
}
