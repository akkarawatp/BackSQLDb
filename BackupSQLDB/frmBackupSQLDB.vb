Imports System.Data.SqlClient
Imports System.IO
Imports System.IO.Packaging
'Imports UNCAccessWithCredentials.UNCAccessWithCredentials

Public Class frmBackupSQLDB
    Dim INIFileName As String = Application.StartupPath & "\ConfigDB.ini"
    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        If txtServerName.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Server Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If txtDatabaseName.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Database Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If txtUserID.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ User ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If txtPassword.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If txtBackupFolder.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Backup Folder", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If txtBackupFolder.Text.EndsWith("\") = False Then
            txtBackupFolder.Text += "\"
        End If


        Dim ini As New Org.Mentalis.Files.IniReader(INIFileName)
        ini.Section = "DbSetting"
        ini.Write("ServerName", txtServerName.Text.Trim)
        ini.Write("DatabaseName", txtDatabaseName.Text.Trim)
        ini.Write("UserID", txtUserID.Text.Trim)
        ini.Write("Password", txtPassword.Text.Trim)
        ini.Write("BackupTempPath", txtBackupFolder.Text.Trim)
        ini.Write("BackupToSharedPath", IIf(rdiSharedPath.Checked = True, 1, 0))
        ini.Write("BackupPath", txtPhysicalPath.Text)

        ini.Section = "SharedPath"
        ini.Write("BackupSharedPath", txtSharedPath.Text)
        ini.Write("SharedUser", txtSharedUser.Text)
        ini.Write("SharedPassword", txtSharedPassword.Text)
        ini.Write("SharedDomain", txtSharedDomain.Text)
        ini = Nothing

        Dim dbName() As String = txtDatabaseName.Text.Trim.Split(";")
        If dbName.Length > 0 Then
            txtMessage.Text = ""
            For Each DatabaseName As String In dbName
                Dim BackupFileName As String = txtBackupFolder.Text & DatabaseName & ".bak"
                BackupDB(DatabaseName, BackupFileName)
            Next
        End If
    End Sub





