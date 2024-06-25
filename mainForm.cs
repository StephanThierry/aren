// Decompiled with JetBrains decompiler
// Type: ARen.mainForm
// Assembly: ARen, Version=1.0.2181.29537, Culture=neutral, PublicKeyToken=null
// MVID: 4B0F63B6-0638-4A43-9AD8-739724F75ED1
// Assembly location: C:\Windows\System32\ARen.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace ARen
{
    public class mainForm : Form
    {
    private TextBox txtLeftPanel;
    private TextBox txtRightPanel;
    private TextBox txtFolderPath;
    private Button btnGetFolder;
    private Button btnCleanup;
    private List<String> fileListOrg;
    private List<String> fileListMod;
    private Button btnMakeBat;
    private Button btnMakeTxt;
    private TextBox txtIndexTitle;
    private System.ComponentModel.Container components = (System.ComponentModel.Container) null;

    public mainForm() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
        if (disposing && this.components != null)
        this.components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.txtLeftPanel = new TextBox();
        this.txtRightPanel = new TextBox();
        this.txtFolderPath = new TextBox();
        this.btnGetFolder = new Button();
        this.btnCleanup = new Button();
        this.btnMakeBat = new Button();
        this.btnMakeTxt = new Button();
        this.txtIndexTitle = new TextBox();
        this.SuspendLayout();
        this.txtLeftPanel.Location = new Point(8, 80);
        this.txtLeftPanel.Multiline = true;
        this.txtLeftPanel.Name = "txtLeftPanel";
        this.txtLeftPanel.ReadOnly = true;
        this.txtLeftPanel.ScrollBars = ScrollBars.Vertical;
        this.txtLeftPanel.Size = new Size(304, 296);
        this.txtLeftPanel.TabIndex = 0;
        this.txtLeftPanel.Text = "";
        this.txtLeftPanel.WordWrap = false;
        this.txtRightPanel.Location = new Point(328, 80);
        this.txtRightPanel.Multiline = true;
        this.txtRightPanel.Name = "txtRightPanel";
        this.txtRightPanel.ReadOnly = true;
        this.txtRightPanel.ScrollBars = ScrollBars.Vertical;
        this.txtRightPanel.Size = new Size(304, 296);
        this.txtRightPanel.TabIndex = 1;
        this.txtRightPanel.Text = "";
        this.txtRightPanel.WordWrap = false;
        this.txtFolderPath.Location = new Point(8, 32);
        this.txtFolderPath.Name = "txtFolderPath";
        this.txtFolderPath.Size = new Size(248, 20);
        this.txtFolderPath.TabIndex = 2;
        this.txtFolderPath.Text = "";
        this.btnGetFolder.Location = new Point(264, 32);
        this.btnGetFolder.Name = "btnGetFolder";
        this.btnGetFolder.TabIndex = 3;
        this.btnGetFolder.Text = "Get Folder";
        this.btnGetFolder.Click += new EventHandler(this.btnGetFolder_Click);
        this.btnCleanup.Location = new Point(344, 32);
        this.btnCleanup.Name = "btnCleanup";
        this.btnCleanup.TabIndex = 4;
        this.btnCleanup.Text = "Cleanup";
        this.btnCleanup.Click += new EventHandler(this.btnCleanup_Click);
        this.btnMakeBat.Location = new Point(432, 8);
        this.btnMakeBat.Name = "btnMakeBat";
        this.btnMakeBat.Size = new Size(96, 23);
        this.btnMakeBat.TabIndex = 5;
        this.btnMakeBat.Text = "Make ARen.bat";
        this.btnMakeBat.Click += new EventHandler(this.btnMakeBat_Click);
        this.btnMakeTxt.Location = new Point(536, 48);
        this.btnMakeTxt.Name = "btnMakeTxt";
        this.btnMakeTxt.TabIndex = 6;
        this.btnMakeTxt.Text = "Make .txt";
        this.btnMakeTxt.Click += new EventHandler(this.btnMakeTxt_Click);
        this.txtIndexTitle.Location = new Point(432, 48);
        this.txtIndexTitle.Name = "txtIndexTitle";
        this.txtIndexTitle.Size = new Size(96, 20);
        this.txtIndexTitle.TabIndex = 7;
        this.txtIndexTitle.Text = "";
        this.AutoScaleBaseSize = new Size(5, 13);
        this.ClientSize = new Size(642, 381);
        this.Controls.Add((Control) this.txtIndexTitle);
        this.Controls.Add((Control) this.btnMakeTxt);
        this.Controls.Add((Control) this.btnMakeBat);
        this.Controls.Add((Control) this.btnCleanup);
        this.Controls.Add((Control) this.btnGetFolder);
        this.Controls.Add((Control) this.txtFolderPath);
        this.Controls.Add((Control) this.txtRightPanel);
        this.Controls.Add((Control) this.txtLeftPanel);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.Name = nameof (mainForm);
        this.Text = "ARen";
        this.ResumeLayout(false);
    }

    [STAThread]
    private static void Main() => Application.Run((Form) new mainForm());

    private void btnGetFolder_Click(object sender, EventArgs e)
    {
            string output = "";
        this.txtLeftPanel.Text = "";
        this.txtRightPanel.Text = "";
        if (this.txtFolderPath.Text == "")
        this.txtFolderPath.Text = ".";
        string[] files = Directory.GetFiles(this.txtFolderPath.Text);
        this.txtIndexTitle.Text = mainForm.GetDiskVolumeName(Directory.GetDirectoryRoot(this.txtFolderPath.Text)) + ".txt";
        this.fileListOrg = new List<String>();
        this.fileListMod = new List<String>();
        foreach (string str1 in files)
        {
        int startIndex = str1.LastIndexOf("\\") + 1;
        string str2 = str1.Substring(startIndex, str1.Length - startIndex);
        this.fileListOrg.Add(str2);
        this.fileListMod.Add(str2);
        TextBox txtLeftPanel = this.txtLeftPanel;
        txtLeftPanel.Text = txtLeftPanel.Text + str2 + "\r\n";
        }
        StreamReader streamReader = File.OpenText("aren.txt");
        string str;
        while ((str = streamReader.ReadLine()) != null)
        {
                output += str + "\r\n";
        }
            this.txtRightPanel.Text = output;
            streamReader.Close();
    }

    private void btnCleanup_Click(object sender, EventArgs e)
    {
        string output = "";
        #region loadReplaceStringsFromTxtToMem
        string[] lookFor = this.txtRightPanel.Text.Split('\n');
        string[] replaceWith = new string[lookFor.Length];
        for (int index = 0; index < lookFor.Length; ++index)
        {
        lookFor[index] = lookFor[index].Replace("\r", "");
        replaceWith[index] = "";
        if (lookFor[index].IndexOf("|") > 0)
        {
            int length = lookFor[index].IndexOf("|");
            replaceWith[index] = lookFor[index].Substring(length + 1, lookFor[index].Length - (length + 1));
            lookFor[index] = lookFor[index].Substring(0, length);
        }
        }
        this.txtRightPanel.Text = "";
        #endregion

        for (int fileListIndex = 0; fileListIndex < this.fileListOrg.Count; ++fileListIndex)
        {
        string baseFilename = this.fileListMod[fileListIndex].ToString();
        int num = baseFilename.LastIndexOf(".");
        string extention = "";
        if (num != -1)
        {
            extention = baseFilename.Substring(num, baseFilename.Length - num);
            baseFilename = baseFilename.Substring(0, num);
        }

        for (int replaceListIndex = 0; replaceListIndex < lookFor.Length; ++replaceListIndex)
        {
            if (lookFor[replaceListIndex] != "")
                baseFilename = baseFilename.Replace(lookFor[replaceListIndex], replaceWith[replaceListIndex]).Trim();
        }
        this.fileListMod[fileListIndex] = baseFilename + extention; //re-add extention
        output += this.fileListMod[fileListIndex].ToString() + "\r\n";
        }
        this.txtRightPanel.Text = output;
    }

    private void btnMakeBat_Click(object sender, EventArgs e)
    {
        StreamWriter text = File.CreateText(this.txtFolderPath.Text + "\\aren.bat");
        int num = 0;
        foreach (string str in this.fileListOrg)
        {
        if (str.Length > num)
            num = str.Length;
        }
        for (int index = 0; index < this.fileListMod.Count; ++index)
        {
        if (this.fileListOrg[index].ToString().CompareTo(this.fileListMod[index].ToString()) != 0)
            text.WriteLine("ren \"" + this.fileListOrg[index] + "\" " + this.makeSpaces(num - this.fileListOrg[index].ToString().Length) + "\"" + this.fileListMod[index] + "\"");
        }
        text.Close();
        this.txtRightPanel.Text = "aren.bat - Done!";
    }

    public static ulong GetDiskSpace(string drive)
    {
        return Convert.ToUInt64(mainForm.GetDriveInfo(drive, "FreeSpace"));
    }

    public static string GetDiskVolumeName(string drive)
    {
        return mainForm.GetDriveInfo(drive, "VolumeName");
    }

    private static string GetDriveInfo(string drive, string identifier)
    {
        ManagementObject managementObject = drive != null && drive.Length != 0 ? new ManagementObject("win32_logicaldisk.deviceid=\"" + (object) drive[0] + ":\"") : throw new ArgumentNullException(nameof (drive));
        return managementObject == null ? "0" : managementObject[identifier].ToString();
    }

    private string makeSpaces(int nr)
    {
        string str = "";
        for (int index = 0; index < nr; ++index)
        str += " ";
        return str;
    }

    private void btnMakeTxt_Click(object sender, EventArgs e)
    {
        StreamWriter text = File.CreateText(".\\" + this.txtIndexTitle.Text);
        string[] directories = Directory.GetDirectories(this.txtFolderPath.Text);
        int num = 0;
        foreach (string str in directories)
        {
        if (str.Length > num)
            num = str.Length;
        }
        if (this.txtFolderPath.Text == "")
        this.txtFolderPath.Text = ".";
        string[] files = Directory.GetFiles(this.txtFolderPath.Text);
        foreach (string str1 in directories)
        {
        int startIndex = str1.LastIndexOf("\\") + 1;
        string str2 = str1.Substring(startIndex, str1.Length - startIndex);
        text.WriteLine(str2 + this.makeSpaces(num - str2.Length) + " <DIR>");
        }
        foreach (string str3 in files)
        {
        int startIndex = str3.LastIndexOf("\\") + 1;
        string str4 = str3.Substring(startIndex, str3.Length - startIndex);
        text.WriteLine(str4);
        }
        text.Close();
        this.txtRightPanel.Text = this.txtIndexTitle.Text + " - Done!";
    }
    }
}
