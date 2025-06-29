Imports System.Net
Imports System.Net.Sockets
Imports System.Net.Sockets.IPPacketInformation
Imports System.Net.WebSockets
Imports System.Net.NetworkInformation.Net.Configuration
Imports System.Net.Cache
Imports System.Net.Http
Imports System.IO.IsolatedStorage
Imports System.IO.MemoryMappedFiles
Imports System.IO.Pipes
Imports System.IO.Ports
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Activities.Expressions
Imports System.Net.Security
Imports System.Net.Mime
Imports System
Imports System.Xml
Imports System.Management
Imports NetFwTypeLib









Public Class GestionnaireReseau
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As System.Windows.Forms.DialogResult
        'only import xml files because that is all the command will accept.
        OpenFileDialog1.Filter = "Xml Files (*.xml)|*.xml;"
        OpenFileDialog1.Multiselect = True
        result = OpenFileDialog1.ShowDialog()
        If result = System.Windows.Forms.DialogResult.OK Then
            If OpenFileDialog1.FileName <> "" Then

                Dim importprocess As Process = New Process
                'importprocess.StartInfo.CreateNoWindow = True
                importprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                'importprocess.StartInfo.UseShellExecute = False
                ' importprocess.StartInfo.RedirectStandardOutput = True
                If OpenFileDialog1.FileNames.Length >= 2 Then
                    For j As Integer = 0 To OpenFileDialog1.FileNames.Length - 1
                        importprocess = Process.Start("c:\Windows\System32\netsh.exe",
                        ("wlan add profile file=" + Chr(34) +
                        OpenFileDialog1.FileNames.GetValue(j) + Chr(34) + " user=current"))
                    Next j
                Else
                    TxtOpenPath.Text = OpenFileDialog1.FileName
                    importprocess = Process.Start("C:\Windows\System32\netsh.exe",
                    "wlan add profile file=" + Chr(34) +
                    OpenFileDialog1.FileName + Chr(34) + " user=current")
                    System.Threading.Thread.Sleep(2000)
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As System.Windows.Forms.DialogResult

        result = FolderBrowserDialog1.ShowDialog()
        If result = System.Windows.Forms.DialogResult.OK Then
            If FolderBrowserDialog1.SelectedPath <> "" Then
                TxtSavePath.Text = FolderBrowserDialog1.SelectedPath
                TxtSavePath.Enabled = False
                Network_Profile_Selection.Show()
            End If
        End If
    End Sub
End Class