#Region "Database Function"
    Private Function BackupDB(DatabaseName As String, BackupFileName As String) As String
        Dim ret As String = "false"
        Dim sql As String = ""

        Try
            sql = "BACKUP DATABASE [" & DatabaseName & "] "
            sql += " TO  DISK = N'" & BackupFileName & "' WITH NOFORMAT, "
            sql += " NOINIT,  NAME = N'" & DatabaseName & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10 "

            txtMessage.Text += "Backup Database : " & DatabaseName & vbNewLine
            ExecuteBackupCommand(sql, DatabaseName)
            Application.DoEvents()
        Catch ex As Exception
            ret = "false|Exception " & ex.Message & vbNewLine & ex.StackTrace
        End Try

        Return ret
    End Function

    Private Function GetConnString(DatabaseName As String) As String
        Dim ini As New Org.Mentalis.Files.IniReader(INIFileName)
        ini.Section = "DbSetting"

        Dim connString As String = "Data Source=" & txtServerName.Text.Trim & ";Initial Catalog=" & DatabaseName.Trim & ";User ID=" & txtUserID.Text.Trim & ";Password=" & txtPassword.Text.Trim & ";"

        Return connString
    End Function

    Private Sub OnExecuteInfoMessage(ByVal sender As Object, ByVal args As SqlInfoMessageEventArgs)
        Dim sqlEvent As SqlError
        For Each sqlEvent In args.Errors
            'Events.FireInformation(sqlEvent.Number, sqlEvent.Procedure, sqlEvent.Message, "", 0, False)
            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & sqlEvent.Message & vbNewLine
            Application.DoEvents()
        Next
    End Sub


    Private Sub ExecuteBackupCommand(ByVal sql As String, DatabaseName As String)
        Dim cmd As New SqlCommand
        Dim LetClose As Boolean = False

        Try
            Dim conn As SqlConnection = GetConnection(GetConnString(DatabaseName))
            AddHandler conn.InfoMessage, New SqlInfoMessageEventHandler(AddressOf OnExecuteInfoMessage)

            LetClose = True
            BuildCommand(cmd, conn, Nothing, CommandType.Text, sql, Nothing)
            cmd.ExecuteNonQuery()

            If LetClose = True Then
                cmd.Dispose()
                conn.Close()
                SqlConnection.ClearAllPools()
            End If
        Catch ex As ApplicationException
            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "ApplicationException : " & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
        Catch ex As SqlException
            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "SqlException : " & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
        Catch ex As Exception
            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Exception : " & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
        End Try
    End Sub


    Private Function GetConnection(ByVal connString As String) As SqlConnection
        Dim conn As SqlConnection = Nothing
        Try
            conn = New SqlConnection(connString)
            conn.Open()
            'Return conn
        Catch ex As ApplicationException
            'Throw ex
        Catch ex As SqlException
            Try
                conn = New SqlConnection(connString)
                conn.Open()
                'Return conn
            Catch ex1 As SqlException
            End Try
        Catch ex As Exception
        End Try

        Return conn
    End Function

    Private Function SetParameter(ParameterName As String, pType As SqlDbType, ParameterValue As Object) As SqlParameter
        Dim p As New SqlParameter(ParameterName, pType)
        If Convert.IsDBNull(ParameterValue) = False Then
            p.Value = ParameterValue
        Else
            p.Value = DBNull.Value
        End If
        Return p
    End Function

    Private Shared Sub BuildCommand(ByVal cmd As SqlCommand, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms() As SqlParameter)
        If conn.State <> ConnectionState.Open Then
            Try
                conn.Open()
            Catch ex As SqlException
                'Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As ApplicationException
                'Throw (ex)
            Catch ex As Exception
                'Throw New ApplicationException(ErrorConnection, ex)

            End Try
        End If

        Try
            cmd.Connection = conn
        Catch ex As Exception
            'Throw New ApplicationException(ErrorSetCommandConnection, ex)

        End Try
        cmd.CommandText = cmdText

        If trans IsNot Nothing Then
            cmd.Transaction = trans
        End If

        Try
            cmd.CommandType = cmdType
            cmd.CommandTimeout = 240
        Catch ex As ArgumentException
            'Throw New ApplicationException(ErrorInvalidCommandType, ex)

        End Try

        If cmdParms IsNot Nothing Then
            For Each parm As SqlParameter In cmdParms

                Try
                    If parm IsNot Nothing Then
                        cmd.Parameters.Add(parm)
                    End If
                Catch ex As ArgumentNullException
                    'Throw New ApplicationException(ErrorNullParameter, ex)

                Catch ex As ArgumentException
                    'Throw New ApplicationException(ErrorDuplicateParameter, ex)

                End Try
            Next
        End If
    End Sub

    Private Sub frmBackupSQLDB_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        FillInDatabaseInfo()

        txtMessage.Text = ""

        If My.Application.CommandLineArgs.Count > 0 Then
            Dim ini As New Org.Mentalis.Files.IniReader(INIFileName)
            ini.Section = "DbSetting"

            Dim BackupTempPath As String = ini.ReadString("BackupTempPath")
            Dim BackupHisMonth As Int16 = ini.ReadString("BackupHisMonth")

            If IO.Directory.Exists(BackupTempPath) = False Then
                IO.Directory.CreateDirectory(BackupTempPath)
            End If

            Dim DbName() As String = txtDatabaseName.Text.Split(";")
            If DbName.Length > 0 Then
                Try
                    Dim vDateNow As DateTime = DateTime.Now
                    For Each DatabaseName As String In DbName
                        Dim BackupFileName As String = BackupTempPath & "Backup_" & DatabaseName & "_" & vDateNow.ToString("yyyyMMdd_HHmm") & ".bak"
                        Dim ZipFileName As String = BackupTempPath & "Backup_" & DatabaseName & "_" & vDateNow.ToString("yyyyMMdd_HHmm") & ".zip"
                        Try
                            BackupDB(DatabaseName, BackupFileName)
                            If File.Exists(BackupFileName) = True Then
                                Threading.Thread.Sleep(5000) 'รอซัก 5 วิ
                                CreateZipFile(BackupFileName, ZipFileName)
                            End If
                        Catch ex As Exception
                            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Exception1 :" & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
                        End Try
                    Next


                    If ini.ReadString("BackupToSharedPath") = 1 Then
                        Using unc As New UNCAccessWithCredentials.UNCAccessWithCredentials
                            ini.Section = "SharedPath"

                            Dim SharedPath As String = ini.ReadString("BackupSharedPath")
                            Dim SharedUser As String = ini.ReadString("SharedUser")
                            Dim SharedPassword As String = ini.ReadString("SharedPassword")
                            Dim SharedDomain As String = ini.ReadString("SharedDomain")

                            If unc.NetUseWithCredentials(SharedPath, SharedUser, SharedDomain, SharedPassword) = True Then
                                For Each ZipFileName As String In Directory.GetFiles(BackupTempPath, "*.zip")
                                    ' Dim ZipFileName As String = BackupPath & "Backup_" & DatabaseName & "_" & vDateNow.ToString("yyyyMMdd_HHmm") & ".zip"
                                    If File.Exists(ZipFileName) = True Then
                                        MoveZipfileToSharedPath(ZipFileName, SharedPath)
                                    End If
                                Next
                                SaveBackupLog(vDateNow, SharedPath)
                            Else
                                'MessageBox.Show(unc.LastError)
                            End If
                        End Using
                    Else
                        Dim BackupPath As String = ini.ReadString("BackupPath")
                        If Directory.Exists(BackupPath) = False Then
                            Directory.CreateDirectory(BackupPath)
                        End If

                        For Each ZipFileName As String In Directory.GetFiles(BackupTempPath, "*.zip")
                            ' Dim ZipFileName As String = BackupPath & "Backup_" & DatabaseName & "_" & vDateNow.ToString("yyyyMMdd_HHmm") & ".zip"
                            If File.Exists(ZipFileName) = True Then
                                MoveZipfileToSharedPath(ZipFileName, BackupPath)
                            End If
                        Next
                        SaveBackupLog(vDateNow, BackupPath)
                    End If

                Catch ex As Exception
                    txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Exception2 UNCAccessWithCredentials :" & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
                End Try
            End If
            ini = Nothing

            'Delete Backup History
            DeleteBackupHistory(BackupHisMonth)
            Application.Exit()
        End If
    End Sub

