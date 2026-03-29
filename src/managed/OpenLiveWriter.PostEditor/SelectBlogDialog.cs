// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Drawing;
using System.Windows.Forms;
using OpenLiveWriter.BlogClient;
using OpenLiveWriter.Controls;
using OpenLiveWriter.CoreServices;
using OpenLiveWriter.CoreServices.Layout;
using OpenLiveWriter.Localization;

namespace OpenLiveWriter.PostEditor
{
    public class SelectBlogDialog : ApplicationDialog
    {
        private Label labelExplanation;
        private ComboBox comboBoxBlogs;
        private Button buttonOK;
        private Button buttonCancel;
        private System.ComponentModel.Container components = null;

        public string SelectedBlogId { get; private set; }

        public SelectBlogDialog(string unknownBlogId)
        {
            InitializeComponent();

            this.Text = Res.Get(StringId.ConfigWizardSelectWeblog);
            this.labelExplanation.Text = "The blog associated with this post could not be found. Please select a blog to use for this post:";
            this.buttonOK.Text = Res.Get(StringId.OKButtonText);
            this.buttonCancel.Text = Res.Get(StringId.CancelButton);

            PopulateBlogs();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void PopulateBlogs()
        {
            BlogDescriptor[] blogs = BlogSettings.GetBlogs(true);
            foreach (BlogDescriptor blog in blogs)
            {
                comboBoxBlogs.Items.Add(new BlogItem(blog));
            }

            if (comboBoxBlogs.Items.Count > 0)
            {
                comboBoxBlogs.SelectedIndex = 0;
            }
            else
            {
                buttonOK.Enabled = false;
            }
        }

        private void InitializeComponent()
        {
            this.labelExplanation = new System.Windows.Forms.Label();
            this.comboBoxBlogs = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.labelExplanation.Location = new System.Drawing.Point(12, 12);
            this.labelExplanation.Name = "labelExplanation";
            this.labelExplanation.Size = new System.Drawing.Size(360, 40);
            this.labelExplanation.TabIndex = 0;

            this.comboBoxBlogs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBlogs.Location = new System.Drawing.Point(15, 55);
            this.comboBoxBlogs.Name = "comboBoxBlogs";
            this.comboBoxBlogs.Size = new System.Drawing.Size(350, 21);
            this.comboBoxBlogs.TabIndex = 1;

            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(209, 95);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);

            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(290, 95);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;

            this.AcceptButton = this.buttonOK;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(384, 130);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxBlogs);
            this.Controls.Add(this.labelExplanation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectBlogDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxBlogs.SelectedItem is BlogItem item)
            {
                SelectedBlogId = item.Descriptor.Id;
            }
        }

        private class BlogItem
        {
            public BlogDescriptor Descriptor { get; }
            public BlogItem(BlogDescriptor descriptor) { Descriptor = descriptor; }
            public override string ToString() => Descriptor.Name;
        }
    }
}
