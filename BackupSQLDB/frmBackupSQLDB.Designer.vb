<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackupSQLDB
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnBrowseFolder = New System.Windows.Forms.Button()
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.txtBackupFolder = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.txtDatabaseName = New System.Windows.Forms.TextBox()
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDatabasePassword = New System.Windows.Forms.Label()
        Me.lblUserID = New System.Windows.Forms.Label()
        Me.lblDatabaseName = New System.Windows.Forms.Label()
        Me.lblServarName = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rdiSharedPath = New System.Windows.Forms.RadioButton()
        Me.rdiPhysicalPath = New System.Windows.Forms.RadioButton()
        Me.txtSharedPath = New System.Windows.Forms.TextBox()
        Me.txtPhysicalPath = New System.Windows.Forms.TextBox()
        Me.btnBrowsePhysicalPath = New System.Windows.Forms.Button()
        Me.txtSharedUser = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSharedPassword = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSharedDomain = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtSharedDomain)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtSharedPassword)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtSharedUser)
        Me.GroupBox1.Controls.Add(Me.btnBrowsePhysicalPath)
        Me.GroupBox1.Controls.Add(Me.txtPhysicalPath)
        Me.GroupBox1.Controls.Add(Me.txtSharedPath)
        Me.GroupBox1.Controls.Add(Me.rdiPhysicalPath)
        Me.GroupBox1.Controls.Add(Me.rdiSharedPath)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnBrowseFolder)
        Me.GroupBox1.Controls.Add(Me.btnBackup)
        Me.GroupBox1.Controls.Add(Me.txtBackupFolder)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.txtUserID)
        Me.GroupBox1.Controls.Add(Me.txtDatabaseName)
        Me.GroupBox1.Controls.Add(Me.txtServerName)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblDatabasePassword)
        Me.GroupBox1.Controls.Add(Me.lblUserID)
        Me.GroupBox1.Controls.Add(Me.lblDatabaseName)
        Me.GroupBox1.Controls.Add(Me.lblServarName)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(712, 182)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Database Information"
        '
        'btnBrowseFolder
        '
        Me.btnBrowseFolder.Location = New System.Drawing.Point(586, 141)
        Me.btnBrowseFolder.Name = "btnBrowseFolder"
        Me.btnBrowseFolder.Size = New System.Drawing.Size(28, 23)
        Me.btnBrowseFolder.TabIndex = 11
        Me.btnBrowseFolder.Text = "..."
        Me.btnBrowseFolder.UseVisualStyleBackColor = True
        '
        'btnBackup
        '
        Me.btnBackup.Location = New System.Drawing.Point(618, 15)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(84, 67)
        Me.btnBackup.TabIndex = 10
        Me.btnBackup.Text = "Backup"
        Me.btnBackup.UseVisualStyleBackColor = True
        '
        'txtBackupFolder
        '
        Me.txtBackupFolder.Location = New System.Drawing.Point(101, 143)
        Me.txtBackupFolder.Name = "txtBackupFolder"
        Me.txtBackupFolder.Size = New System.Drawing.Size(484, 20)
        Me.txtBackupFolder.TabIndex = 9
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(414, 38)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(197, 20)
        Me.txtPassword.TabIndex = 8
        '
        'txtUserID
        '
        Me.txtUserID.Location = New System.Drawing.Point(101, 38)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(197, 20)
        Me.txtUserID.TabIndex = 7
        '
        'txtDatabaseName
        '
        Me.txtDatabaseName.Location = New System.Drawing.Point(414, 15)
        Me.txtDatabaseName.Name = "txtDatabaseName"
        Me.txtDatabaseName.Size = New System.Drawing.Size(197, 20)
        Me.txtDatabaseName.TabIndex = 6
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(101, 15)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(197, 20)
        Me.txtServerName.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 146)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Temp Folder : "
        '
        'lblDatabasePassword
        '
        Me.lblDatabasePassword.AutoSize = True
        Me.lblDatabasePassword.Location = New System.Drawing.Point(315, 41)
        Me.lblDatabasePassword.Name = "lblDatabasePassword"
        Me.lblDatabasePassword.Size = New System.Drawing.Size(62, 13)
        Me.lblDatabasePassword.TabIndex = 3
        Me.lblDatabasePassword.Text = "Password : "
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Location = New System.Drawing.Point(17, 41)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(52, 13)
        Me.lblUserID.TabIndex = 2
        Me.lblUserID.Text = "User ID : "
        '
        'lblDatabaseName
        '
        Me.lblDatabaseName.AutoSize = True
        Me.lblDatabaseName.Location = New System.Drawing.Point(315, 18)
        Me.lblDatabaseName.Name = "lblDatabaseName"
        Me.lblDatabaseName.Size = New System.Drawing.Size(93, 13)
        Me.lblDatabaseName.TabIndex = 1
        Me.lblDatabaseName.Text = "Database Name : "
        '
        'lblServarName
        '
        Me.lblServarName.AutoSize = True
        Me.lblServarName.Location = New System.Drawing.Point(17, 18)
        Me.lblServarName.Name = "lblServarName"
        Me.lblServarName.Size = New System.Drawing.Size(78, 13)
        Me.lblServarName.TabIndex = 0
        Me.lblServarName.Text = "Server Name : "
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Location = New System.Drawing.Point(12, 200)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ReadOnly = True
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessage.Size = New System.Drawing.Size(711, 138)
        Me.txtMessage.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Backup To : "
        '
        'rdiSharedPath
        '
        Me.rdiSharedPath.AutoSize = True
        Me.rdiSharedPath.Checked = True
        Me.rdiSharedPath.Location = New System.Drawing.Point(101, 85)
        Me.rdiSharedPath.Name = "rdiSharedPath"
        Me.rdiSharedPath.Size = New System.Drawing.Size(84, 17)
        Me.rdiSharedPath.TabIndex = 13
        Me.rdiSharedPath.TabStop = True
        Me.rdiSharedPath.Text = "Shared Path"
        Me.rdiSharedPath.UseVisualStyleBackColor = True
        '
        'rdiPhysicalPath
        '
        Me.rdiPhysicalPath.AutoSize = True
        Me.rdiPhysicalPath.Location = New System.Drawing.Point(101, 62)
        Me.rdiPhysicalPath.Name = "rdiPhysicalPath"
        Me.rdiPhysicalPath.Size = New System.Drawing.Size(89, 17)
        Me.rdiPhysicalPath.TabIndex = 14
        Me.rdiPhysicalPath.Text = "Physical Path"
        Me.rdiPhysicalPath.UseVisualStyleBackColor = True
        '
        'txtSharedPath
        '
        Me.txtSharedPath.Location = New System.Drawing.Point(191, 84)
        Me.txtSharedPath.Name = "txtSharedPath"
        Me.txtSharedPath.Size = New System.Drawing.Size(420, 20)
        Me.txtSharedPath.TabIndex = 15
        '
        'txtPhysicalPath
        '
        Me.txtPhysicalPath.Enabled = False
        Me.txtPhysicalPath.Location = New System.Drawing.Point(191, 61)
        Me.txtPhysicalPath.Name = "txtPhysicalPath"
        Me.txtPhysicalPath.Size = New System.Drawing.Size(394, 20)
        Me.txtPhysicalPath.TabIndex = 16
        '
        'btnBrowsePhysicalPath
        '
        Me.btnBrowsePhysicalPath.Enabled = False
        Me.btnBrowsePhysicalPath.Location = New System.Drawing.Point(586, 59)
        Me.btnBrowsePhysicalPath.Name = "btnBrowsePhysicalPath"
        Me.btnBrowsePhysicalPath.Size = New System.Drawing.Size(28, 23)
        Me.btnBrowsePhysicalPath.TabIndex = 17
        Me.btnBrowsePhysicalPath.Text = "..."
        Me.btnBrowsePhysicalPath.UseVisualStyleBackColor = True
        '
        'txtSharedUser
        '
        Me.txtSharedUser.Location = New System.Drawing.Point(191, 106)
        Me.txtSharedUser.Name = "txtSharedUser"
        Me.txtSharedUser.Size = New System.Drawing.Size(107, 20)
        Me.txtSharedUser.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(110, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Shared User : "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(312, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Password : "
        '
        'txtSharedPassword
        '
        Me.txtSharedPassword.Location = New System.Drawing.Point(375, 106)
        Me.txtSharedPassword.Name = "txtSharedPassword"
        Me.txtSharedPassword.Size = New System.Drawing.Size(107, 20)
        Me.txtSharedPassword.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(500, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Domain : "
        '
        'txtSharedDomain
        '
        Me.txtSharedDomain.Location = New System.Drawing.Point(557, 106)
        Me.txtSharedDomain.Name = "txtSharedDomain"
        Me.txtSharedDomain.Size = New System.Drawing.Size(145, 20)
        Me.txtSharedDomain.TabIndex = 22
        '
        'frmBackupSQLDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 345)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmBackupSQLDB"
        Me.Text = "Backup SQL DB"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnBackup As Button
    Friend WithEvents txtBackupFolder As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtUserID As TextBox
    Friend WithEvents txtDatabaseName As TextBox
    Friend WithEvents txtServerName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblDatabasePassword As Label
    Friend WithEvents lblUserID As Label
    Friend WithEvents lblDatabaseName As Label
    Friend WithEvents lblServarName As Label
    Friend WithEvents txtMessage As TextBox
    Friend WithEvents btnBrowseFolder As Button
    Friend WithEvents btnBrowsePhysicalPath As Button
    Friend WithEvents txtPhysicalPath As TextBox
    Friend WithEvents txtSharedPath As TextBox
    Friend WithEvents rdiPhysicalPath As RadioButton
    Friend WithEvents rdiSharedPath As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtSharedDomain As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtSharedPassword As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSharedUser As TextBox
End Class
