﻿using Prism;
using System;

namespace Zametek.Client.ProjectPlan.Wpf
{
    public partial class ArrowGraphSettingsManagerView
        : IActiveAware
    {
        #region Fields

        private bool m_IsActive;

        #endregion

        #region Ctors

        public ArrowGraphSettingsManagerView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public IArrowGraphSettingsManagerViewModel ViewModel
        {
            get
            {
                return DataContext as IArrowGraphSettingsManagerViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion

        #region IActiveAware Members

        public event EventHandler IsActiveChanged;

        public bool IsActive
        {
            get
            {
                return m_IsActive;
            }
            set
            {
                if (m_IsActive != value)
                {
                    m_IsActive = value;
                    IsActiveChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
