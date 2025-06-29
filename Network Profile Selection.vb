Imports System.IO
Imports System.Windows.Forms

Public Class Network_Profile_Selection

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Network_Profile_Selection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Dim Profiles As List(Of String) = New List(Of String)
        Dim tempstr As String
        Dim tmpstr1 As String
        Dim tmpstr2 As String
        Dim i As Integer
        Dim wifistart As ProcessStartInfo = New ProcessStartInfo
        Dim wifiprofiles As Process = New Process
        Dim baddata As String
        Dim profilesread As StreamReader
        wifistart.CreateNoWindow = True
        wifistart.WindowStyle = ProcessWindowStyle.Hidden
        wifistart.FileName = "netsh"
        wifistart.Arguments = "wlan show profiles"
        wifistart.UseShellExecute = False
        wifistart.RedirectStandardOutput = True
        wifiprofiles = Process.Start(wifistart)

        Dim profilefile As StreamReader = wifiprofiles.StandardOutput
        If File.Exists(Application.StartupPath + "\Profiles.wpro") Then
            File.Delete(Application.StartupPath + "\Profiles.wpro")
        End If
        Dim profilewrite As StreamWriter =
          New StreamWriter(Application.StartupPath + "\Profiles.wpro", False)
        Do Until wifiprofiles.StandardOutput.EndOfStream
            profilewrite.WriteLine(profilefile.ReadLine)
        Loop
        profilewrite.Close()
        wifiprofiles.StandardOutput.Close()

        profilesread = New StreamReader(Application.StartupPath + "\Profiles.wpro")
        Do While Not profilesread.EndOfStream

            If i >= 9 Then
                tempstr = profilesread.ReadLine
                If tempstr.IndexOf("All User Profile") <> -1 Then
                    tmpstr2 = tempstr.Remove(tempstr.IndexOf("All"), 22)
                    ' MsgBox("All user profile replace:" + tmpstr2)
                    lstNetworkProfiles.Items.Add(tmpstr2)
                End If
                If tempstr.IndexOf("Current User Profile") <> -1 Then
                    tmpstr1 = tempstr.Remove(tempstr.IndexOf("Current"), 22)
                    lstNetworkProfiles.Items.Add(tmpstr1)
                    ' MsgBox("current user profile replace:" + tmpstr1)
                End If

            Else
                baddata = Nothing
                baddata = profilesread.ReadLine()
            End If
            i = i + 1
        Loop

        System.Threading.Thread.Sleep(3000)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Dim Profiles As List(Of String) = New List(Of String)
        Dim tempstr As String
        Dim tmpstr1 As String
        Dim tmpstr2 As String
        Dim i As Integer
        Dim wifistart As ProcessStartInfo = New ProcessStartInfo
        Dim wifiprofiles As Process = New Process
        Dim baddata As String
        Dim profilesread As StreamReader
        wifistart.CreateNoWindow = True
        wifistart.WindowStyle = ProcessWindowStyle.Hidden
        wifistart.FileName = "netsh"
        wifistart.Arguments = "wlan show profiles"
        wifistart.UseShellExecute = False
        wifistart.RedirectStandardOutput = True
        wifiprofiles = Process.Start(wifistart)

        Dim profilefile As StreamReader = wifiprofiles.StandardOutput
        If File.Exists(Application.StartupPath + "\Profiles.wpro") Then
            File.Delete(Application.StartupPath + "\Profiles.wpro")
        End If
        Dim profilewrite As StreamWriter =
          New StreamWriter(Application.StartupPath + "\Profiles.wpro", False)
        Do Until wifiprofiles.StandardOutput.EndOfStream
            profilewrite.WriteLine(profilefile.ReadLine)
        Loop
        profilewrite.Close()
        wifiprofiles.StandardOutput.Close()

        profilesread = New StreamReader(Application.StartupPath + "\Profiles.wpro")
        Do While Not profilesread.EndOfStream

            If i >= 9 Then
                tempstr = profilesread.ReadLine
                If tempstr.IndexOf("All User Profile") <> -1 Then
                    tmpstr2 = tempstr.Remove(tempstr.IndexOf("All"), 22)
                    ' MsgBox("All user profile replace:" + tmpstr2)
                    lstNetworkProfiles.Items.Add(tmpstr2)
                End If
                If tempstr.IndexOf("Current User Profile") <> -1 Then
                    tmpstr1 = tempstr.Remove(tempstr.IndexOf("Current"), 22)
                    lstNetworkProfiles.Items.Add(tmpstr1)
                    ' MsgBox("current user profile replace:" + tmpstr1)
                End If

            Else
                baddata = Nothing
                baddata = profilesread.ReadLine()
            End If
            i = i + 1
        Loop

        System.Threading.Thread.Sleep(3000)
    End Sub
End Class