#Region "Move Zip File To Shared Path"
    Private Sub MoveZipfileToSharedPath(ZipFileName As String, DestPath As String)
        txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "MoveZipfileToSharedPath:" & ZipFileName & "  DestPath:" & DestPath & vbNewLine
        Dim DestFileName As String = DestPath & "\" & Path.GetFileName(ZipFileName)
        File.Copy(ZipFileName, DestFileName, True)

        'เสร็จแล้วให้ลบ Zip File เลย
        If File.Exists(DestFileName) = True Then
            Try
                File.SetAttributes(ZipFileName, FileAttributes.Normal)
                File.Delete(ZipFileName)
            Catch ex As Exception
                txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Exception Delete ZipFile MoveZipfileToSharedPath:" & ZipFileName & vbNewLine & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
            End Try
        End If
    End Sub
#End Region

#Region "Create Zip File"
    Private Sub CreateZipFile(BackupFileName As String, ZipFileName As String)
        'Create Zip File
        If File.Exists(ZipFileName) = True Then
            File.Delete(ZipFileName)
        End If

        If File.Exists(BackupFileName) = True Then
            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Create zip file " & ZipFileName & vbNewLine
            AddFileStreamToZip(BackupFileName, ZipFileName)

            '######################################################################
            'สั่ง Delete File ขยะที่อยู่ใน Zip
            'ถ้ามีหลายๆ ไฟล์เอามารวมเป็น zip เดียว ก็ให้ AddFileToZip ให้ครบทุกไฟล์ก่อน
            Dim FileName As New ArrayList
            Dim zip As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            For Each pkgPart As PackagePart In zip.GetParts()
                FileName.Add(pkgPart.Uri.ToString())
            Next
            zip.Close()

            'Open the zip file
            Dim zip2 As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            For Each Str As String In FileName
                If IO.Path.GetExtension(Str) = ".psmdcp" OrElse Str.IndexOf("_rels") > -1 Then
                    Dim partUri As New Uri(Str, System.UriKind.Relative)
                    zip2.DeletePart(partUri)
                End If
            Next
            zip2.Close()
            '######################################################################

            If File.Exists(ZipFileName) = True Then
                Try
                    File.SetAttributes(BackupFileName, FileAttributes.Normal)
                    File.Delete(BackupFileName)

                    txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Delete Backup file " & ZipFileName & vbNewLine
                Catch ex As Exception
                    txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Delete Fail Backup file " & BackupFileName & vbNewLine & ex.Message & vbNewLine & ex.StackTrace & vbNewLine
                End Try
            End If
        End If
    End Sub


    Private Function AddFileStreamToZip(ByVal SourceFile As String, ByVal ZipFileName As String, Optional ByVal uri As String = "") As ArchiveFile

        'ArchiveFile is a custom class that stores the File Name, Type, Modified, Uri,
        '  and gets the correct system icon.
        Dim archFile As New ArchiveFile(SourceFile)

        'Open the zip file if it exists, else create a new one
        Dim zip As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)

        'If no Uri was provided, then create one from the existing file path
        '   An optional route would be to just use the file name as the Uri, but then
        '   it will extract to the root directory. 
        If uri <> "" Then
            'Change all backward slashes to forward slashes
            uri = uri.Replace("\", "/")
        Else
            'Uri was not provided, so use the name of the file:
            uri = String.Concat("/", IO.Path.GetFileName(SourceFile))

            'Spaces cannot appear in the file name, so replace them with underscores.
            uri = uri.Replace(" ", "_")
        End If

        Dim partUri As New Uri(uri, UriKind.Relative)
        Dim contentType As String = Net.Mime.MediaTypeNames.Application.Zip   'constant: "application/zip"

        'The PackagePart contains the information:
        '   Where to extract the file when it's extracted (partUri)
        '   The type of content stream (MIME type) - (contentType)
        '   The type of compression to use (CompressionOption.Normal)
        Dim pkgPart As PackagePart = zip.CreatePart(partUri, contentType, CompressionOption.Normal)
        'Compress and write the bytes to the zip file
        pkgPart.Package.PackageProperties.Modified = archFile.Modified

        Using fileStream As New FileStream(SourceFile, FileMode.Open, FileAccess.Read)
            CopyStream(fileStream, pkgPart.GetStream())
        End Using

        'store the Uri in the Custom ArchiveFile
        archFile.Uri = uri

        zip.Close()  'Close the zip file

        Return archFile

    End Function

    Private Shared Sub CopyStream(ByVal source As Stream, ByVal target As Stream)
        Const bufSize As Integer = 1024 * 16
        Dim buf(bufSize - 1) As Byte
        Dim bytesRead As Integer = 0
        bytesRead = source.Read(buf, 0, bufSize)
        Do While bytesRead > 0
            target.Write(buf, 0, bytesRead)
            bytesRead = source.Read(buf, 0, bufSize)
        Loop
    End Sub ' end:CopyStream()


    'This sub demonstrates adding files to a zip
    Private Function AddFileToZip(ByVal filePath As String, ByVal ZipFileName As String, Optional ByVal uri As String = "") As ArchiveFile

        'ArchiveFile is a custom class that stores the File Name, Type, Modified, Uri,
        '  and gets the correct system icon.
        Dim archFile As New ArchiveFile(filePath)

        'Open the zip file if it exists, else create a new one
        Dim zip As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)

        'If no Uri was provided, then create one from the existing file path
        '   An optional route would be to just use the file name as the Uri, but then
        '   it will extract to the root directory. 
        If uri <> "" Then
            'Change all backward slashes to forward slashes
            uri = uri.Replace("\", "/")
        Else
            'Uri was not provided, so use the name of the file:
            uri = String.Concat("/", IO.Path.GetFileName(filePath))

            'Spaces cannot appear in the file name, so replace them with underscores.
            uri = uri.Replace(" ", "_")
        End If

        Dim partUri As New Uri(uri, UriKind.Relative)
        Dim contentType As String = Net.Mime.MediaTypeNames.Application.Zip   'constant: "application/zip"

        'The PackagePart contains the information:
        '   Where to extract the file when it's extracted (partUri)
        '   The type of content stream (MIME type) - (contentType)
        '   The type of compression to use (CompressionOption.Normal)
        Dim pkgPart As PackagePart = zip.CreatePart(partUri, contentType, CompressionOption.Normal)

        'Read all of the bytes from the file to add to the zip file
        Dim bites As Byte() = File.ReadAllBytes(filePath)

        'Compress and write the bytes to the zip file
        pkgPart.Package.PackageProperties.Modified = archFile.Modified
        pkgPart.GetStream().Write(bites, 0, bites.Length)

        'store the Uri in the Custom ArchiveFile
        archFile.Uri = uri

        zip.Close()  'Close the zip file

        Return archFile

    End Function

#End Region


    Private Sub DeleteBackupHistory(BackupHisMonth As Int16)
        If BackupHisMonth > 0 Then
            Dim ini As New Org.Mentalis.Files.IniReader(INIFileName)
            ini.Section = "DbSetting"

            If ini.ReadString("BackupToSharedPath") = 1 Then
                Using unc As New UNCAccessWithCredentials.UNCAccessWithCredentials
                    ini.Section = "SharedPath"
                    Dim SharedPath As String = ini.ReadString("BackupSharedPath")
                    Dim SharedUser As String = ini.ReadString("SharedUser")
                    Dim SharedPassword As String = ini.ReadString("SharedPassword")
                    Dim SharedDomain As String = ini.ReadString("SharedDomain")

                    If unc.NetUseWithCredentials(SharedPath, SharedUser, SharedDomain, SharedPassword) = True Then
                        For Each f As String In IO.Directory.GetFiles(SharedPath)
                            Dim fInfo As New IO.FileInfo(f)
                            If DateAdd(DateInterval.Month, BackupHisMonth, fInfo.LastAccessTime) < DateTime.Today Then
                                Try
                                    IO.File.SetAttributes(f, IO.FileAttributes.Normal)
                                    IO.File.Delete(f)
                                Catch ex As Exception
                                    txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Exception Delete DeleteBackupHistory : FileName=" & f & vbNewLine & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
                                End Try
                            End If
                            fInfo = Nothing
                        Next
                    End If
                    ini = Nothing
                End Using
            Else
                Dim BackupPath = ini.ReadString("BackupPath")
                For Each f As String In IO.Directory.GetFiles(BackupPath)
                    Dim fInfo As New IO.FileInfo(f)
                    If DateAdd(DateInterval.Month, BackupHisMonth, fInfo.LastAccessTime) < DateTime.Today Then
                        Try
                            IO.File.SetAttributes(f, IO.FileAttributes.Normal)
                            IO.File.Delete(f)
                        Catch ex As Exception
                            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   " & "Exception Delete DeleteBackupHistory : FileName=" & f & vbNewLine & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
                        End Try
                    End If
                    fInfo = Nothing
                Next

            End If

        End If
    End Sub

    Private Sub SaveBackupLog(vDateNow As DateTime, SharedPath As String)
        Try
            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   Save Backup Log"
            Dim frame As StackFrame = New StackFrame(1, True)
            Dim ClassName As String = frame.GetMethod.ReflectedType.Name
            Dim FunctionName As String = frame.GetMethod.Name
            Dim LineNo As Integer = frame.GetFileLineNumber

            Dim MY As String = DateTime.Now.ToString("yyyyMM")
            Dim DD As String = DateTime.Now.ToString("dd")
            Dim LogFolder As String = SharedPath & "\BackupLog\" & MY & "\" & DD & "\"
            If IO.Directory.Exists(LogFolder) = False Then
                IO.Directory.CreateDirectory(LogFolder)
            End If

            Dim FileName As String = LogFolder & "LogBackupDB" & "_" & vDateNow.ToString("yyyyMMddHH") & ".txt"
            Dim obj As New IO.StreamWriter(FileName, True)

            Dim LogMsg As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " " & FunctionName & " Line No :" & LineNo & Environment.NewLine
            LogMsg += txtMessage.Text.Trim
            obj.WriteLine(LogMsg)
            obj.Flush()
            obj.Close()
        Catch ex As Exception
            txtMessage.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "   Exception : Save Backup Log " & vbNewLine & ex.Message & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine
        End Try
    End Sub

    Private Sub FillInDatabaseInfo()
        Dim ini As New Org.Mentalis.Files.IniReader(INIFileName)
        ini.Section = "DbSetting"
        txtServerName.Text = ini.ReadString("ServerName")
        txtDatabaseName.Text = ini.ReadString("DatabaseName")
        txtUserID.Text = ini.ReadString("UserID")
        txtPassword.Text = ini.ReadString("Password")
        txtBackupFolder.Text = ini.ReadString("BackupTempPath")
        txtPhysicalPath.Text = ini.ReadString("BackupPath")
        If ini.ReadString("BackupToSharedPath") = 1 Then
            rdiSharedPath.Checked = True
            txtSharedPath.Enabled = True
            txtSharedUser.Enabled = True
            txtSharedPassword.Enabled = True
            txtSharedDomain.Enabled = True

            rdiPhysicalPath.Checked = False
            txtPhysicalPath.Enabled = False
        Else
            rdiPhysicalPath.Checked = True
            txtPhysicalPath.Enabled = True

            rdiSharedPath.Checked = False
            txtSharedPath.Enabled = False
            txtSharedUser.Enabled = False
            txtSharedPassword.Enabled = False
            txtSharedDomain.Enabled = False
        End If

        ini.Section = "SharedPath"
        txtSharedPath.Text = ini.ReadString("BackupSharedPath")
        txtSharedUser.Text = ini.ReadString("SharedUser")
        txtSharedPassword.Text = ini.ReadString("SharedPassword")
        txtSharedDomain.Text = ini.ReadString("SharedDomain")
        ini = Nothing
    End Sub

    Private Sub btnBrowseFolder_Click(sender As Object, e As EventArgs) Handles btnBrowseFolder.Click
        Dim fd As New FolderBrowserDialog
        If txtBackupFolder.Text.Trim <> "" Then
            fd.SelectedPath = txtBackupFolder.Text
        End If

        If fd.ShowDialog = DialogResult.OK Then
            txtBackupFolder.Text = fd.SelectedPath & "\"
        End If
    End Sub

    Private Sub btnBrowsePhysicalPath_Click(sender As Object, e As EventArgs) Handles btnBrowsePhysicalPath.Click
        Dim fd As New FolderBrowserDialog
        If txtPhysicalPath.Text.Trim <> "" Then
            fd.SelectedPath = txtPhysicalPath.Text
        End If

        If fd.ShowDialog = DialogResult.OK Then
            txtPhysicalPath.Text = fd.SelectedPath & "\"
        End If
    End Sub

    Private Sub rdiPhysicalPath_CheckedChanged(sender As Object, e As EventArgs) Handles rdiPhysicalPath.CheckedChanged
        If rdiPhysicalPath.Checked = True Then
            txtPhysicalPath.Enabled = True
            btnBrowsePhysicalPath.Enabled = True

            txtSharedPath.Enabled = False
            txtSharedUser.Enabled = False
            txtSharedDomain.Enabled = False
            txtSharedPassword.Enabled = False
        End If
    End Sub

    Private Sub rdiSharedPath_CheckedChanged(sender As Object, e As EventArgs) Handles rdiSharedPath.CheckedChanged
        If rdiSharedPath.Checked = True Then
            txtPhysicalPath.Enabled = False
            btnBrowsePhysicalPath.Enabled = False

            txtSharedPath.Enabled = True
            txtSharedUser.Enabled = True
            txtSharedDomain.Enabled = True
            txtSharedPassword.Enabled = True
        End If
    End Sub
#End Region
End Class
